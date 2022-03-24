namespace MyAudio.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Models;

    /// <summary>
    /// Interface to provide data access for my audio app.
    /// </summary>
    public interface IMyAudioDataAccess
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
        Task<AudioFile> GetAudioFileAsync(string id);

        /// <summary>
        /// Gets all the audio files in a playlist.
        /// </summary>
        /// <param name="playlist">The playlist to get audio files for..</param>
        /// <returns>List of audio files that are part of the playlist.</returns>
        Task<List<AudioFile>> GetAudioFilesInPlaylistAsync(IPlaylist playlist);

        /// <summary>
        /// Saves an audio file.
        /// </summary>
        /// <param name="audioFile">The audio file instance to save.</param>
        /// <returns>The number of rows saved/updated.</returns>
        Task<bool> SaveAudioFileAsync(IAudioFile audioFile);

        /// <summary>
        /// Gets all the existing playlists.
        /// </summary>
        /// <returns>An asynchronous task for returning the list of existing playlists.</returns>
        Task<List<Playlist>> GetPlaylists();

        /// <summary>
        /// Gets a specific playlist.
        /// </summary>
        /// <param name="id">The ID of the playlist to get.</param>
        /// <returns>The playlist.</returns>
        Task<Playlist> GetPlaylistAsync(string id);

        /// <summary>
        /// Saves an AudioFilePlaylist to the table.
        /// </summary>
        /// <param name="afp">AudioFilePlaylist to be saved.</param>
        /// <returns>The numer of rows edited/added as an asynchronous task.</placeholder></returns>
        Task<bool> SaveAudioFilePlaylistAsync(IAudioFilePlaylist afp);

        /// <summary>
        /// Saves a Playlist to the table.
        /// </summary>
        /// <param name="playlist">Playlist to be saved.</param>
        /// <returns>The numer of rows edited/added as an asynchronous task.</placeholder></returns>
        Task<bool> SavePlaylistAsync(IPlaylist playlist);
    }
}
