namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Services;
    using Xamarin.Forms;

    public class AudioFilesListViewModel : BaseViewModel
    {
        private ObservableCollection<AudioFileViewModel> audioFiles;
        private AudioFileViewModel selectedAudioFile;
        private IAudioPlayerService audioPlayerService;

        public AudioFilesListViewModel(IAudioPlayerService _audioPlayerService)
        {
            audioPlayerService = _audioPlayerService;
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
        public AudioFileViewModel SelectedAudioFileVM
        {
            get => selectedAudioFile;
            set
            {
                if (value != selectedAudioFile)
                {
                    selectedAudioFile = value;
                    OnPropertyChanged(nameof(SelectedAudioFileVM));
                }
            }
        }

        /// <summary>
        /// Gets or sets command to play an audio file.
        /// </summary>
        public ICommand PlayAudioFileCommand { get; set; }

        /// <summary>
        /// Plays the selected audio file.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file.</placeholder></returns>
        public async Task PlayAudioFile()
        {
            // Initialise the audio player queue
            List<string> audioFilePaths = GetListOfAudioFilePaths(AudioFiles);
            audioPlayerService.Queue = audioFilePaths;

            // Set the list of AudioFiles
            List<IAudioFile> audioFilesList = new List<IAudioFile>();
            AudioFiles.ToList().ForEach(audioFileVM => audioFilesList.Add(audioFileVM.AudioFile));
            audioPlayerService.AudioFilesList = audioFilesList;

            // Play selected audio file
            int audioFileIdx = AudioFiles.IndexOf(SelectedAudioFileVM);
            await SelectedAudioFileVM.Play(audioFileIdx);
        }

        private List<string> GetListOfAudioFilePaths(ObservableCollection<AudioFileViewModel> afvms)
        {
            List<string> audioFilePaths = new List<string>();
            afvms.ToList().ForEach(audioFileVM => audioFilePaths.Add(audioFileVM.FilePath));
            return audioFilePaths;
        }
    }
}
