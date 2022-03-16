namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using Xamarin.Forms;

    /// <summary>
    /// View model for the PlaylistDetailsPage view.
    /// </summary>
    [QueryProperty(nameof(PlaylistToShow), "PlaylistToShow")]
    internal class PlaylistDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {
        private IMyAudioDataAccess dataAccess;
        private ObservableCollection<AudioFile> playlistAudioFiles;
        private string playlistTitle;
        private Playlist playlistToShow;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistDetailsPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">App data access.</param>
        /// <param name="_playlist">The playlist to show details for.</param>
        public PlaylistDetailsPageViewModel(IMyAudioDataAccess _dataAccess)
        {
            this.dataAccess = _dataAccess;
        }

        /// <summary>
        /// Gets the playlist to show the details of.
        /// </summary>
        public Playlist PlaylistToShow
        {
            get => playlistToShow;
            private set
            {
                playlistToShow = value;
                OnPropertyChanged(nameof(PlaylistToShow));
            }
        }

        public string PlaylistTitle
        {
            get => playlistTitle;
            set
            {
                playlistTitle = value;
                OnPropertyChanged(nameof(PlaylistTitle));
            }
        }

        public ObservableCollection<AudioFile> PlaylistAudioFiles
        {
            get => playlistAudioFiles;
            set
            {
                playlistAudioFiles = value;
                OnPropertyChanged(nameof(PlaylistAudioFiles));
            }
        }

        private int playlistID;

        /// <summary>
        /// Gets the parameters from the query.
        /// </summary>
        /// <param name="query">Dictionary of the parameters passed to page.</param>
        public void ApplyQueryAttributes(IDictionary<string, string> query)
        {
            playlistID = int.Parse(HttpUtility.UrlDecode(query["playlistID"]));
        }

        public override async Task Initialise()
        {
            await LoadPlaylist(playlistID);
            var currentAudioFilesInPlaylist = await this.dataAccess.GetPlaylistAudioFilesAsync(PlaylistToShow);
            this.PlaylistAudioFiles = new ObservableCollection<AudioFile>();
            if (currentAudioFilesInPlaylist.Count > 0)
            {
                currentAudioFilesInPlaylist.ForEach(audioFile => this.PlaylistAudioFiles.Add(audioFile));
            }
        }

        private async Task LoadPlaylist(int playlistID)
        {
            try
            {
                List<Playlist> playlists = await this.dataAccess.GetPlaylists(); // Check the Playlists Table
                PlaylistToShow = await this.dataAccess.GetPlaylistAsync(playlistID);
                PlaylistTitle = PlaylistToShow.Title;
            }
            catch (Exception)
            {
                Debug.Fail("Failed to load playlist.");
            }
        }
    }
}
