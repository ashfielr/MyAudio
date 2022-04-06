using MyAudio.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyAudio.Services
{
    /// <summary>
    /// Class used to wrap the static class <see cref="FilePicker"/> inside a non-static class.
    /// </summary>
    public class FilePickerWrapper : IFilePicker
    {
        public Task<FileResult> PickAsync(PickOptions options = null)
        {
            return FilePicker.PickAsync(options);
        }
    }
}
