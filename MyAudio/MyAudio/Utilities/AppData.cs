namespace MyAudio.Utilities
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MyAudio.Services;
    using Xamarin.Forms;

    public static class AppData
    {
        public static string UserID { get; set; }

        public static IAuth Auth { get; set; } = DependencyService.Get<IAuth>();
    }
}
