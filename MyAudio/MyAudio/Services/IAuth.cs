namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public interface IAuth
    {
        Task<string> LoginViaEmailPassword(string email, string password);

        Task<string> SignupViaEmailPassword(string email, string password);

        bool SignOut();

        bool IsSignedIn();
    }
}
