namespace MyAudio.Views
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
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
        }
    }
}