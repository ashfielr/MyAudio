
namespace MyAudio.Interfaces
{
    internal interface IAudioFile
    {
        int ID { get; set; }
        string Artist { get; set; }

        string AlbumName { get; set; }

        int Duration { get; set; }

        string Image { get; set; }
    }
}
