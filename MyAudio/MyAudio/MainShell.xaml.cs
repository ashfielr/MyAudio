namespace MyAudio
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.Views;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class MainShell : Shell
    {
        public MainShell()
        {
            InitializeComponent();
            Routing.RegisterRoute("Login", typeof(LoginPage));
            Routing.RegisterRoute("SignUp", typeof(SignUpPage));
        }
    }
}