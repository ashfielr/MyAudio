﻿namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using MediaManager;

    /// <summary>
    /// Service which controls playback of audio files.
    /// </summary>
    internal class AudioPlayerService : IAudioPlayerService
    {
        /// <summary>
        /// Gets a value indicating whether an audio file is playing.
        /// </summary>
        public bool IsPlaying => CrossMediaManager.Current.IsPlaying();

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of changing the current audio playback state.</placeholder></returns>
        public async Task ChangeCurrentAudioState()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
            }
            else
            {
                await CrossMediaManager.Current.Play();
            }
        }

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
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file.</placeholder></returns>
        public async Task Play(string filePath)
        {
            await CrossMediaManager.Current.Play(filePath);
        }
    }
}
