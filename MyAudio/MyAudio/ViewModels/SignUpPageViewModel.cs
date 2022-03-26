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
        private string email;
        private string password;

        public SignUpPageViewModel()
        {
            SignUpCommand = new Command(async () => await SignUp(), () => SignUpCanExecute());
        }

        public Command SignUpCommand { get; set; }

        public override string Email
        {
            get => email;
            set
            {
                if (value != null)
                {
                    email = value;
                    SignUpCommand.ChangeCanExecute();
                    OnPropertyChanged(nameof(Email));
                }
            }
        }

        public override string Password
        {
            get => password;
            set
            {
                if (value != null)
                {
                    password = value;
                    SignUpCommand.ChangeCanExecute();
                    OnPropertyChanged(nameof(Password));
                }
            }
        }

        /// <summary>
        /// A user can only sign up if they have entered an email and password.
        /// </summary>
        /// <returns>A bool indicating if email and password are not null or empty.</returns>
        private bool SignUpCanExecute()
        {
            return !string.IsNullOrEmpty(Email) && !string.IsNullOrEmpty(Password);
        }

        private async Task SignUp()
        {
            try
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
                    await Shell.Current.DisplayAlert("ERROR", $"An error occurred during the sign up process.{Environment.NewLine}{Environment.NewLine}Please try again.", "Ok");
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("ERROR", $"{ex.Message}{Environment.NewLine}{Environment.NewLine}Please try again.", "Ok");
            }
        }
    }
}
