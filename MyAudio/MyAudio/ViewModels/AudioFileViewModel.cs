namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Services;

    public class AudioFileViewModel : BaseViewModel
    {
        protected IAudioFile audioFile;
        protected IAudioPlayerService audioPlayerService;

        public AudioFileViewModel(IAudioFile _audioFile, IAudioPlayerService _audioPlayerService)
        {
            audioFile = _audioFile;
            audioPlayerService = _audioPlayerService;
        }

        public string Title { get => audioFile.Title; }

        public string Artist { get => audioFile.Artist; }

        public string AlbumName { get => audioFile.AlbumName; }

        public int Duration { get => audioFile.Duration; }

        public string Image { get => audioFile.Image; }

        public string FilePath { get => audioFile.FilePath; }

        public override Task Initialise()
        {
            throw new NotImplementedException();
        }

        public async Task Play()
        {
            await audioPlayerService.Play(audioFile);
        }
    }
}
