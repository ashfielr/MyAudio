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

    public class SignUpPageViewModel : EmailPasswordViewModel
    {
        private IAuth auth;

        public SignUpPageViewModel()
        {
            auth = DependencyService.Get<IAuth>();
            SignUpCommand = new Command(async () => await SignUp());
        }

        public ICommand SignUpCommand { get; set; }

        private async Task SignUp()
        {
            var user = await auth.SignupViaEmailPassword(Email, Password);
            if (user != null)
            {
                var signOut = auth.SignOut();
                if (signOut)
                {
                    //await Shell.Current.DisplayAlert("Success", "You're now signed up.", "Ok");
                    Application.Current.MainPage = new LoginPage();
                }
            }
            else
            {
                //await Shell.Current.DisplayAlert("ERROR", "An error occurred during the sign up process, please try again.", "Ok");
            }
        }
    }
}
