namespace MyAudio
{
    using System;
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
            this.InitializeComponent();
            IocProvider.Init();
            this.MainPage = new AppShell();
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
