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
    using MyAudio.Views;
    using Xamarin.Forms;

    public class LoginPageViewModel : EmailPasswordViewModel
    {
        public LoginPageViewModel()
        {
            LogInCommand = new Command(async () => await LogIn());
            GoToSignUpPageCommand = new Command(async () => await GoToSignUp());
        }

        public ICommand LogInCommand { get; set; }

        public ICommand GoToSignUpPageCommand { get; set; }

        private async Task LogIn()
        {
            var userToken = await AppData.Auth.LoginViaEmailPassword(Email, Password);
            if (AppData.Auth.IsSignedIn())
            {
                Application.Current.MainPage = new SecureMainShell();
            }
            else
            {
                await Shell.Current.DisplayAlert("ERROR", "Invalid email and password combination.", "Ok");
            }
        }

        private async Task GoToSignUp()
        {
            await Shell.Current.GoToAsync("SignUp");
        }
    }
}
