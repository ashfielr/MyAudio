namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.Utilities;
    using Plugin.FirebaseStorage;

    public class FirebaseStorageService : IFileService
    {
        /// <summary>
        /// Copys an mp3 file to the audio files folder.
        /// </summary>
        /// <param name="filePath">The file path of original mp3 file.</param>
        /// <param name="copyName">Name of copy mp3 file.</param>
        /// <returns>The file path of the copy.</returns>
        public async Task<string> CopyMp3(string filePath, string copyName)
        {
            FileStream fs = new FileStream(filePath, FileMode.Open);
            string userID = AppData.Auth.GetCurrentLoggedInUserID();
            var reference = CrossFirebaseStorage.Current.Instance.RootReference
                                        .Child("AudioFiles")
                                        .Child(userID)
                                        .Child($"{copyName}.mp3");
            await reference.PutStreamAsync(fs);
            var mp3DownloadURI = await reference.GetDownloadUrlAsync();
            return mp3DownloadURI.AbsoluteUri;
        }

        /// <summary>
        /// Saves an image to local app data.
        /// </summary>
        /// <param name="name">The file name of the image to save.</param>
        /// <param name="imageData">The image data in bytes.</param>
        /// <param name="location">Folder to save the image in.</param>
        /// <returns>The file path of the saved image.</returns>
        public async Task<string> SaveImage(string name, byte[] imageData, string location = "temp")
        {
            Stream fs = new MemoryStream(imageData);
            string userID = AppData.Auth.GetCurrentLoggedInUserID();
            var reference = CrossFirebaseStorage.Current.Instance.RootReference
                                        .Child("Images")
                                        .Child(AppData.Auth.GetCurrentLoggedInUserID())
                                        .Child($"{name}.jpg");
            await reference.PutStreamAsync(fs);
            var imageDownloadURI = await reference.GetDownloadUrlAsync();
            return imageDownloadURI.AbsoluteUri;
        }


    }
}
