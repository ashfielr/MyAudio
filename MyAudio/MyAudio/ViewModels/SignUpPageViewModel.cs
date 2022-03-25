namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Services;
    using MyAudio.Utilities;
    using MyAudio.Views;
    using Xamarin.Forms;

    public class SignUpPageViewModel : EmailPasswordViewModel
    {
        public SignUpPageViewModel()
        {
            SignUpCommand = new Command(async () => await SignUp());
        }

        public ICommand SignUpCommand { get; set; }

        private async Task SignUp()
        {
            var user = await AppData.Auth.SignupViaEmailPassword(Email, Password);
            if (user != string.Empty)
            {
                var signOut = AppData.Auth.SignOut();
                if (signOut)
                {
                    await Shell.Current.DisplayAlert("Success", "You're now signed up.", "Ok");
                    await Shell.Current.GoToAsync("Login");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("ERROR", "An error occurred during the sign up process, please try again.", "Ok");
            }
        }
    }
}
