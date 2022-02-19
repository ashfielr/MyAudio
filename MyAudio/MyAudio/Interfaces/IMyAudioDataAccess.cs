namespace MyAudio.Interfaces
{
    using System.Collections.Generic;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Models;

    internal interface IMyAudioDataAccess
    {
        Task<List<AudioFile>> GetAudioFilesAsync();
        Task<AudioFile> GetAudioFileAsync(int id);
        Task<int> SaveAudioFileAsync(IAudioFile audioFile);
        Task<int> DeleteAudioFileAsync(IAudioFile audioFile);
    }
}
