namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;

    /// <summary>s
    /// A service which controls playback of audio files.
    /// </summary>
    public interface IAudioPlayerService
    {
        /// <summary>
        /// Gets or sets a value indicating whether an audio file is playing.
        /// </summary>
        bool IsPlaying { get; set; }

        List<string> Queue { get; set; }

        List<IAudioFile> AudioFilesList { get; set; }

        IAudioFile CurrentAudioFile { get; set; }

        event EventHandler CurrentAudioFileChanged;

        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="audioFile">The audio file to play.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file.</placeholder></returns>
        Task Play(IAudioFile audioFile);

        /// <summary>
        /// Plays an audio file from a list of audio files.
        /// </summary>
        /// <param name="audioFile">The audio file to be played</param>
        /// <param name="audioFileIdx">Index of the audio file in the queue to play.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file from a list of audio files.</returns>
        Task Play(IAudioFile audioFile, int audioFileIdx);

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of changing the current audio playback state.</placeholder></returns>
        Task ChangeCurrentAudioState();
    }
}
