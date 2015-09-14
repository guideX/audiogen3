namespace Audiogen.Mp3Decoder.Models {
    /// <summary>
    /// Mp3 Decode File Model
    /// </summary>
    public class Mp3DecodeFileModel {
        /// <summary>
        /// Read Decode Result
        /// </summary>
        public int ReadDecodeResult;
        /// <summary>
        /// Chunk Size
        /// </summary>
        public int ChunkSize;
        /// <summary>
        /// Num Bytes Read
        /// </summary>
        public long NumBytesRead;
        /// <summary>
        /// Percent
        /// </summary>
        public long Percent;
        /// <summary>
        /// Num Bytes to Read
        /// </summary>
        public long NumBytesToRead;
        /// <summary>
        /// Total Bytes
        /// </summary>
        public long TotalBytes;
        /// <summary>
        /// Bytes
        /// </summary>
        public byte[] Bytes;
        /// <summary>
        /// Current File
        /// </summary>
        public string CurrentFile;
        /// <summary>
        /// Current File Name
        /// </summary>
        public string CurrentFileName;
    }
}