namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Interface allowing images to be saved.
    /// </summary>
    public interface IFileService
    {
        /// <summary>
        /// Saves an image.
        /// </summary>
        /// <param name="name">The name of the image.</param>
        /// <param name="imageData">Image data in bytes.</param>
        /// <param name="location">Location to save the image.</param>
        /// <returns>The file path of the saved image.</returns>
        string SaveImage(string name, byte[] imageData, string location = "temp");

        /// <summary>
        /// Copys an mp3 file to the audio files folder.
        /// </summary>
        /// <param name="filePath">The file path of original mp3 file.</param>
        /// <param name="copyName">Name of copy mp3 file.</param>
        /// <returns>The file path of the copy.</returns>
        string CopyMp3(string filePath, string copyName);
    }
}
