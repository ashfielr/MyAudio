namespace MyAudio.Interfaces
{
    /// <summary>
    /// Interface for an audio file.
    /// </summary>
    public interface IAudioFile
    {
        /// <summary>
        /// Gets or sets ID of an audio file.
        /// </summary>
        string ID { get; set; }

        /// <summary>
        /// Gets or sets title of the audio file.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets name of the artist.
        /// </summary>
        string Artist { get; set; }

        /// <summary>
        /// Gets or sets name of the album.
        /// </summary>
        string AlbumName { get; set; }

        /// <summary>
        /// Gets or sets duration of the audio file.
        /// </summary>
        int Duration { get; set; }

        /// <summary>
        /// Gets or sets image for the audio file.
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Gets or sets the file path for the mp3 file.
        /// </summary>
        string FilePath { get; set; }
    }
}
