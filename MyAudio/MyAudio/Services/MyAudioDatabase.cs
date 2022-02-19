using SQLite;
using MyAudio.Utilities;
using System.Threading.Tasks;
using MyAudio.Models;
using MyAudio.Interfaces;
using System.Collections.Generic;

namespace MyAudio.Services
{
    internal class MyAudioDatabase : IMyAudioDataAccess
    {
        static SQLiteAsyncConnection Database;

        public static readonly AsyncLazy<MyAudioDatabase> Instance = new AsyncLazy<MyAudioDatabase>(async () =>
        {
            var instance = new MyAudioDatabase();
            CreateTableResult result = await Database.CreateTableAsync<AudioFile>();
            return instance;
        });

        public MyAudioDatabase()
        {
            Database = new SQLiteAsyncConnection(Constants.DatabasePath, Constants.Flags);
        }

        public Task<List<AudioFile>> GetAudioFilesAsync()
        {
            return Database.Table<AudioFile>().ToListAsync();
        }

        public Task<AudioFile> GetAudioFileAsync(int id)
        {
            return Database.Table<AudioFile>().Where(i => i.ID == id).FirstOrDefaultAsync();
        }

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

        public Task<int> DeleteAudioFileAsync(IAudioFile audioFile)
        {
            return Database.DeleteAsync(audioFile);
        }
    }
}
