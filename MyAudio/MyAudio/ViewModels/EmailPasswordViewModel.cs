namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class EmailPasswordViewModel : BaseViewModel
    {

        public string Email { get; set; }

        public string Password { get; set; }

        public override Task Initialise()
        {
            throw new NotImplementedException();
        }
    }
}
