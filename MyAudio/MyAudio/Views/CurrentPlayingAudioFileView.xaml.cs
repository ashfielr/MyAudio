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

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class CurrentPlayingAudioFileView : ContentView
    {
        public CurrentPlayingAudioFileView()
        {
            InitializeComponent();
        }
    }
}