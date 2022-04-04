using Foundation;
using MyAudio.iOS;
using MyAudio.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UIKit;
using Xamarin.Forms;
using Firebase.Auth;

[assembly: Dependency(typeof(AuthiOS))]
namespace MyAudio.iOS
{
    // reference -  https://www.youtube.com/watch?v=7LPIOttwMXE
    public class AuthiOS : IAuth
    {
        public string GetCurrentLoggedInUserID()
        {
            return Auth.DefaultInstance.CurrentUser.Uid;
        }

        public bool IsSignedIn()
        {
            var user = Auth.DefaultInstance.CurrentUser;
            return user != null;
        }

        public async Task<string> LoginViaEmailPassword(string email, string password)
        {
            try
            {
                var user = await Auth.DefaultInstance.SignInWithPasswordAsync(email, password);
                return await user.User.GetIdTokenAsync();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }

        public bool SignOut()
        {
            try
            {
                _ = Auth.DefaultInstance.SignOut(out NSError error);
                return error == null;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<string> SignupViaEmailPassword(string email, string password)
        {
            try
            {
                var newUser = await Auth.DefaultInstance.CreateUserAsync(email, password);
                return await newUser.User.GetIdTokenAsync();
            }
            catch (Exception)
            {
                return string.Empty;
            }
        }
    }
}