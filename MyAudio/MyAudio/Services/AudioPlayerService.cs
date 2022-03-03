namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using MediaManager;

    /// <summary>
    /// Service which controls playback of audio files.
    /// </summary>
    internal class AudioPlayerService : IAudioPlayerService
    {

        /// <summary>
        /// Pauses the player.
        /// </summary>
        public void Pause()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="filePath">The file path of audio file to playback.</param>
        public async void Play(string filePath)
        {
            FileStream audioSourceStream = File.Open(filePath, FileMode.Open);
            await CrossMediaManager.Current.Play(filePath);
        }
    }
}
