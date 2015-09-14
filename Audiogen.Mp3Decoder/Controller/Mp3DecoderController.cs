using System.Threading;
using AudiogenWaveWriter.AudiogenWaveWriter;
using Audiogen.Mp3Decoder.Models;
/// <summary>
/// Mp3 Decoder
/// </summary>
namespace Audiogen.Mp3Decoder.Controllers {
    /// <summary>
    /// Decode Controller
    /// </summary>
    public class Mp3DecoderController {
        /// <summary>
        /// Mp3 Decode File Model
        /// </summary>
        public Mp3DecodeFileModel Model { get; set; }
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
        /// Mp3 Decoder Controller
        /// </summary>
        public Mp3DecoderController() {
            try {
                Model = new Mp3DecodeFileModel();
            } catch {
                throw;
            }
        }
        /// <summary>
        /// Read Decode Chunk
        /// </summary>
        private void ReadDecodeChunk() {
            try {
                Model.ReadDecodeResult = _mp3Stream.Read(Model.Bytes, (int)Model.NumBytesRead, Model.ChunkSize); // Read Decode Result
                _writer.Write(Model.Bytes, Model.ChunkSize); // Writer Write
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
                if (!string.IsNullOrEmpty(Model.CurrentFile)) {
                    Model.ChunkSize = 4096; // Set Default Chunk Size
                    if (Model.CurrentFile.ToLower().Contains(".mp3")) { // Check Mp3
                        if (System.IO.File.Exists(Model.CurrentFile)) { // Check File Exists
                            Model.CurrentFileName = System.IO.Path.GetFileName(Model.CurrentFile);
                            using (var mp3Stream = new Mp3Sharp.Mp3Stream(Model.CurrentFile)) { // Create Mp3 Stream
                                _mp3Stream = mp3Stream; // Set Mp3 Stream
                                Model.Bytes = new byte[mp3Stream.Length]; // Set Bytes
                                Model.TotalBytes = mp3Stream.Length; // Set Total Bytes
                                Model.NumBytesToRead = Model.TotalBytes; // Set Num Bytes to Read
                                Model.NumBytesRead = 0; // Set Num Bytes Read to 0
                                using (var writer = new WaveWriter(Model.CurrentFile.Replace(".mp3", ".wav"))) { // Create Writer
                                    _writer = writer; // Set Writer
                                    while (Model.NumBytesToRead > 0) { // Loop through Chunks
                                        if (Model.ChunkSize > Model.NumBytesToRead) { // Check Progress isn't greater than remaining bytes
                                            Model.ChunkSize = 1; // Check a chunk at a time
                                        }
                                        var t = new Thread(ReadDecodeChunk);
                                        t.Start();
                                        if (!t.Join(1500)) {
                                            t.Abort(); // Oops! We read 1 too many bytes! Lets stop trying, we got everything.
                                            Model.NumBytesToRead = 0; // This should take us out of the loop soon
                                        }
                                        if (Model.ReadDecodeResult == 0) {
                                            break;
                                        }
                                        Model.NumBytesRead += Model.ReadDecodeResult;
                                        Model.NumBytesToRead -= Model.ReadDecodeResult;
                                        Model.Percent = ((int)Model.NumBytesRead * 100 / Model.TotalBytes);
                                        if (PercentChanged != null) {
                                            PercentChanged(Model.Percent);
                                        }
                                    }
                                    _writer = null;
                                    writer.Close();
                                    writer.Dispose();
                                }
                                Model.NumBytesToRead = Model.Bytes.Length;
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