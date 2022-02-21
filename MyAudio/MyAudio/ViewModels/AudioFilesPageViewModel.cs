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
    /// The view model for an AudioFilesPage View />.
    /// </summary>
    internal class AudioFilesPageViewModel : BaseViewModel
    {
        private IMyAudioDataAccess dataAccess;

        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFilesPageViewModel"/> class.
        /// </summary>
        /// <param name="dataAccess">The data access for the application.</param>
        public AudioFilesPageViewModel(IMyAudioDataAccess dataAccess)
        {
            this.dataAccess = dataAccess;
        }

        /// <summary>
        /// Gets or sets the collection of audio files which implement <see cref="IAudioFile"/>.
        /// </summary>
        public ObservableCollection<AudioFile> AudioFiles { get; set; } = new ObservableCollection<AudioFile>();

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
    }
}
