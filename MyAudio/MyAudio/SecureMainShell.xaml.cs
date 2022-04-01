namespace MyAudio
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.ViewModels;
    using MyAudio.Views;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class SecureMainShell : Shell
    {
        public SecureMainShell()
        {
            InitializeComponent();
            BindingContext = IocProvider.ServiceProvider.GetService<MainShellViewModel>();
            
            Routing.RegisterRoute("Playlists/AddPlaylist", typeof(AddPlaylistPage));
            Routing.RegisterRoute("Playlists/PlaylistDetails", typeof(PlaylistDetailsPage));
            Routing.RegisterRoute("CurrentPlayingAudioFilePage", typeof(CurrentPlayingAudioFilePage));
        }

    }
}