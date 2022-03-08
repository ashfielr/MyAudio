namespace MyAudio.Interfaces
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    internal interface IAudioFilePlaylist
    {
        int ID { get; set; }

        int AudioFileID { get; set; }

        int PlaylistID { get; set; }
    }
}
