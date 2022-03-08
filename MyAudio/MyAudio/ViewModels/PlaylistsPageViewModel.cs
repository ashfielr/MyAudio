﻿namespace MyAudio.ViewModels
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
    /// The view model for a PlaylistsPage View />.
    /// </summary>
    internal class PlaylistsPageViewModel : BaseViewModel
    {
        private IMyAudioDataAccess dataAccess;
        private ObservableCollection<Playlist> playlists;

        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistsPageViewModel"/> class.
        /// </summary>
        /// <param name="_dataAccess">The data access for the application.</param>
        public PlaylistsPageViewModel(IMyAudioDataAccess _dataAccess)
        {
            this.dataAccess = _dataAccess;
            this.GoToAddPlaylistPageCommand = new Command(async () => await GoToAddPlaylistPage());
        }

        /// <summary>
        /// Gets or sets the collection of playlists that exist.
        /// </summary>
        public ObservableCollection<Playlist> Playlists
        {
            get => playlists;
            set
            {
                playlists = value;
                OnPropertyChanged(nameof(Playlists));
            }
        }

        /// <summary>
        /// Gets or sets the command to go to the add playlist page.
        /// </summary>
        public ICommand GoToAddPlaylistPageCommand { get; set; }

        /// <summary>
        /// Initialises the view model for use with page.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of initialising the VM.</returns>
        public override async Task Initialise()
        {
            await Task.Delay(1);
            var currentPlaylists = this.dataAccess.GetPlaylists();
            this.Playlists = new ObservableCollection<Playlist>();
            if (currentPlaylists.Count > 0)
            {
                currentPlaylists.ForEach(playlist => this.Playlists.Add(playlist));
            }
        }

        private async Task GoToAddPlaylistPage()
        {
            await Shell.Current.GoToAsync("Playlists/AddPlaylist");
        }
    }
}