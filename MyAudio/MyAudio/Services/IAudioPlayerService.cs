namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>s
    /// A service which controls playback of audio files.
    /// </summary>
    internal interface IAudioPlayerService
    {
        /// <summary>
        /// Gets a value indicating whether an audio file is playing.
        /// </summary>
        bool IsPlaying { get; }

        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="filePath">The path of the audio file to play.</param>
        void Play(string filePath);

        /// <summary>
        /// Pauses the currently playing file.
        /// </summary>
        void Pause();

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        void ChangeCurrentAudioState();
    }
}
