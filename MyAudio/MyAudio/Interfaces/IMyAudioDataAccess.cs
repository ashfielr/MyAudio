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
        /// <returns>An asynchronous task for returning the list of existing playlists.</returns>
        Task<List<Playlist>> GetPlaylists();

        /// <summary>
        /// Gets a specific playlist.
        /// </summary>
        /// <param name="id">The ID of the playlist to get.</param>
        /// <returns>The playlist.</returns>
        Task<Playlist> GetPlaylistAsync(int id);

        /// <summary>
        /// Saves an AudioFilePlaylist to the table.
        /// </summary>
        /// <param name="afp">AudioFilePlaylist to be saved.</param>
        /// <returns>The numer of rows edited/added as an asynchronous task.</placeholder></returns>
        Task<int> SaveAudioFilePlaylistAsync(IAudioFilePlaylist afp);

        /// <summary>
        /// Saves a Playlist to the table.
        /// </summary>
        /// <param name="playlist">Playlist to be saved.</param>
        /// <returns>The numer of rows edited/added as an asynchronous task.</placeholder></returns>
        Task<int> SavePlaylistAsync(IPlaylist playlist);

        /// <summary>
        /// Gets the audio file IDs of of audio files in given playlist.
        /// </summary>
        /// <param name="playlistID">ID of the playlist.</param>
        /// <returns>List of audio file IDs that are in the playlist.</returns>
        Task<List<int>> GetAudioFileIDsInPlaylist(int playlistID);

        /// <summary>
        /// Gets all of the audio files which are in the playlist.
        /// </summary>
        /// <param name="playlist">The playlist to get the audio files for.</param>
        /// <returns>List of the audio files in playlist.</returns>
        Task<List<AudioFile>> GetPlaylistAudioFilesAsync(IPlaylist playlist);
    }
}
