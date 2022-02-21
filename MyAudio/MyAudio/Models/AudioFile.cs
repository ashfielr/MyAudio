namespace MyAudio.Models
{
    using MyAudio.Interfaces;
    using SQLite;

    /// <summary>
    /// Model for audio file.
    /// </summary>
    internal class AudioFile : IAudioFile
    {
        private int id;
        private string title;
        private string artist;
        private string albumName;
        private int duration;
        private string image;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFile"/> class.
        /// </summary>
        public AudioFile()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFile"/> class.
        /// </summary>
        /// <param name="id">Unique ID.</param>
        /// /// <param name="title">Title of the audio file.</param>
        /// <param name="artist">Artist of the audio file.</param>
        /// <param name="albumName">The name of the album the audio file is from.</param>
        /// <param name="duration">The length of audio file in seconds.</param>
        /// <param name="image">The path for the image.</param>
        public AudioFile(int id, string title, string artist, string albumName, int duration, string image)
        {
            this.id = id;
            this.title = title;
            this.artist = artist;
            this.albumName = albumName;
            this.duration = duration;
            this.image = image;
        }

        /// <summary>
        /// Gets or sets ID of an audio file.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets title of the audio file.
        /// </summary>
        public string Title { get; set; }

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
