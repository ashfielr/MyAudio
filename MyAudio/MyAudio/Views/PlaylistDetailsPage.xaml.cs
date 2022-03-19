namespace MyAudio.Views
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.ViewModels;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// A view to display a playlist and its audio files.
    /// </summary>
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class PlaylistDetailsPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PlaylistDetailsPage"/> class.
        /// </summary>
        public PlaylistDetailsPage()
        {
            InitializeComponent();
            this.BindingContext = IocProvider.ServiceProvider.GetService<PlaylistDetailsPageViewModel>();
            this.SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            this.Appearing += this.PlaylistDetailsPage_Appearing;
        }

        private async void PlaylistDetailsPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                PlaylistDetailsPageViewModel playlistDetailsPageViewModel = (PlaylistDetailsPageViewModel)BindingContext;
                await playlistDetailsPageViewModel.Initialise();
                playlistDetailsPageViewModel.CurrentPlayingAudioFileViewModel.Initialise();
            }
            catch (Exception error)
            {
                Debug.Fail(error.Message); // handle gracefully here
            }
        }
    }
}