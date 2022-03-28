namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Services;
    using MyAudio.Utilities;
    using Xamarin.Forms;

    public class MainShellViewModel
    {
        private IAudioPlayerService audioPlayerService;

        public MainShellViewModel(IAudioPlayerService _audioPlayerService)
        {
            audioPlayerService = _audioPlayerService;
            LogoutCommand = new Command(async () => await Logout());
        }

        public ICommand LogoutCommand { get; set; }

        private async Task Logout()
        {
            await audioPlayerService.Dispose();
            AppData.Auth.SignOut();
            Application.Current.MainPage = new MainShell();
        }
    }
}
