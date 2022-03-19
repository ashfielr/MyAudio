namespace MyAudio.Interfaces
{
    /// <summary>
    /// Interface for an audio file.
    /// </summary>
    public interface IAudioFile
    {
        /// <summary>
        /// Gets or sets ID used to identify audio file.
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Gets or sets title of the audio file.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets Artist of the audio file.
        /// </summary>
        string Artist { get; set; }

        /// <summary>
        /// Gets or sets Album name.
        /// </summary>
        string AlbumName { get; set; }

        /// <summary>
        /// Gets or sets the duration.
        /// </summary>
        int Duration { get; set; }

        /// <summary>
        /// Gets or sets image path.
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Gets or sets the file path for the mp3 file.
        /// </summary>
        string FilePath { get; set; }
    }
}
