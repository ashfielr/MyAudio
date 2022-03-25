namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Utilities;
    using Plugin.CloudFirestore;  // https://github.com/f-miyu/Plugin.CloudFirestore

    public class FirestoreDbAccess : IMyAudioDataAccess
    {
        private IFirestore db;

        public FirestoreDbAccess()
        {
            db = CrossCloudFirestore.Current.Instance;
        }

        public async Task<AudioFile> GetAudioFileAsync(string id)
        {
            var docRef = db.Collection("userAudioFiles").Document(AppData.Auth.GetCurrentLoggedInUserID()).Collection("audioFiles").Document(id);
            var snapshot = await docRef.GetAsync();
            if (snapshot.Exists)
            {
                AudioFile audioFile = snapshot.ToObject<AudioFile>();
                return audioFile;
            }

            return null;
        }

        public async Task<List<AudioFile>> GetAudioFilesAsync()
        {
            var collectionRef = db.Collection("userAudioFiles").Document().Collection("audioFiles");
            var query = await collectionRef.GetAsync();
            List<AudioFile> audioFiles = query.ToObjects<AudioFile>().ToList<AudioFile>();
            if (audioFiles.Count > 0)
            {
                return audioFiles;
            }

            return null;
        }

        public async Task<List<AudioFile>> GetAudioFilesInPlaylistAsync(IPlaylist playlist)
        {
            List<AudioFile> audioFiles = new List<AudioFile>();
            if (playlist.AudioFileIDs != null)
            {
                foreach (var audioFileID in playlist.AudioFileIDs)
                {
                    AudioFile audioFile = await GetAudioFileAsync(audioFileID);
                    audioFiles.Add(audioFile);
                }

                return audioFiles;
            }

            return null;
        }

        public async Task<Playlist> GetPlaylistAsync(string id)
        {
            var docRef = db.Collection("userPlaylists").Document(AppData.Auth.GetCurrentLoggedInUserID()).Collection("playlists").Document(id);
            var snapshot = await docRef.GetAsync();
            if (snapshot.Exists)
            {
                Playlist playlist = snapshot.ToObject<Playlist>();
                return playlist;
            }

            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>Returns the list of playlists. Returns null if there are no playlists.</returns>
        public async Task<List<Playlist>> GetPlaylists()
        {
            var collectionRef = db.Collection("userPlaylists").Document(AppData.Auth.GetCurrentLoggedInUserID()).Collection("playlists");
            var query = await collectionRef.GetAsync();
            List<Playlist> playlists = query.ToObjects<Playlist>().ToList<Playlist>();
            if (playlists.Count > 0)
            {
                return playlists;
            }

            return null;
        }

        public async Task<bool> SaveAudioFileAsync(IAudioFile audioFile)
        {
            try
            {
                var collectionRef = db.Collection("userAudioFiles").Document(AppData.Auth.GetCurrentLoggedInUserID()).Collection("audioFiles");
                await collectionRef.AddAsync(audioFile);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SaveAudioFilePlaylistAsync(IAudioFilePlaylist afp)
        {
            try
            {
                // add the ID of audio file to playlist's list of audio file IDs
                var docRef = db.Collection("userPlaylists").Document(AppData.Auth.GetCurrentLoggedInUserID()).Collection("playlists").Document(afp.PlaylistID.ToString());
                await docRef.UpdateAsync("AudioFileIDs", FieldValue.ArrayUnion(afp.AudioFileID));
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<bool> SavePlaylistAsync(IPlaylist playlist)
        {
            try
            {
                var collectionRef = db.Collection("userPlaylists").Document(AppData.Auth.GetCurrentLoggedInUserID()).Collection("playlists");
                await collectionRef.AddAsync(playlist);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
