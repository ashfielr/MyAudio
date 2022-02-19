namespace MyAudio.Models
{
    using MyAudio.Interfaces;
    using SQLite;

    internal class AudioFile : IAudioFile
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set ; }
        public string Artist { get; set; }
        public string AlbumName { get; set; }
        public int Duration { get; set; }
        public string Image { get; set; }
    }
}
