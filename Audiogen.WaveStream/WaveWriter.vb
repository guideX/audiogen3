'nexENCODE Studio 5.0 Alpha 1.3
'October 6th, 2013
Option Explicit On
Option Strict Off
Imports System.IO
Imports System.Runtime.InteropServices
Namespace AudiogenWaveWriter
    Public Class WaveWriter
        Inherits Stream
        Private waveFile As String
        Private hMmio As IntPtr = IntPtr.Zero
        Private disposed As Boolean = False
        Private format As WinMMInterop.WAVEFORMATEX
        Private dataOffset As Integer = 0
        Private audioLength As Integer = 0
        Private mmckInfoChild As WinMMInterop.MMCKINFO
        Private mmckInfoParent As WinMMInterop.MMCKINFO

        Public Sub New()
            MyBase.New()
            format.cbSize = 0
            format.nChannels = 2
            format.nSamplesPerSec = 44100
            format.wBitsPerSample = 16
            format.nBlockAlign = 4
            format.wFormatTag = 1
        End Sub

        Public Sub New(ByVal file As String)
            Me.New()
            Filename = file
        End Sub

        Public Sub New(ByVal file As String, ByVal samplingFrequency As Integer)
            Me.New()
            samplingFrequency = samplingFrequency
            Filename = file
        End Sub

        Public Sub New(ByVal file As String, ByVal samplingFrequency As Integer, ByVal channels As Short)
            Me.New()
            format.nSamplesPerSec = samplingFrequency
            format.nChannels = channels
            Filename = file
        End Sub

        Public Sub New(ByVal file As String, ByVal samplingFrequency As Integer, ByVal channels As Short, ByVal bitsPerSample As Short)
            Me.New()
            format.nSamplesPerSec = samplingFrequency
            format.nChannels = channels
            format.wBitsPerSample = bitsPerSample
            Filename = file
        End Sub

        Protected Overrides Sub Finalize()
            DisposeResources(False)
        End Sub

        Public Overloads Sub Dispose()
            DisposeResources(True)
            GC.SuppressFinalize(Me)
        End Sub

        Protected Overridable ReadOnly Property Handle() As IntPtr
            Get
                Return hMmio
            End Get
        End Property

        Protected Overridable Sub DisposeResources(ByVal disposing As Boolean)
            If Not (disposed) Then
                If (disposing) Then
                    'Do Nothing 
                End If
                CloseWaveFile()
                disposed = True
            End If
        End Sub

        Public Property Filename() As String
            Get
                Return waveFile
            End Get
            Set(ByVal Value As String)
                If Not (hMmio.Equals(IntPtr.Zero)) Then
                    CloseWaveFile()
                End If
                waveFile = Value
                CreateWaveFile()
            End Set
        End Property

        Public Property Channels() As Short
            Get
                Return format.nChannels
            End Get
            Set(ByVal Value As Short)
                If Not (hMmio.Equals(IntPtr.Zero)) Then
                    Throw New InvalidOperationException("Cannot change number of audio channels on an open file.")
                End If
                format.nChannels = Value
            End Set
        End Property

        Public Property SamplingFrequency() As Integer
            Get
                Return format.nSamplesPerSec
            End Get
            Set(ByVal Value As Integer)
                If Not (hMmio.Equals(IntPtr.Zero)) Then
                    Throw New InvalidOperationException("Cannot change sampling frequency on an open file.")
                End If
                format.nSamplesPerSec = Value
            End Set
        End Property

        Public Property BitsPerSample() As Short
            Get
                Return format.wBitsPerSample
            End Get
            Set(ByVal Value As Short)
                If Not (hMmio.Equals(IntPtr.Zero)) Then
                    Throw New InvalidOperationException("Cannot change bits/sample on an open file.")
                End If
                format.wBitsPerSample = Value
            End Set
        End Property

        Public Overrides Sub Flush()

        End Sub

        Public Overrides ReadOnly Property CanRead() As Boolean
            Get
                Return Not (hMmio.Equals(IntPtr.Zero))
            End Get
        End Property

        Public Overrides ReadOnly Property CanSeek() As Boolean
            Get
                Return Not (hMmio.Equals(IntPtr.Zero))
            End Get
        End Property

        Public Overrides ReadOnly Property CanWrite() As Boolean
            Get
                Return Not (hMmio.Equals(IntPtr.Zero))
            End Get
        End Property

        Public Overrides ReadOnly Property Length() As Long
            Get
                Return audioLength
            End Get
        End Property

        Public Overrides Sub SetLength(ByVal length As Long)
            Throw New InvalidOperationException( _
             "This class can only read files.  Use the WaveStreamWriter class to write files.")
        End Sub

        Public Overrides Property Position() As Long
            Get
                Return 0
            End Get
            Set(ByVal Value As Long)
                Seek(Value, SeekOrigin.Begin)
            End Set
        End Property

        Public Overridable Overloads Function Read(ByVal buffer As Byte(), ByVal count As Integer) As Integer
            Return Read(buffer, 0, count)
        End Function

        Public Overloads Overrides Function Read(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer) As Integer
            If (hMmio.Equals(IntPtr.Zero)) Then
                Throw New InvalidOperationException("No wave data is open")
            End If

            If (offset <> 0) Then
                Seek(offset, SeekOrigin.Current)
            End If

            Dim handle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
            Dim ptrBuffer As IntPtr = handle.AddrOfPinnedObject()

            Dim dataRemaining As Integer = (dataOffset + audioLength - _
                WinMMInterop.mmioSeek(hMmio, 0, WinMMInterop.SEEK_CUR))
            Dim amtRead As Integer = 0
            If (count < dataRemaining) Then
                amtRead = WinMMInterop.mmioRead(hMmio, ptrBuffer, count)
            ElseIf (dataRemaining > 0) Then
                amtRead = WinMMInterop.mmioRead(hMmio, ptrBuffer, dataRemaining)
            End If

            If (handle.IsAllocated) Then
                handle.Free()
            End If
            Return amtRead
        End Function

        Public Overridable Function Read16Bit(ByVal buffer() As Short, ByVal count As Integer) As Integer
            Return Read16Bit(buffer, 0, count)
        End Function

        Public Overridable Function Read16Bit(ByVal buffer() As Short, ByVal offset As Integer, ByVal count As Integer) As Integer
            If (hMmio.Equals(IntPtr.Zero)) Then
                Throw New InvalidOperationException("No wave data is open")
            End If
            If (offset <> 0) Then
                Seek((offset * 2), SeekOrigin.Current)
            End If

            Dim handle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
            Dim ptrBuffer As IntPtr = handle.AddrOfPinnedObject()

            Dim dataRemaining As Integer = (dataOffset + audioLength - _
                WinMMInterop.mmioSeek(hMmio, 0, WinMMInterop.SEEK_CUR)) / 2
            Dim amtRead As Integer = 0
            If (count < dataRemaining) Then
                amtRead = WinMMInterop.mmioRead(hMmio, ptrBuffer, count * 2)
            ElseIf (dataRemaining > 0) Then
                amtRead = WinMMInterop.mmioRead(hMmio, ptrBuffer, dataRemaining * 2)
            End If

            If (handle.IsAllocated) Then
                handle.Free()
            End If
            Return amtRead
        End Function

        Public Overridable Overloads Sub Write(ByVal buffer() As Byte, ByVal count As Integer)
            Write(buffer, 0, count)
        End Sub

        Public Overloads Overrides Sub Write(ByVal buffer() As Byte, ByVal offset As Integer, ByVal count As Integer)
            If (hMmio.Equals(IntPtr.Zero)) Then
                Throw New InvalidOperationException("No wave file is open")
            End If
            If (offset <> 0) Then
                Seek(offset, SeekOrigin.Current)
            End If
            Dim handle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
            Dim ptrBuffer As IntPtr = handle.AddrOfPinnedObject()
            Dim amtWrite = WinMMInterop.mmioWrite(hMmio, ptrBuffer, count)
            If (amtWrite <> count) Then
                Throw New IOException(String.Format("Data truncation: only wrote {0} of {1} requested bytes", amtWrite, count))
            End If
            If (handle.IsAllocated) Then
                handle.Free()
            End If
        End Sub

        Public Overridable Sub Write16Bit(ByVal buffer() As Short, ByVal count As Integer)
            Write16Bit(buffer, 0, count)
        End Sub

        Public Overridable Sub Write16Bit(ByVal buffer() As Short, ByVal offset As Integer, ByVal count As Integer)
            If (hMmio.Equals(IntPtr.Zero)) Then
                Throw New InvalidOperationException("No wave file is open")
            End If
            If (offset <> 0) Then
                Seek((offset * 2), SeekOrigin.Current)
            End If
            Dim handle As GCHandle = GCHandle.Alloc(buffer, GCHandleType.Pinned)
            Dim ptrBuffer As IntPtr = handle.AddrOfPinnedObject()
            Dim amtWrite As Integer = WinMMInterop.mmioWrite(hMmio, ptrBuffer, count * 2)
            If (amtWrite <> (count * 2)) Then
                Throw New IOException(String.Format("Data truncation: only wrote {0} of {1} requested bytes", amtWrite, count))
            End If
            If (handle.IsAllocated) Then
                handle.Free()
            End If
        End Sub

        Public Overrides Function Seek(ByVal position As Long, ByVal origin As SeekOrigin) As Long
            If (hMmio.Equals(IntPtr.Zero)) Then
                Throw New InvalidOperationException("No wave file is open")
            End If
            Dim offset As Integer = position
            Dim mmOrigin As Integer = WinMMInterop.SEEK_CUR
            If (origin = SeekOrigin.Begin) Then
                offset += dataOffset
                mmOrigin = WinMMInterop.SEEK_SET
            ElseIf (origin = SeekOrigin.End) Then
                mmOrigin = WinMMInterop.SEEK_END
            End If
            Dim result As Integer = WinMMInterop.mmioSeek(hMmio, offset, mmOrigin)
            If (result = -1) Then
                Throw New WaveException( _
                 String.Format("Failed to seek to position {0} in file", position))
            End If
            Return result
        End Function

        Private Sub CreateWaveFile()
            CloseWaveFile()
            hMmio = WinMMInterop.mmioOpen(waveFile, IntPtr.Zero, WinMMInterop.MMIO_ALLOCBUF Or WinMMInterop.MMIO_READWRITE Or WinMMInterop.MMIO_CREATE)
            If (hMmio.Equals(IntPtr.Zero)) Then
                Throw New IOException(String.Format("Could not open file {0}", waveFile))
            End If
            CreateWaveFormatHeader()
        End Sub

        Private Sub CreateWaveFormatHeader()
            Dim result As Integer = 0
            format.nBlockAlign = ((format.nChannels * format.wBitsPerSample) / 8)
            format.nAvgBytesPerSec = format.nSamplesPerSec * format.nBlockAlign
            mmckInfoParent = New WinMMInterop.MMCKINFO()
            mmckInfoParent.fccType = WinMMInterop.mmioStringToFOURCC("WAVE", 0)
            result = WinMMInterop.mmioCreateChunk(hMmio, mmckInfoParent, WinMMInterop.MMIO_CREATERIFF)
            If (result <> WinMMInterop.MMSYSERR_NOERROR) Then
                CloseWaveFile()
                Throw New WaveException("Could not write the WAVE RIFF header chunk to the file.")
            End If
            mmckInfoChild = New WinMMInterop.MMCKINFO()
            mmckInfoChild.ckid = WinMMInterop.mmioStringToFOURCC("fmt", 0)
            mmckInfoChild.ckSize = Marshal.SizeOf(format.GetType())
            result = WinMMInterop.mmioCreateChunk(hMmio, mmckInfoChild, 0)
            If (result <> WinMMInterop.MMSYSERR_NOERROR) Then
                CloseWaveFile()
                Throw New WaveException("Could not write the 'fmt' header chunk to the file.")
            End If
            Dim size As Integer = WinMMInterop.mmioWriteWaveFormat(hMmio, format, mmckInfoChild.ckSize)
            If (size <> mmckInfoChild.ckSize) Then
                CloseWaveFile()
                Throw New WaveException("Could not write the format information into the 'fmt' header chunk of the file.")
            End If
            result = WinMMInterop.mmioAscend(hMmio, mmckInfoChild, 0)
            If (result <> WinMMInterop.MMSYSERR_NOERROR) Then
                CloseWaveFile()
                Throw New WaveException("Could not ascend out of 'fmt' header chunk.")
            End If
            mmckInfoChild.ckid = WinMMInterop.mmioStringToFOURCC("data", 0)
            result = WinMMInterop.mmioCreateChunk(hMmio, mmckInfoChild, 0)
            If (result <> WinMMInterop.MMSYSERR_NOERROR) Then
                CloseWaveFile()
                Throw New WaveException("Could not create the 'data' chunk for the audio data.")
            End If
            dataOffset = WinMMInterop.mmioSeek(hMmio, 0, WinMMInterop.SEEK_CUR)
        End Sub

        Public Sub CloseWaveFile()
            If Not (hMmio.Equals(IntPtr.Zero)) Then
                Dim result As Integer
                result = WinMMInterop.mmioAscend(hMmio, mmckInfoChild, 0)
                result = WinMMInterop.mmioAscend(hMmio, mmckInfoParent, 0)
                WinMMInterop.mmioClose(hMmio, 0)
                hMmio = IntPtr.Zero
                audioLength = 0
            End If
        End Sub
    End Class
End Namespace
