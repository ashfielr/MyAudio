namespace MyAudio.Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;
    using MyAudio.Interfaces;

    /// <summary>
    /// AudioFilePlaylist table to link audio files to their playlists.
    /// </summary>
    internal class AudioFilePlaylist : IAudioFilePlaylist
    {
        public AudioFilePlaylist()
        {
        }

        public AudioFilePlaylist(int audioFileID, int playlistID)
        {
            AudioFileID = audioFileID;
            PlaylistID = playlistID;
        }

        public int ID { get; set; }

        public int AudioFileID { get; set; }

        public int PlaylistID { get; set; }
    }
}
