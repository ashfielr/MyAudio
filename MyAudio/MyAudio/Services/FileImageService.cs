namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Service which provides functionality of saving an image.
    /// </summary>
    internal class FileImageService : IFileImageService
    {
        /// <summary>
        /// Saves an image to local app data.
        /// </summary>
        /// <param name="fileName">The file name of the image to save.</param>
        /// <param name="imageData">The image data in bytes.</param>
        /// <param name="location">Folder to save the image in.</param>
        /// <returns>The file path of the saved image.</returns>
        public string SaveImage(string fileName, byte[] imageData, string location = "temp")
        {
            // See https://stackoverflow.com/a/51040802
            var folderPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            folderPath = Path.Combine(folderPath, location);
            Directory.CreateDirectory(folderPath);

            string filePath = Path.Combine(folderPath, fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fs.Write(imageData, 0, imageData.Length);
            }

            return filePath;
        }
    }
}
