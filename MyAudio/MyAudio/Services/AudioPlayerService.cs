namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using MediaManager;
    using MyAudio.Interfaces;
    using MyAudio.Models;

    /// <summary>
    /// Service which controls playback of audio files.
    /// </summary>
    public class AudioPlayerService : IAudioPlayerService
    {
        public AudioPlayerService()
        {
            CrossMediaManager.Current.MediaItemFinished += CrossMediaManager_MediaItemFinished;
        }

        private async void CrossMediaManager_MediaItemFinished(object sender, EventArgs e) // object sender, EventArgs e
        {
            if (Queue.Count > CurrentQueueIndex + 1)
            {
                ++CurrentQueueIndex;
                await Play(AudioFilesList[CurrentQueueIndex], CurrentQueueIndex);
            }
            else
            {
                CurrentQueueIndex = 0;
                await Play(AudioFilesList[CurrentQueueIndex], CurrentQueueIndex);
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether an audio file is playing.
        /// </summary>
        public bool IsPlaying { get; set; } = false;

        public List<string> Queue { get; set; } = new List<string>();

        public List<IAudioFile> AudioFilesList { get; set; }

        public int CurrentQueueIndex { get; set; }

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

        /// <summary>
        /// Plays an audio file from a list of audio files.
        /// </summary>
        /// <param name="audioFile">The audio file to be played</param>
        /// <param name="audioFileIdx">Index of the audio file in the queue to play.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file from a list of audio files.</returns>
        public async Task Play(IAudioFile audioFile, int audioFileIdx)
        {
            await CrossMediaManager.Current.Play(Queue[audioFileIdx]);
            IsPlaying = true;
            CurrentQueueIndex = audioFileIdx;
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
