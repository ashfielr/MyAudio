namespace MyAudio.Services
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using Google.Cloud.Firestore;
    using MyAudio.Interfaces;
    using MyAudio.Models;
    using MyAudio.Utilities;

    public class FirestoreDbAccess : IMyAudioDataAccess
    {
        private FirestoreDb db;

        public FirestoreDbAccess()
        {
            FirestoreDb db = FirestoreDb.Create(Constants.FirebaseProjectID);
        }

        public async Task<AudioFile> GetAudioFileAsync(string id)
        {
            DocumentReference docRef = db.Collection("audioFiles").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                AudioFile audioFile = snapshot.ConvertTo<AudioFile>();
                return audioFile;
            }

            return null;
        }

        public async Task<List<AudioFile>> GetAudioFilesAsync()
        {
            CollectionReference collRef = db.Collection("audioFiles");
            QuerySnapshot snapshot = await collRef.GetSnapshotAsync();
            List<DocumentSnapshot> snapshotDocuments = snapshot.Documents.ToList<DocumentSnapshot>();
            if (snapshotDocuments.Count > 0)
            {
                List<AudioFile> audioFiles = new List<AudioFile>();
                snapshotDocuments.ForEach(snapshotDoc => audioFiles.Add(snapshotDoc.ConvertTo<AudioFile>()));
                return audioFiles;
            }

            return null;
        }

        public async Task<List<AudioFile>> GetAudioFilesInPlaylistAsync(IPlaylist playlist)
        {
            List<AudioFile> audioFiles = new List<AudioFile>();
            if (playlist.AudioFileIDs != null)
            {
                playlist.AudioFileIDs.ForEach(async audioFileID => audioFiles.Add(await GetAudioFileAsync(audioFileID)));
                return audioFiles;
            }

            return null;
        }

        public async Task<Playlist> GetPlaylistAsync(string id)
        {
            DocumentReference docRef = db.Collection("playlists").Document(id);
            DocumentSnapshot snapshot = await docRef.GetSnapshotAsync();
            if (snapshot.Exists)
            {
                Playlist playlist = snapshot.ConvertTo<Playlist>();
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
            CollectionReference collRef = db.Collection("playlists");
            QuerySnapshot snapshot = await collRef.GetSnapshotAsync();
            List<DocumentSnapshot> snapshotDocuments = snapshot.Documents.ToList<DocumentSnapshot>();
            if (snapshotDocuments.Count > 0)
            {
                List<Playlist> playlists = new List<Playlist>();
                snapshotDocuments.ForEach(snapshotDoc => playlists.Add(snapshotDoc.ConvertTo<Playlist>()));
                return playlists;
            }

            return null;
        }

        public async Task<bool> SaveAudioFileAsync(IAudioFile audioFile)
        {
            try
            {
                CollectionReference collRef = db.Collection("audioFiles");
                await collRef.AddAsync(audioFile);
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
                DocumentReference docRef = db.Collection("playlists").Document(afp.PlaylistID.ToString());
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
                CollectionReference collRef = db.Collection("playlists");
                await collRef.AddAsync(playlist);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
