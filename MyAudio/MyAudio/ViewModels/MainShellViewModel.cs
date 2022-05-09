namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.AppShells;
    using MyAudio.Services;
    using MyAudio.Utilities;
    using Xamarin.Forms;

    public class MainShellViewModel : BaseViewModel
    {
        private IAudioPlayerService audioPlayerService;

        public MainShellViewModel(IAudioPlayerService _audioPlayerService)
        {
            audioPlayerService = _audioPlayerService;
            LogoutCommand = new Command(async () => await Logout());
            ChangeThemeCommand = new Command(() => ChangeTheme());
        }

        public ICommand LogoutCommand { get; set; }
        public ICommand ChangeThemeCommand { get; set; }

        public String SwitchThemeText { get; set; } = "Dark mode";

        private async Task Logout()
        {
            await audioPlayerService.Dispose();
            AppData.Auth.SignOut();
            Application.Current.MainPage = new MainShell();
        }

        private void ChangeTheme()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;
            Application.Current.UserAppTheme = currentTheme == OSAppTheme.Dark ? OSAppTheme.Light : OSAppTheme.Dark;

            SwitchThemeText = currentTheme == OSAppTheme.Dark ? "Dark mode" : "Light mode";
            OnPropertyChanged(nameof(SwitchThemeText));
        }

        public void Initialise()
        {
            OSAppTheme currentTheme = Application.Current.RequestedTheme;

            SwitchThemeText = currentTheme == OSAppTheme.Dark ? "Light mode" : "Dark mode";
            OnPropertyChanged(nameof(SwitchThemeText));
        }
    }
}
