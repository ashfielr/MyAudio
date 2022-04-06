namespace MyAudio
{
    using System;
    using MyAudio.AppShells;
    using MyAudio.Utilities;
    using MyAudio.Views;
    using Xamarin.Forms;
    using Xamarin.Forms.Xaml;

    /// <summary>
    /// The MyAudio app.
    /// </summary>
    public partial class App : Application
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="App"/> class.
        /// Starts the app.
        /// </summary>
        public App()
        {
            Device.SetFlags(new string[] { "AppTheme_Experimental" });
            this.InitializeComponent();
            IocProvider.Init();
            if (AppData.Auth.IsSignedIn())
            {
                this.MainPage = new SecureMainShell();
            }
            else
            {
                this.MainPage = new MainShell();
            }
        }

        /// <summary>
        /// Function to be executed on app startup.
        /// </summary>
        protected override void OnStart()
        {
        }

        /// <summary>
        /// Function to be exected when user leaves app.
        /// </summary>
        protected override void OnSleep()
        {
        }

        /// <summary>
        /// Fuction to be executed when user returns to app.
        /// </summary>
        protected override void OnResume()
        {
        }
    }
}
