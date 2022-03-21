namespace MyAudio.ViewModels
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Threading.Tasks;
    using System.Windows.Input;
    using MyAudio.Interfaces;

    public interface ICurrentPlayingAudioFileViewModel
    {
        IAudioFile AudioFile { get; }

        ICommand ChangeAudioStateCommand { get; set; }

        string CurrentAudioStateImg { get; set; }

        Task ChangeAudioState();

    }
}
