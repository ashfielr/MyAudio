namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
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

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFilesPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">The data access for the application.</param>
        /// /// <param name="_fileImageService">Service which allows an image to be saved.</param>
        public AudioFilesPageViewModel(IMyAudioDataAccess _dataAccess, IFileService _fileImageService, IAudioPlayerService _audioPlayerService)
        {
            dataAccess = _dataAccess;
            fileService = _fileImageService;
            audioPlayerService = _audioPlayerService;
            UploadAudioFileCommand = new Command(async () => await UploadAudioFile());
            PlayAudioFileCommand = new Command<AudioFile>(audioFile => { PlayAudioFile(SelectedAudioFile); });
        }

        /// <summary>
        /// Gets or sets the collection of audio files which implement <see cref="IAudioFile"/>.
        /// </summary>
        public ObservableCollection<AudioFile> AudioFiles { get; set; } = new ObservableCollection<AudioFile>();

        public AudioFile SelectedAudioFile { get; set; }

        /// <summary>
        /// Gets or sets command to allow user to upload an audio file.
        /// </summary>
        public ICommand UploadAudioFileCommand { get; set; }

        public ICommand PlayAudioFileCommand { get; set; }

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

        public void PlayAudioFile(AudioFile audioFile)
        {
            audioPlayerService.Play(audioFile.FilePath);
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
                            this.AudioFiles.Add(audioFile);
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
