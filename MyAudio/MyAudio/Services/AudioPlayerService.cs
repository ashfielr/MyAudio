namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Plugin.SimpleAudioPlayer;

    internal class AudioPlayerService : IAudioPlayerService
    {
        public void Play(string filePath)
        {
            ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            player.Load(filePath);
            player.Play();
        }
    }
}
