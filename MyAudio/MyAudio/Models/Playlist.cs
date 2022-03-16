﻿namespace MyAudio.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MyAudio.Interfaces;
    using SQLite;

    /// <summary>
    /// Class for a playlist of audio files.
    /// </summary>
    public class Playlist : IPlaylist
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Playlist"/> class.
        /// </summary>
        public Playlist()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Playlist"/> class.
        /// </summary>
        /// /// <param name="title">Title of the audio file.</param>
        /// <param name="numOfAudioFiles">The number of audio files in the playlist.</param>
        /// <param name="image">The path for the image.</param>
        /// <param name="totalDuration">The total duration of the audio files in the playlist.</param>
        public Playlist(string title, int numOfAudioFiles, string image, int totalDuration)
        {
            this.Title = title;
            this.Image = image;
            this.NumOfAudioFiles = numOfAudioFiles;
            this.TotalDuration = totalDuration;
        }

        /// <summary>
        /// Gets or sets ID of a playlist.
        /// </summary>
        [PrimaryKey]
        [AutoIncrement]
        public int ID { get; set; }

        /// <summary>
        /// Gets or sets title of the playlist.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Gets or sets image for the playlist.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Gets or sets the number of audio files in playlist.
        /// </summary>
        public int NumOfAudioFiles { get; set; }

        /// <summary>
        /// Gets or sets the total duration of all audio files in the playlist.
        /// </summary>
        public int TotalDuration { get; set; }
    }
}
