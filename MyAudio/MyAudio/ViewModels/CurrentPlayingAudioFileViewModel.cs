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

    public class CurrentPlayingAudioFileViewModel : AudioFileViewModel
    {

        private string currentAudioStateImg;

        public CurrentPlayingAudioFileViewModel(IAudioPlayerService _audioPlayerService)
            : base(_audioPlayerService.CurrentAudioFile, _audioPlayerService)
        {
            audioPlayerService.CurrentAudioFileChanged += audioPlayerService_CurrentAudioFileChanged;
            ChangeAudioStateCommand = new Command(async () => await ChangeAudioState());
            GoToSingleAudioPageCommand = new Command(async () => await GoToSingleAudioPage());
            PlayNextAudioFileCommand = new Command(async () => await PlayNextAudioFile());
            PlayPreviousAudioFileCommand = new Command(async () => await PlayPreviousAudioFile());
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

        public ICommand GoToSingleAudioPageCommand { get; set; }

        public ICommand PlayNextAudioFileCommand { get; set; } 

        public ICommand PlayPreviousAudioFileCommand { get; set; }

        private async Task GoToSingleAudioPage()
        {
            await Shell.Current.GoToAsync("CurrentPlayingAudioFilePage");
        }

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

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of changing the current audio playback state.</placeholder></returns>
        public async Task ChangeAudioState()
        {
            await audioPlayerService.ChangeCurrentAudioState();
            UpdateAudioStateImg();
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

        private void audioPlayerService_CurrentAudioFileChanged(object sender, EventArgs e)
        {
            // Update audio file to currently playing audio file
            OnPropertyChanged(nameof(AudioFile));
            UpdateAudioStateImg();
        }

        private async Task PlayNextAudioFile()
        {
            await audioPlayerService.PlayNext();
        }

        private async Task PlayPreviousAudioFile()
        {
            await audioPlayerService.PlayPrevious();
        }
    }
}
