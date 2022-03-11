namespace MyAudio.Services
{
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Utilities;
    using SQLite;

    /// <summary>
    /// Database access for the application.
    /// </summary>
    internal class MyAudioDatabase : IMyAudioDataAccess
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MyAudioDatabase"/> class.
        /// Opens SQLite database connection.
        /// </summary>
        public MyAudioDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
            Database.CreateTableAsync<Playlist>().Wait();
            Database.CreateTableAsync<AudioFile>().Wait();
            Database.CreateTableAsync<AudioFilePlaylist>().Wait();
        }

        /// <summary>
        /// Gets or sets database instance.
        /// </summary>
        private static SQLiteAsyncConnection Database { get; set; }

        /// <summary>
        /// Gets all of the audio files.
        /// </summary>
        /// <returns>List of all audio files.</returns>
        public Task<List<AudioFile>> GetAudioFilesAsync()
        {
            return Database.Table<AudioFile>().ToListAsync();
        }

        /// <summary>
        /// Gets a specific audio file.
        /// </summary>
        /// <param name="id">The ID of the audio file to get.</param>
        /// <returns>The audio file.</returns>
        public Task<AudioFile> GetAudioFileAsync(int id)
        {
            return Database.Table<AudioFile>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        /// <summary>
        /// Inserts or updates an audio file.
        /// </summary>
        /// <param name="audioFile">Audio file to add.</param>
        /// <returns>The number of rows added/updated.</returns>
        public Task<int> SaveAudioFileAsync(IAudioFile audioFile)
        {
            if (audioFile.ID != 0)
            {
                return Database.UpdateAsync(audioFile);
            }
            else
            {
                return Database.InsertAsync(audioFile);
            }
        }

        /// <summary>
        /// Deletes an audio file.
        /// </summary>
        /// <param name="audioFile">The audio file to delete.</param>
        /// <returns>The number of rows deleted.</returns>
        public Task<int> DeleteAudioFileAsync(IAudioFile audioFile)
        {
            return Database.DeleteAsync(audioFile);
        }

        /// <summary>
        /// Gets all the playlists.
        /// </summary>
        /// <returns>A collection of the existing playists.</returns>
        public Task<List<Playlist>> GetPlaylists()
        {
            return Database.Table<Playlist>().ToListAsync();
        }

        /// <summary>
        /// Gets a specific playlist.
        /// </summary>
        /// <param name="id">The ID of the playlist to get.</param>
        /// <returns>The playlist.</returns>
        public Task<Playlist> GetPlaylistAsync(int id)
        {
            return Database.Table<Playlist>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveAudioFilePlaylistAsync(IAudioFilePlaylist afp)
        {
            if (afp.ID != 0)
            {
                return Database.UpdateAsync(afp);
            }
            else
            {
                return Database.InsertAsync(afp);
            }
        }

        public Task<int> SavePlaylistAsync(IPlaylist playlist)
        {
            if (playlist.ID != 0)
            {
                return Database.UpdateAsync(playlist);
            }
            else
            {
                return Database.InsertAsync(playlist);
            }
        }

        /// <summary>
        /// Gets the audio file IDs of of audio files in given playlist.
        /// </summary>
        /// <param name="playlistID">ID of the playlist.</param>
        /// <returns>List of audio file IDs that are in the playlist.</returns>
        public async Task<List<int>> GetAudioFileIDsInPlaylist(int playlistID)
        {
            List<AudioFilePlaylist> rows = await Database.Table<AudioFilePlaylist>()
                                                    .Where(afp => afp.PlaylistID == playlistID)
                                                    .ToListAsync();

            return rows.Select(afp => afp.AudioFileID).ToList();
        }

        /// <summary>
        /// Gets all of the audio files which are in the playlist.
        /// </summary>
        /// <param name="playlist">The playlist to get the audio files for.</param>
        /// <returns>List of the audio files in playlist.</returns>
        public async Task<List<AudioFile>> GetPlaylistAudioFilesAsync(IPlaylist playlist)
        {
            List<AudioFile> audioFiles = new List<AudioFile>();

            List<int> audioFileIDs = await GetAudioFileIDsInPlaylist(playlist.ID);
            audioFileIDs.ForEach(async audioFileID => audioFiles.Add(await GetAudioFileAsync(audioFileID)));
            return audioFiles;
        }
    }
}
