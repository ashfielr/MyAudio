namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Id3;
    using Microsoft.Extensions.DependencyInjection;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// The view model for an AudioFilesPage View />.
    /// </summary>
    internal class AudioFilesPageViewModel : BaseViewModel
    {
        private IMyAudioDataAccess dataAccess;
        private IFileService fileService;
        private IAudioPlayerService audioPlayerService;
        private ObservableCollection<AudioFile> audioFiles;
        private AudioFile selectedAudioFile;
        private string currentAudioStateImg;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFilesPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">The data access for the application.</param>
        /// <param name="_fileImageService">Service which allows an image to be saved.</param>
        /// <param name="_audioPlayerService">Service which allows playback of audio files.</param>
        public AudioFilesPageViewModel(IMyAudioDataAccess _dataAccess, IFileService _fileImageService, IAudioPlayerService _audioPlayerService)
        {
            dataAccess = _dataAccess;
            fileService = _fileImageService;
            audioPlayerService = _audioPlayerService;
            UploadAudioFileCommand = new Command(async () => await UploadAudioFile());
            PlayAudioFileCommand = new Command(async () => await PlayAudioFile());
            ChangeAudioStateCommand = new Command(async () => await ChangeAudioState());
        }

        /// <summary>
        /// Gets or sets the collection of audio files which implement <see cref="IAudioFile"/>.
        /// </summary>
        public ObservableCollection<AudioFile> AudioFiles
        {
            get => audioFiles;
            set
            {
                audioFiles = value;
                OnPropertyChanged(nameof(AudioFiles));
            }
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
        /// Gets or sets the currently selected audio file.
        /// </summary>
        public AudioFile SelectedAudioFile
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
        /// Gets or sets command to allow user to upload an audio file.
        /// </summary>
        public ICommand UploadAudioFileCommand { get; set; }

        /// <summary>
        /// Gets or sets command to play an audio file.
        /// </summary>
        public ICommand PlayAudioFileCommand { get; set; }

        /// <summary>
        /// Gets or sets command to change audio state (playing or paused).
        /// </summary>
        public ICommand ChangeAudioStateCommand { get; set; }

        /// <summary>
        /// Overrided initialise method for the <see cref="AudioFilesPageViewModel"/> view model.
        /// </summary>
        /// <returns>The initialisation task.</returns>
        public override async Task Initialise()
        {
            var currentAudioFiles = await this.dataAccess.GetAudioFilesAsync();
            this.AudioFiles = new ObservableCollection<AudioFile>();
            if (currentAudioFiles.Count > 0)
            {
                currentAudioFiles.ForEach(audioFile => this.AudioFiles.Add(audioFile));
            }
        }

        /// <summary>
        /// Plays the selected audio file.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of playing an audio file.</placeholder></returns>
        public async Task PlayAudioFile()
        {
            await audioPlayerService.Play(SelectedAudioFile.FilePath);
            UpdateAudioStateImg();
        }

        /// <summary>
        /// Opens file picker for user to select an audio file.
        /// </summary>
        /// <param name="options">The file picker options.</param>
        /// <returns>The task result.</returns>
        public async Task<FileResult> UploadAudioFile()
        {
            PickOptions options = new PickOptions
            {
                PickerTitle = "Please select an audio file.",
                // FileTypes = customFileType,
            };
            try
            {
                var result = await FilePicker.PickAsync(options);
                string timestamp = DateTime.Now.Ticks.ToString();
                string audioFilePath = fileService.CopyMp3(result.FullPath, timestamp);
                if (result != null)
                {
                    if (result.FileName.EndsWith("mp3", StringComparison.OrdinalIgnoreCase))
                    {
                        using (var mp3 = new Mp3(result.FullPath))
                        {
                            Id3Tag tag = mp3.GetTag(Id3TagFamily.Version2X);
                            List<Id3Version> id3v = new List<Id3Version>(mp3.AvailableTagVersions);
                            byte[] image = tag.Pictures[0].PictureData;

                            string location = "AudioFiles";
                            string imageFilePath = fileService.SaveImage(timestamp, image, location);
                            AudioFile audioFile = new AudioFile(tag.Title, tag.Artists.ToString(), tag.Album, (int)mp3.Audio.Duration.TotalMilliseconds, imageFilePath, audioFilePath);
                            await dataAccess.SaveAudioFileAsync(audioFile);
                            this.AudioFiles.Add(audioFile);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                Debug.Fail(ex.Message); // The user canceled or something went wrong
            }

            return null;
        }

        /// <summary>
        /// Switches the audio state (playing or paused).
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of changing the current audio playback state.</placeholder></returns>
        private async Task ChangeAudioState()
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
            else
            {
                CurrentAudioStateImg = "PlayButton.png";
            }
        }
    }
}
