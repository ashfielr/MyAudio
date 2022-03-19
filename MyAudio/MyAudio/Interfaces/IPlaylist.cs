namespace MyAudio.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>
    /// Interface for functionality of a playlist.
    /// </summary>
    public interface IPlaylist
    {
        /// <summary>
        /// Gets or sets ID of a playlist.
        /// </summary>
        int ID { get; set; }

        /// <summary>
        /// Gets or sets title of the playlist.
        /// </summary>
        string Title { get; set; }

        /// <summary>
        /// Gets or sets image for the playlist.
        /// </summary>
        string Image { get; set; }

        /// <summary>
        /// Gets the number of audio files in playlist.
        /// </summary>
        int NumOfAudioFiles { get; }

        /// <summary>
        /// Gets or sets the total duration of all audio files in the playlist.
        /// </summary>
        int TotalDuration { get; set; }
    }
}
