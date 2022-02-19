namespace MyAudio.Models
{
    using MyAudio.Interfaces;
    using SQLite;

    /// <summary>
    /// Model for audio file.
    /// </summary>
    internal class AudioFile : IAudioFile
    {
        /// <summary>
        /// Gets or sets ID of an audio file.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets name of the artist.
        /// </summary>
        public string Artist { get; set; }

        /// <summary>
        /// Gets or sets name of the album.
        /// </summary>
        public string AlbumName { get; set; }

        /// <summary>
        /// Gets or sets duration of the audio file.
        /// </summary>
        public int Duration { get; set; }

        /// <summary>
        /// Gets or sets image for the audio file.
        /// </summary>
        public string Image { get; set; }
    }
}
