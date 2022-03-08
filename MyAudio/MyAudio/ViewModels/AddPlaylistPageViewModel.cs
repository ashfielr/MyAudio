namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Models;

    /// <summary>
    /// View model for the AddPlaylistPage.
    /// </summary>
    internal class AddPlaylistPageViewModel : BaseViewModel
    {
        private ObservableCollection<AudioFile> audioFiles;
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
        /// Function which deals with initialising the view model.
        /// </summary>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation of initialising the view model.</placeholder></returns>
        public async override Task Initialise()
        {
            var currentAudioFiles = await this.dataAccess.GetAudioFilesAsync();
            this.AudioFiles = new ObservableCollection<AudioFile>();
            if (currentAudioFiles.Count > 0)
            {
                currentAudioFiles.ForEach(audioFile => this.AudioFiles.Add(audioFile));
            }
        }
    }
}
