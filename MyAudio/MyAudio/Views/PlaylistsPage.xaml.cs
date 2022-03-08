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
    public partial class PlaylistsPage : ContentPage
    {
        public PlaylistsPage()
        {
            InitializeComponent();
            this.BindingContext = IocProvider.ServiceProvider.GetService<PlaylistsPageViewModel>();
            this.SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            this.Appearing += this.PlaylistsPage_Appearing;
        }

        private async void PlaylistsPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                await (BindingContext as PlaylistsPageViewModel).Initialise();
            }
            catch (Exception error)
            {
                Debug.Fail(error.Message); // handle gracefully here
            }
        }
    }
}