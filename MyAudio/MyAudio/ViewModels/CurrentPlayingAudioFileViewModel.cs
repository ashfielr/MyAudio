namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Interfaces;
    using MyAudio.Services;
    using Xamarin.Forms;

    public class CurrentPlayingAudioFileViewModel : AudioFileViewModel, ICurrentPlayingAudioFileViewModel
    {

        private string currentAudioStateImg;

        public CurrentPlayingAudioFileViewModel(IAudioPlayerService _audioPlayerService)
            : base(_audioPlayerService.CurrentAudioFile, _audioPlayerService)
        {
            audioPlayerService.CurrentAudioFileChanged += audioPlayerService_CurrentAudioFileChanged;
            ChangeAudioStateCommand = new Command(async () => await ChangeAudioState());
            UpdateAudioStateImg();
        }

        public void Initialise()
        {
            UpdateAudioStateImg();
        }

        /// <summary>
        /// Gets or sets command to change audio state (playing or paused).
        /// </summary>
        public ICommand ChangeAudioStateCommand { get; set; }

        public IAudioFile AudioFile
        {
            get => audioPlayerService.CurrentAudioFile;
        }

        /// <summary>
        /// Gets or sets the current audio state image (play or pause image).
        /// </summary>
        public string CurrentAudioStateImg
        {
            get => currentAudioStateImg;
            set
            {
                if (value != currentAudioStateImg)
                {
                    currentAudioStateImg = value;
                    OnPropertyChanged(nameof(CurrentAudioStateImg));
                }
            }
        }

        private void UpdateAudioStateImg()
        {
            // Update image for button state
            if (audioPlayerService.IsPlaying)
            {
                CurrentAudioStateImg = "PauseButton.png";
            }
            else if (!audioPlayerService.IsPlaying)
            {
                CurrentAudioStateImg = "PlayButton.png";
            }
            else
            {
                CurrentAudioStateImg = null;
            }
        }

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of changing the current audio playback state.</placeholder></returns>
        public async Task ChangeAudioState()
        {
            await audioPlayerService.ChangeCurrentAudioState();
            UpdateAudioStateImg();
        }

        private void audioPlayerService_CurrentAudioFileChanged(object sender, EventArgs e)
        {
            // Update audio file to currently playing audio file
            OnPropertyChanged(nameof(AudioFile));
            UpdateAudioStateImg();
        }
    }
}
