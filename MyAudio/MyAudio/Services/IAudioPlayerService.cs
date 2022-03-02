namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal interface IAudioPlayerService
    {
        void Play(string filePath);
    }
}
