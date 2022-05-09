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
            };
            try
            {
                var result = await IocProvider.ServiceProvider.GetService<IFilePicker>().PickAsync(options);
                string timestampStr = DateTime.Now.Ticks.ToString();
                string audioFilePath = await fileService.CopyMp3(result.FullPath, timestampStr);
                if (result != null)
                {
                    if (result.FileName.EndsWith("mp3", StringComparison.OrdinalIgnoreCase))
                    {
                        var audioFile = await GetID3AudioFileData(result, timestampStr);
                        audioFile.FilePath = audioFilePath;
                        await dataAccess.SaveAudioFileAsync(audioFile);
                        this.AudioFilesListViewModel.AudioFiles.Add(new AudioFileViewModel(audioFile, audioPlayerService));
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

        public async Task<AudioFile> GetID3AudioFileData(FileResult mp3FileResult, string timestampStr)
        {
            using (var mp3FileObj = new Mp3(mp3FileResult.FullPath))
            {
                Id3Tag tag = mp3FileObj.GetTag(Id3TagFamily.Version2X);
                List<Id3Version> id3v = new List<Id3Version>(mp3FileObj.AvailableTagVersions);

                string imageFilePath;
                if (tag != null && tag.Pictures.Count > 0)
                {
                    byte[] image = tag.Pictures[0].PictureData;
                    string location = "AudioFiles";
                    imageFilePath = await fileService.SaveImage(timestampStr, image, location);
                }
                else
                {
                    imageFilePath = "clef_music_notes.png";
                }

                AudioFile audioFile = new AudioFile();
                if (tag != null)
                {
                    audioFile.Title = string.IsNullOrEmpty(tag.Title) ? "Unknown" : tag.Title.ToString().Replace("\0", string.Empty);
                    audioFile.Artist = string.IsNullOrEmpty(tag.Artists) ? "Unknown" : tag.Artists.ToString().Replace("\0", string.Empty);
                    audioFile.AlbumName = string.IsNullOrEmpty(tag.Album) ? "Unknown" : tag.Album.ToString().Replace("\0", string.Empty);
                }
                else
                {
                    audioFile.Title = mp3FileResult.FileName;
                    audioFile.Artist = "Unknown";
                    audioFile.AlbumName = "Unknown";
                }
                audioFile.Duration = (int)mp3FileObj.Audio.Duration.TotalMilliseconds;
                audioFile.Image = imageFilePath;
                return audioFile;
            }
        }
    }
}
