namespace MyAudio.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Models;

    /// <summary>
    /// Interface to provide data access for my audio app.
    /// </summary>
    internal interface IMyAudioDataAccess
    {
        /// <summary>
        /// Gets all existing audio files.
        /// </summary>
        /// <returns>List of audio files.</returns>
        Task<List<AudioFile>> GetAudioFilesAsync();

        /// <summary>
        /// Retrieves an audio file based on <paramref name="id"/>.
        /// </summary>
        /// <param name="id">ID of audio file.</param>
        /// <returns>Instance of audio file.</returns>
        Task<AudioFile> GetAudioFileAsync(int id);

        /// <summary>
        /// Saves an audio file.
        /// </summary>
        /// <param name="audioFile">The audio file instance to save.</param>
        /// <returns>The number of rows saved/updated.</returns>
        Task<int> SaveAudioFileAsync(IAudioFile audioFile);

        /// <summary>
        /// Deletes an audio file.
        /// </summary>
        /// <param name="audioFile">The audio file to be deleted.</param>
        /// <returns>The number of rows deleted.</returns>
        Task<int> DeleteAudioFileAsync(IAudioFile audioFile);

        /// <summary>
        /// Gets all the existing playlists.
        /// </summary>
        /// <returns>The list of existing playlists.</returns>
        List<Playlist> GetPlaylists();
    }
}
