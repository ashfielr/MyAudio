namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Utilities;
    using Xamarin.Forms;

    public class MainShellViewModel
    {
        public MainShellViewModel()
        {
            LogoutCommand = new Command(async () => await Logout());
        }

        public ICommand LogoutCommand { get; set; }

        private async Task Logout()
        {
            AppData.Auth.SignOut();
            Application.Current.MainPage = new AppShell();
        }
    }
}
