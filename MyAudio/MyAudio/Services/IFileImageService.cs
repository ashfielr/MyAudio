namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Interface allowing images to be saved.
    /// </summary>
    public interface IFileImageService
    {
        /// <summary>
        /// Saves an image.
        /// </summary>
        /// <param name="name">The name of the image.</param>
        /// <param name="imageData">Image data in bytes.</param>
        /// <param name="location">Location to save the image.</param>
        /// <returns>The file path of the saved image.</returns>
        string SaveImage(string name, byte[] imageData, string location = "temp");
    }
}
