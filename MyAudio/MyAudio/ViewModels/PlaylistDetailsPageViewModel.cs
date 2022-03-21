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
    using MyAudio.Services;
    using Xamarin.Forms;

    /// <summary>
    /// View model for the PlaylistDetailsPage view.
    /// </summary>
    [QueryProperty(nameof(PlaylistToShow), "PlaylistToShow")]
    internal class PlaylistDetailsPageViewModel : BaseViewModel, IQueryAttributable
    {
        private IMyAudioDataAccess dataAccess;
        private IAudioPlayerService audioPlayerService;
        private ObservableCollection<AudioFile> audioFiles;
        private string playlistTitle;
        private Playlist playlistToShow;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistDetailsPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">App data access.</param>
        /// <param name="_playlist">The playlist to show details for.</param>
        public PlaylistDetailsPageViewModel(IMyAudioDataAccess _dataAccess, IAudioPlayerService _audioPlayerService, ICurrentPlayingAudioFileViewModel cpafVM)
        {
            dataAccess = _dataAccess;
            audioPlayerService = _audioPlayerService;
            AudioFilesListViewModel = new AudioFilesListViewModel();
            CurrentPlayingAudioFileViewModel = (CurrentPlayingAudioFileViewModel)cpafVM;
        }

        public AudioFilesListViewModel AudioFilesListViewModel { get; set; }

        public CurrentPlayingAudioFileViewModel CurrentPlayingAudioFileViewModel { get; set;}

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
            this.AudioFilesListViewModel.AudioFiles = new ObservableCollection<AudioFileViewModel>();
            if (currentAudioFilesInPlaylist.Count > 0)
            {
                currentAudioFilesInPlaylist.ForEach(audioFile => this.AudioFilesListViewModel.AudioFiles.Add(new AudioFileViewModel(audioFile, audioPlayerService)));
            }
        }

        private async Task LoadPlaylist(int playlistID)
        {
            try
            {
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
