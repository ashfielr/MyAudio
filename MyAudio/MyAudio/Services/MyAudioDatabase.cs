namespace MyAudio.Services
{
    using System.Collections.Generic;
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
        /// Gets or sets database instance.
        /// </summary>
        private static SQLiteAsyncConnection Database { get; set; }

        /// <summary>
        /// The singleton intance of database access.
        /// </summary>
        public static readonly AsyncLazy<MyAudioDatabase> Instance = new AsyncLazy<MyAudioDatabase>(async () =>
        {
            var instance = new MyAudioDatabase();
            CreateTableResult result = await Database.CreateTableAsync<AudioFile>();
            return instance;
        });

        /// <summary>
        /// Initializes a new instance of the <see cref="MyAudioDatabase"/> class.
        /// Opens SQLite database connection.
        /// </summary>
        public MyAudioDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

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
    }
}
