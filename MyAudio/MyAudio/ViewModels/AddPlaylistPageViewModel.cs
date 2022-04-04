namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Services;
    using Xamarin.Essentials;
    using Xamarin.Forms;

    /// <summary>
    /// View model for the AddPlaylistPage.
    /// </summary>
    public class AddPlaylistPageViewModel : BaseViewModel
    {
        private IMyAudioDataAccess dataAccess;
        private IFileService fs;
        private ObservableCollection<PlaylistAudioFile> playlistAudioFile;
        private string playlistImgPath;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPlaylistPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">The data access for application.</param>
        public AddPlaylistPageViewModel(IMyAudioDataAccess _dataAccess, IFileService _fs)
        {
            this.dataAccess = _dataAccess;
            this.fs = _fs;
            this.CreatePlaylistCommand = new Command(async () =>
            {
                await CreatePlaylist();
                Shell.Current.SendBackButtonPressed();
            });
            this.CaptureImageCommand = new Command(async () => await CaptureImage());
        }

        /// <summary>
        /// Gets or sets the collection of audio files from which user selects ones to be added.
        /// </summary>
        public ObservableCollection<PlaylistAudioFile> PlaylistAudioFiles
        {
            get => playlistAudioFile;
            set
            {
                playlistAudioFile = value;
                OnPropertyChanged(nameof(PlaylistAudioFiles));
            }
        }

        /// <summary>
        /// Gets or sets the name to be given to the playlist being created.
        /// </summary>
        public string PlaylistName { get; set; }

        /// <summary>
        /// Gets or sets the playlist image path to be.
        /// </summary>
        public string PlaylistImgPath
        {
            get => playlistImgPath;
            set
            {
                if (playlistImgPath != value)
                {
                    playlistImgPath = value;
                    OnPropertyChanged(nameof(PlaylistImgPath));
                }
            }
        }

        /// <summary>
        /// Gets or sets the command to create a new playlist with the audio files selected.
        /// </summary>
        public ICommand CreatePlaylistCommand { get; set; }

        /// <summary>
        /// Gets or sets the command to take an image to be used as the playlist image.
        /// </summary>
        public ICommand CaptureImageCommand { get; set; }

        /// <summary>
        /// Function which deals with initialising the view model.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of initialising the view model.</placeholder></returns>
        public async Task Initialise()
        {
            var currentAudioFiles = await this.dataAccess.GetAudioFilesAsync();
            this.PlaylistAudioFiles = new ObservableCollection<PlaylistAudioFile>();
            if (currentAudioFiles.Count > 0)
            {
                currentAudioFiles.ForEach(audioFile => this.PlaylistAudioFiles.Add(new PlaylistAudioFile(audioFile, false)));
            }
        }

        private async Task<bool> CreatePlaylist()
        {
            Playlist playlist = GeneratePlaylistFromView();
            bool playlistCreated = await this.dataAccess.SavePlaylistAsync(playlist);  // Add playlist to database
            return playlistCreated;
        }

        private async Task CaptureImage()
        {
            var pic = await MediaPicker.CapturePhotoAsync();
            string imagePath = pic.FullPath;
            string timestamp = DateTime.Now.Ticks.ToString();

            // Extract byte data of image
            FileInfo fileInfo = new FileInfo(imagePath);
            Byte[] imgData = new byte[fileInfo.Length];
            using (FileStream fs  = fileInfo.OpenRead())
            {
                fs.Read(imgData, 0, imgData.Length);
            }
            fileInfo.Delete();

            // save image
            string savedImgPath = await this.fs.SaveImage(timestamp, imgData);
            this.PlaylistImgPath = savedImgPath;
        }

        public Playlist GeneratePlaylistFromView()
        {
            Playlist playlist = new Playlist();
            playlist.Title = this.PlaylistName;
            playlist.Image = PlaylistImgPath == null ? "clef_music_notes.png" : PlaylistImgPath;
            List<string> audioFileIDs = GetSelectedAudioFileIDs();
            playlist.AudioFileIDs = audioFileIDs;

            playlist.NumOfAudioFiles = audioFileIDs.Count;
            playlist.TotalDuration = GetPlaylistDuration();
            return playlist;
        }

        public int GetPlaylistDuration()
        {
            int totalDuration = 0;
            foreach (PlaylistAudioFile paf in PlaylistAudioFiles)
            {
                if (paf.IsSelected)
                {
                    totalDuration += paf.AudioFile.Duration;
                }
            }

            return totalDuration;
        }

        public List<string> GetSelectedAudioFileIDs()
        {
            List<string> selectedAudioFiles = new List<string>();
            foreach (PlaylistAudioFile paf in PlaylistAudioFiles)
            {
                if (paf.IsSelected)
                {
                    selectedAudioFiles.Add(paf.AudioFile.ID);
                }
            }

            return selectedAudioFiles;
        }

        /// <summary>
        /// Class to encapsulate an <see cref="AudioFile"/> instance with a boolean value indicatin whether to add it to the playlist.
        /// </summary>
        public class PlaylistAudioFile
        {
            private bool isSelected;

            public PlaylistAudioFile(AudioFile _audioFile, bool _isSelected = false)
            {
                AudioFile = _audioFile;
                isSelected = _isSelected;
            }

            /// <summary>
            /// Gets or sets <see cref="Models.AudioFile"/> instance to be considered to be added to playlist.
            /// </summary>
            public AudioFile AudioFile { get; set; }

            /// <summary>
            /// Gets or sets a value indicating whether the audio file has been selected to be added to the playlist.
            /// </summary>
            public bool IsSelected
            {
                get => isSelected;
                set
                {
                    if (isSelected != value)
                    {
                        isSelected = value;
                    }
                }
            }
        }
    }
}
