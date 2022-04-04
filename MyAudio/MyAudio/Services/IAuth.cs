namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    // reference -  https://www.youtube.com/watch?v=7LPIOttwMXE
    public interface IAuth
    {
        string GetCurrentLoggedInUserID();

        Task<string> LoginViaEmailPassword(string email, string password);

        Task<string> SignupViaEmailPassword(string email, string password);

        bool SignOut();

        bool IsSignedIn();
    }
}
