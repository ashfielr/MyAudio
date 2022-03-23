namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Services;
    using MyAudio.Views;
    using Xamarin.Forms;

    public class LoginPageViewModel : EmailPasswordViewModel
    {
        private IAuth auth;

        public LoginPageViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            LogInCommand = new Command(async () => await LogIn());
            GoToSignUpPageCommand = new Command(async () => await GoToSignUp());
        }

        public ICommand LogInCommand { get; set; }

        public ICommand GoToSignUpPageCommand { get; set; }

        private async Task LogIn()
        {
            var userToken = await auth.LoginViaEmailPassword(Email, Password);
            if (userToken != string.Empty)
            {
                Application.Current.MainPage = new MainShell();
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
