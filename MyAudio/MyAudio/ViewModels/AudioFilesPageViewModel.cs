namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using Id3;
    using MediaManager;
    using Microsoft.Extensions.DependencyInjection;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// The view model for an AudioFilesPage View />.
    /// </summary>
    public class AudioFilesPageViewModel : BaseViewModel
    {
        private IMyAudioDataAccess dataAccess;
        private IFileService fileService;
        private IAudioPlayerService audioPlayerService;
        private IAudioFile selectedAudioFile;

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
            AudioFilesListViewModel = new AudioFilesListViewModel(_audioPlayerService);
            CurrentPlayingAudioFileViewModel = IocProvider.ServiceProvider.GetService<CurrentPlayingAudioFileViewModel>();
            UploadAudioFileCommand = new Command(async () => await UploadAudioFile());
        }

        public AudioFilesListViewModel AudioFilesListViewModel { get; set; }

        public CurrentPlayingAudioFileViewModel CurrentPlayingAudioFileViewModel { get; set; }

        /// <summary>
        /// Gets or sets command to allow user to upload an audio file.
        /// </summary>
        public ICommand UploadAudioFileCommand { get; set; }

        /// <summary>
        /// Overrided initialise method for the <see cref="AudioFilesPageViewModel"/> view model.
        /// </summary>
        /// <returns>The initialisation task.</returns>
        public async Task Initialise()
        {
            var currentAudioFiles = await this.dataAccess.GetAudioFilesAsync();
            this.AudioFilesListViewModel.AudioFiles = new ObservableCollection<AudioFileViewModel>();
            if (currentAudioFiles != null && currentAudioFiles.Count > 0)
            {
                currentAudioFiles.ForEach(audioFile => this.AudioFilesListViewModel.AudioFiles.Add(new AudioFileViewModel(audioFile, audioPlayerService)));
            }
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
                var result = await IocProvider.ServiceProvider.GetService<IFilePicker>().PickAsync(options);
                string timestamp = DateTime.Now.Ticks.ToString();
                string audioFilePath = await fileService.CopyMp3(result.FullPath, timestamp);
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
                            string imageFilePath = await fileService.SaveImage(timestamp, image, location);
                            AudioFile audioFile = new AudioFile(tag.Title, tag.Artists.ToString(), tag.Album, (int)mp3.Audio.Duration.TotalMilliseconds, imageFilePath, audioFilePath);
                            await dataAccess.SaveAudioFileAsync(audioFile);
                            this.AudioFilesListViewModel.AudioFiles.Add(new AudioFileViewModel(audioFile, audioPlayerService));
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
    }
}
