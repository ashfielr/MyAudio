namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;

    /// <summary>
    /// Service which provides functionality of saving an image.
    /// </summary>
    internal class FileService : IFileService
    {
        private string localDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

        /// <summary>
        /// Copys an mp3 file to the audio files folder.
        /// </summary>
        /// <param name="filePath">The file path of original mp3 file.</param>
        /// <param name="copyName">Name of copy mp3 file.</param>
        /// <returns>The file path of the copy.</returns>
        public string CopyMp3(string filePath, string copyName)
        {
            string copyFilePath = $"{this.localDataPath}/AudioFiles/{copyName}.mp3";
            File.Copy(filePath, copyFilePath);
            return copyFilePath;
        }

        /// <summary>
        /// Saves an image to local app data.
        /// </summary>
        /// <param name="fileName">The file name of the image to save.</param>
        /// <param name="imageData">The image data in bytes.</param>
        /// <param name="location">Folder to save the image in.</param>
        /// <returns>The file path of the saved image.</returns>
        public string SaveImage(string name, byte[] imageData, string location = "temp")
        {
            // See https://stackoverflow.com/a/51040802
            string folderPath = Path.Combine(this.localDataPath, location);
            Directory.CreateDirectory(folderPath);

            string fileName = $"{name}.jpg";
            string filePath = Path.Combine(folderPath, fileName);

            using (FileStream fs = new FileStream(filePath, FileMode.OpenOrCreate))
            {
                fs.Write(imageData, 0, imageData.Length);
            }

            return filePath;
        }
    }
}
