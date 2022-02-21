namespace MyAudio.Views
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Microsoft.Extensions.DependencyInjection;
    using MyAudio.ViewModels;
    using Xamarin.Forms;

    /// <summary>
    /// Class for the Main page.
    /// </summary>
    public partial class AudioFilesPage : ContentPage
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="AudioFilesPage"/> class.
        /// </summary>
        public AudioFilesPage()
        {
            this.InitializeComponent();
            this.BindingContext = IocProvider.ServiceProvider.GetService<AudioFilesPageViewModel>();
            this.SubscribeToEvents();
        }

        private void SubscribeToEvents()
        {
            this.Appearing += this.AudioFilesPage_Appearing;
        }

        private async void AudioFilesPage_Appearing(object sender, EventArgs e)
        {
            try
            {
                await (this.BindingContext as AudioFilesPageViewModel).Initialise();
            }
            catch (Exception error)
            {
                Debug.Fail(error.Message); // handle gracefully here
            }
        }
    }
}
