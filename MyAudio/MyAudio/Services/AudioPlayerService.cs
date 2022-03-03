namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Text;
    using Plugin.SimpleAudioPlayer;

    /// <summary>
    /// Service which controls playback of audio files.
    /// </summary>
    internal class AudioPlayerService : IAudioPlayerService
    {
        /// <summary>
        /// Plays an audio file.
        /// </summary>
        /// <param name="filePath">The file path of audio file to playback.</param>
        public void Play(string filePath)
        {
            ISimpleAudioPlayer player = Plugin.SimpleAudioPlayer.CrossSimpleAudioPlayer.Current;
            FileStream audioSourceStream = File.Open(filePath, FileMode.Open);
            player.Load(audioSourceStream);
            player.Play();
        }
    }
}
