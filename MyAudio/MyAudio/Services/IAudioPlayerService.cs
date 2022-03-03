namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    /// <summary>s
    /// A service which controls playback of audio files.
    /// </summary>
    internal interface IAudioPlayerService
    {
        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="filePath">The file path of audio file to playback.</param>
        void Play(string filePath);
    }
}
