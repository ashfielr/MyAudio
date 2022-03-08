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

    /// <summary>
    /// View model for the AddPlaylistPage.
    /// </summary>
    internal class AddPlaylistPageViewModel : BaseViewModel
    {
        private ObservableCollection<PlaylistAudioFile> playlistAudioFile;
        private IMyAudioDataAccess dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPlaylistPageViewModel"/> class.
        /// </summary>
        public AddPlaylistPageViewModel()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="AddPlaylistPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">The data access for application.</param>
        public AddPlaylistPageViewModel(IMyAudioDataAccess _dataAccess)
        {
            this.dataAccess = _dataAccess;
            this.CreatePlaylistCommand = new Command(async () => await CreatePlaylist());
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
        /// Gets or sets the command to create a new playlist with the audio files selected.
        /// </summary>
        public ICommand CreatePlaylistCommand { get; set; }

        /// <summary>
        /// Function which deals with initialising the view model.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of initialising the view model.</placeholder></returns>
        public async override Task Initialise()
        {
            var currentAudioFiles = await this.dataAccess.GetAudioFilesAsync();
            this.PlaylistAudioFiles = new ObservableCollection<PlaylistAudioFile>();
            if (currentAudioFiles.Count > 0)
            {
                currentAudioFiles.ForEach(audioFile => this.PlaylistAudioFiles.Add(new PlaylistAudioFile(audioFile, false)));
            }
        }

        private Task<int> CreatePlaylist()
        {
            Playlist playlist = new Playlist();
            playlist.Title = this.PlaylistName;
            List<int> audioFileIDs = GetSelectedAudioFileIDs();
            foreach (int audioFileID in audioFileIDs)
            {
               this.dataAccess.SaveAudioFilePlaylistAsync(new AudioFilePlaylist(audioFileID, playlist.ID));
            }

            playlist.NumOfAudioFiles = audioFileIDs.Count;
            playlist.TotalDuration = GetPlaylistDuration();
            return this.dataAccess.SavePlaylistAsync(playlist);
        }

        private int GetPlaylistDuration()
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

        private List<int> GetSelectedAudioFileIDs()
        {
            List<int> selectedAudioFiles = new List<int>();
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
        /// Struct to encapsulate an <see cref="AudioFile"/> instance with a boolean value indicatin whether to add it to the playlist.
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
