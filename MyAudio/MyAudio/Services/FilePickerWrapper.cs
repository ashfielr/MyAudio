using MyAudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyAudio.Services
{
    public class FilePickerWrapper : IFilePicker
    {
        public Task<FileResult> PickAsync(PickOptions options = null)
        {
            return FilePicker.PickAsync(options);
        }
    }
}
