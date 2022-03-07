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
        /// Gets or sets a value indicating whether an audio file is playing.
        /// </summary>
        bool IsPlaying { get; set; }

        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="filePath">The path of the audio file to play.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file.</placeholder></returns>
        Task Play(string filePath);

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of changing the current audio playback state.</placeholder></returns>
        Task ChangeCurrentAudioState();
    }
}
