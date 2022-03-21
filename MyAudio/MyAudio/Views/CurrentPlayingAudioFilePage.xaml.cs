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
    public partial class CurrentPlayingAudioFilePage : ContentPage
    {
        public CurrentPlayingAudioFilePage()
        {
            InitializeComponent();
            this.BindingContext = IocProvider.ServiceProvider.GetService<ICurrentPlayingAudioFileViewModel>();
            this.SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            this.Appearing += this.CurrentPlayingAudioFilePage_Appearing;
        }

        private async void CurrentPlayingAudioFilePage_Appearing(object sender, EventArgs e)
        {
            try
            {
                CurrentPlayingAudioFileViewModel singleAudioFilePageViewModel = (CurrentPlayingAudioFileViewModel)BindingContext;
                singleAudioFilePageViewModel.Initialise();
            }
            catch (Exception error)
            {
                Debug.Fail(error.Message); // handle gracefully here
            }
        }
    }
}