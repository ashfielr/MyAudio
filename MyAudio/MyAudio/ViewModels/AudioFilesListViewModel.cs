namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using Xamarin.Forms;

    public class AudioFilesListViewModel : BaseViewModel
    {
        private ObservableCollection<AudioFileViewModel> audioFiles;
        private AudioFileViewModel selectedAudioFile;

        public AudioFilesListViewModel()
        {
            PlayAudioFileCommand = new Command(async () => await PlayAudioFile());
        }

        public ObservableCollection<AudioFileViewModel> AudioFiles
        {
            get => audioFiles;
            set
            {
                audioFiles = value;
                OnPropertyChanged(nameof(AudioFiles));
            }
        }

        /// <summary>
        /// Gets or sets the currently selected audio file.
        /// </summary>
        public AudioFileViewModel SelectedAudioFile
        {
            get => selectedAudioFile;
            set
            {
                if (value != selectedAudioFile)
                {
                    selectedAudioFile = value;
                    OnPropertyChanged(nameof(SelectedAudioFile));
                }
            }
        }

        /// <summary>
        /// Gets or sets command to play an audio file.
        /// </summary>
        public ICommand PlayAudioFileCommand { get; set; }

        public override Task Initialise()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Plays the selected audio file.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file.</placeholder></returns>
        public async Task PlayAudioFile()
        {
            await SelectedAudioFile.Play();
        }
    }
}
