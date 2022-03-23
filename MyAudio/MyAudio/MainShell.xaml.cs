using MyAudio.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace MyAudio
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Playlists/AddPlaylist", typeof(AddPlaylistPage));
            Routing.RegisterRoute("Playlists/PlaylistDetails", typeof(PlaylistDetailsPage));
            Routing.RegisterRoute("CurrentPlayingAudioFilePage", typeof(CurrentPlayingAudioFilePage));
        }
    }
}