namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;

    public class EmailPasswordViewModel : BaseViewModel
    {

        public virtual string Email { get; set; }

        public virtual string Password { get; set; }

    }
}
