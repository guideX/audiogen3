using System.Threading;
namespace Audiogen3.Mp3Decoder {
    /// <summary>
    /// Decode Controller
    /// </summary>
    public class DecodeController {
        /// <summary>
        /// Percent Changed
        /// </summary>
        public event PercentChangedEventHandler PercentChanged;
        /// <summary>
        /// Percent Changed Event Handler
        /// </summary>
        /// <param name="percent"></param>
        public delegate void PercentChangedEventHandler(long percent);
        /// <summary>
        /// Mp3 Stream
        /// </summary>
        private Mp3Sharp.Mp3Stream _mp3Stream;
        /// <summary>
        /// Wave Writer
        /// </summary>
        private WaveWriter _writer;
        /// <summary>
        /// Read Decode Result
        /// </summary>
        private int _readDecodeResult;
        /// <summary>
        /// Chunk Size
        /// </summary>
        private int _chunkSize;
        /// <summary>
        /// Num Bytes Read
        /// </summary>
        private long _numBytesRead;
        /// <summary>
        /// Percent
        /// </summary>
        private long _percent;
        /// <summary>
        /// Num Bytes to Read
        /// </summary>
        private long _numBytesToRead;
        /// <summary>
        /// Total Bytes
        /// </summary>
        private long _totalBytes;
        /// <summary>
        /// Bytes
        /// </summary>
        private byte[] _bytes;
        /// <summary>
        /// Current File
        /// </summary>
        public string CurrentFile;
        /// <summary>
        /// Current File Name
        /// </summary>
        public string CurrentFileName;
        /// <summary>
        /// Read Decode Chunk
        /// </summary>
        private void ReadDecodeChunk() {
            try {
                _readDecodeResult = _mp3Stream.Read(_bytes, (int)_numBytesRead, _chunkSize); // Read Decode Result
                _writer.Write(_bytes, _chunkSize); // Writer Write
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Decode File
        /// </summary>
        /// <param name="file"></param>
        /// <returns></returns>
        public bool DecodeFile() {
            try {
                if (!string.IsNullOrEmpty(CurrentFile)) {
                    _chunkSize = 4096; // Set Default Chunk Size
                    if (CurrentFile.ToLower().Contains(".mp3")) { // Check Mp3
                        if (System.IO.File.Exists(CurrentFile)) { // Check File Exists
                            CurrentFileName = System.IO.Path.GetFileName(CurrentFile);
                            using (var mp3Stream = new Mp3Sharp.Mp3Stream(CurrentFile)) { // Create Mp3 Stream
                                _mp3Stream = mp3Stream; // Set Mp3 Stream
                                _bytes = new byte[mp3Stream.Length]; // Set Bytes
                                _totalBytes = mp3Stream.Length; // Set Total Bytes
                                _numBytesToRead = _totalBytes; // Set Num Bytes to Read
                                _numBytesRead = 0; // Set Num Bytes Read to 0
                                using (var writer = new WaveWriter(CurrentFile.Replace(".mp3", ".wav"))) { // Create Writer
                                    _writer = writer; // Set Writer
                                    while (_numBytesToRead > 0) { // Loop through Chunks
                                        if (_chunkSize > _numBytesToRead) { // Check Progress isn't greater than remaining bytes
                                            _chunkSize = 1; // Check a chunk at a time
                                        }
                                        var t = new Thread(ReadDecodeChunk);
                                        t.Start();
                                        if (!t.Join(1500)) {
                                            t.Abort(); // Oops! We read 1 too many bytes! Lets stop trying, we got everything.
                                            _numBytesToRead = 0; // This should take us out of the loop soon
                                        }
                                        if (_readDecodeResult == 0) {
                                            break;
                                        }
                                        _numBytesRead += _readDecodeResult;
                                        _numBytesToRead -= _readDecodeResult;
                                        _percent = ((int)_numBytesRead * 100 / _totalBytes);
                                        if (PercentChanged != null) {
                                            PercentChanged(_percent);
                                        }
                                    }
                                    _writer = null;
                                    writer.Close();
                                    writer.Dispose();
                                }
                                _numBytesToRead = _bytes.Length;
                                _mp3Stream = null;
                            }
                        }
                    }
                }
                return true;
            } catch {
                throw;
            }
        }
    }
}