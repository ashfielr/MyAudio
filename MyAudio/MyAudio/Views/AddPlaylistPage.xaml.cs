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
    public partial class AddPlaylistPage : ContentPage
    {
        public AddPlaylistPage()
        {
            InitializeComponent();
            this.BindingContext = IocProvider.ServiceProvider.GetService<AddPlaylistPageViewModel>();
            this.SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            this.Appearing += this.AddPlaylistPage_Appearing;
        }

        private async void AddPlaylistPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                await (BindingContext as AddPlaylistPageViewModel).Initialise();
            }
            catch (Exception error)
            {
                Debug.Fail(error.Message); // handle gracefully here
            }
        }
    }
}