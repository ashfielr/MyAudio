using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyAudio.Interfaces
{
    public  interface IFilePicker
    {
        Task<FileResult> PickAsync(PickOptions options = null);
    }
}
