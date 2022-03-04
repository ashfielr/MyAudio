namespace MyAudio.Services
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
        public void ChangeCurrentAudioState()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                CrossMediaManager.Current.Pause().Wait();
            }
            else
            {
                CrossMediaManager.Current.Play().Wait();
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
        public void Play(string filePath)
        {
            CrossMediaManager.Current.Play(filePath).Wait();
        }
    }
}
