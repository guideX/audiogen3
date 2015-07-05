'nexENCODE Studio 5.0 Alpha 1.3
'October 6th, 2013
Option Explicit On
Option Strict On
Imports System.Runtime.InteropServices
Namespace AudiogenWaveWriter
    Public Class WinMMInterop
        <StructLayoutAttribute(LayoutKind.Sequential, Pack:=2)> Public Structure WAVEFORMATEX
            Public wFormatTag As Int16
            Public nChannels As Int16
            Public nSamplesPerSec As Integer
            Public nAvgBytesPerSec As Integer
            Public nBlockAlign As Int16
            Public wBitsPerSample As Int16
            Public cbSize As Int16
        End Structure

        <StructLayoutAttribute(LayoutKind.Sequential)> Public Structure MMIOINFO
            Public dwFlags As Integer
            Public fccIOProc As Integer
            Public pIOProc As IntPtr
            Public wErrorRet As Integer
            Public hTask As IntPtr
            Public cchBuffer As Integer
            Public pchBuffer As IntPtr
            Public pchNext As IntPtr
            Public pchEndRead As IntPtr
            Public pchEndWrite As IntPtr
            Public lBufOffset As Integer
            Public lDiskOffset As Integer
            Public adwInfo0 As Integer
            Public adwInfo1 As Integer
            Public adwInfo2 As Integer
            Public adwInfo3 As Integer
            Public dwReserved1 As Integer
            Public dwReserved2 As Integer
            Public hMMIO As IntPtr
        End Structure

        <StructLayoutAttribute(LayoutKind.Sequential)> Public Structure MMCKINFO
            Public ckid As Integer
            Public ckSize As Integer
            Public fccType As Integer
            Public dwDataOffset As Integer
            Public dwFlags As Integer
        End Structure

        Public Declare Function mmioClose Lib "winmm" (ByVal hmmio As IntPtr, ByVal uFlags As Integer) As Integer
        Public Declare Function mmioDescend Lib "winmm" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByRef lpckParent As MMCKINFO, ByVal uFlags As Integer) As Integer
        Public Declare Function mmioDescendParent Lib "winmm" Alias "mmioDescend" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByVal x As Integer, ByVal uFlags As Integer) As Integer
        Public Declare Auto Function mmioOpen Lib "winmm" (ByVal szFileName As String, ByVal lpMmioInfo As IntPtr, ByVal dwOpenFlags As Integer) As IntPtr
        Public Declare Function mmioRead Lib "winmm" (ByVal hmmio As IntPtr, ByVal pch As IntPtr, ByVal cch As Integer) As Integer
        Public Declare Function mmioWrite Lib "winmm" (ByVal hmmio As IntPtr, ByVal pch As IntPtr, ByVal cch As Integer) As Integer
        Public Declare Function mmioReadWaveFormat Lib "winmm" Alias "mmioRead" (ByVal hmmio As IntPtr, ByRef format As WAVEFORMATEX, ByVal cch As Integer) As Integer
        Public Declare Function mmioWriteWaveFormat Lib "winmm" Alias "mmioWrite" (ByVal hmmio As IntPtr, ByRef format As WAVEFORMATEX, ByVal cch As Integer) As Integer
        Public Declare Function mmioSeek Lib "winmm" (ByVal hmmio As IntPtr, ByVal lOffset As Integer, ByVal iOrigin As Integer) As Integer
        Public Declare Auto Function mmioStringToFOURCC Lib "winmm" (<MarshalAs(UnmanagedType.LPTStr)> ByVal sz As String, ByVal uFlags As Integer) As Integer
        Public Declare Function mmioAscend Lib "winmm" (ByVal hmmio As IntPtr, ByRef lpck As MMCKINFO, ByVal uFlags As Integer) As Integer
        Public Declare Function mmioCreateChunk Lib "winmm" (ByVal hmmio As IntPtr, ByRef pmmcki As MMCKINFO, ByVal fuCreate As Integer) As Integer
        Public Const MMIO_READ As Integer = &H0
        Public Const MMIO_WRITE As Integer = &H1
        Public Const MMIO_READWRITE As Integer = &H2
        Public Const MMIO_FINDCHUNK As Integer = &H10
        Public Const MMIO_FINDRIFF As Integer = &H20
        Public Const MMIO_CREATERIFF As Integer = &H20
        Public Const MMIO_ALLOCBUF As Integer = &H10000
        Public Const MMIO_CREATE As Integer = &H1000
        Public Const MMSYSERR_NOERROR As Integer = 0
        Public Const SEEK_CUR As Integer = 1
        Public Const SEEK_END As Integer = 2
        Public Const SEEK_SET As Integer = 0
    End Class
End Namespace