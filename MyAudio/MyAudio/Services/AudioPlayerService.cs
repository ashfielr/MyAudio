namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using MediaManager;
    using MyAudio.Interfaces;

    /// <summary>
    /// Service which controls playback of audio files.
    /// </summary>
    public class AudioPlayerService : IAudioPlayerService
    {
        /// <summary>
        /// Gets or sets a value indicating whether an audio file is playing.
        /// </summary>
        public bool IsPlaying { get; set; } = false;

        public IAudioFile CurrentAudioFile { get; set; }

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of changing the current audio playback state.</placeholder></returns>
        public async Task ChangeCurrentAudioState()
        {
            if (CrossMediaManager.Current.IsPlaying())
            {
                await CrossMediaManager.Current.Pause();
                IsPlaying = false;
            }
            else
            {
                await CrossMediaManager.Current.Play();
                IsPlaying = true;
            }
        }

        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file.</placeholder></returns>
        public async Task Play(IAudioFile audioFile)
        {
            await CrossMediaManager.Current.Play(audioFile.FilePath);
            IsPlaying = true;
            CurrentAudioFile = audioFile;
            OnCurrentAudioFileChanged();
        }

        public event EventHandler CurrentAudioFileChanged;

        protected virtual void OnCurrentAudioFileChanged()
        {
            EventHandler handler = CurrentAudioFileChanged;
            handler?.Invoke(this, EventArgs.Empty);
        }
    }
}
