namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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
        private IFileImageService fileImageService;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFilesPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">The data access for the application.</param>
        /// /// <param name="_fileImageService">Service which allows an image to be saved.</param>
        public AudioFilesPageViewModel(IMyAudioDataAccess _dataAccess, IFileImageService _fileImageService)
        {
            dataAccess = _dataAccess;
            fileImageService = _fileImageService;
            UploadAudioFileCommand = new Command(async () => await UploadAudioFile());
        }

        /// <summary>
        /// Gets or sets the collection of audio files which implement <see cref="IAudioFile"/>.
        /// </summary>
        public ObservableCollection<AudioFile> AudioFiles { get; set; } = new ObservableCollection<AudioFile>();

        /// <summary>
        /// Gets or sets command to allow user to upload an audio file.
        /// </summary>
        public ICommand UploadAudioFileCommand { get; set; }

        /// <summary>
        /// Overrided initialise method for the <see cref="AudioFilesPageViewModel"/> view model.
        /// </summary>
        /// <returns>The initialisation task.</returns>
        public override async Task Initialise()
        {
            var currentAudioFiles = await this.dataAccess.GetAudioFilesAsync();

            if (currentAudioFiles.Count > 0)
            {
                currentAudioFiles.ForEach(audioFile => this.AudioFiles.Add(audioFile));
            }
            else
            {
                this.AudioFiles = new ObservableCollection<AudioFile>();
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
                var result = await FilePicker.PickAsync(options);
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
                            string fileName = $"{DateTime.Now.Ticks}.jpg";
                            string filePath = fileImageService.SaveImage(fileName, image, location);
                            AudioFile audioFile = new AudioFile(tag.Title, tag.Artists.ToString(), tag.Album, 123, filePath);
                            AudioFiles.Add(audioFile);
                            await dataAccess.SaveAudioFileAsync(audioFile);
                        }
                    }
                }

                return result;
            }
            catch (Exception ex)
            {
                // The user canceled or something went wrong
            }

            return null;
        }
    }
}
