using Autofac.Extras.Moq;
using MediaManager.Library;
using Moq;
using MyAudio.Interfaces;
using MyAudio.Models;
using MyAudio.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms.Internals;
using System.Linq;
using static MyAudio.ViewModels.AddPlaylistPageViewModel;

namespace MyAudioTests.ViewModels
{
    public class AddPlaylistPageViewModelTests
    {
        [Test]
        public async Task Initialise_Should_Pass_When_Service_Returns_AudioFiles_From_DataAccess_Creating_PlaylistAudioFiles()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                new AudioFile("Title1","Artist1","AlbumName1",300,"imageURI","mp3URI"),
                new AudioFile("Title2","Artist2","AlbumName2",200,"imageURI","mp3URI"),
            });

            var viewModelToTest = mock.Create<AddPlaylistPageViewModel>();

            // act
            await viewModelToTest.Initialise();

            // assert
            Assert.IsTrue(viewModelToTest.PlaylistAudioFiles.Count == 2);
            foreach (var playlistAudioFile in viewModelToTest.PlaylistAudioFiles)
            {
                Assert.IsFalse(playlistAudioFile.IsSelected); // all the checkboxes will be unticked
            }
        }

        [Test]
        public async Task GetSelectedAudioFileIDs_Should_Pass_SelectedAudioFiles_Returns_EmptyList_If_None_Selected()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                new AudioFile("Title1","Artist1","AlbumName1",300,"imageURI","mp3URI"),
                new AudioFile("Title2","Artist2","AlbumName2",200,"imageURI","mp3URI"),
            });

            var viewModelToTest = mock.Create<AddPlaylistPageViewModel>();

            // act
            await viewModelToTest.Initialise();
            var selectedAudioFileIDs = viewModelToTest.GetSelectedAudioFileIDs();

            // assert
            Assert.AreEqual(selectedAudioFileIDs.Count, 0);
        }


        [Test]
        public async Task GetSelectedAudioFileIDs_Should_Pass_SelectedAudioFiles_Returns_CorrectListOfAudioFile_If_One_Selected()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                new AudioFile("Title1","Artist1","AlbumName1",300,"imageURI","mp3URI"),
                new AudioFile("Title2","Artist2","AlbumName2",200,"imageURI","mp3URI"),
            });

            var viewModelToTest = mock.Create<AddPlaylistPageViewModel>();

            // act
            await viewModelToTest.Initialise();
            viewModelToTest.PlaylistAudioFiles[0].IsSelected = true;
            var selectedAudioFileIDs = viewModelToTest.GetSelectedAudioFileIDs();

            // assert
            Assert.AreEqual(selectedAudioFileIDs.Count, 1);
        }

        [Test]
        public async Task GetSelectedAudioFileIDs_Should_Pass_SelectedAudioFiles_Returns_CorrectListOfAudioFiles_If_All_Selected()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                new AudioFile("Title1","Artist1","AlbumName1",300,"imageURI","mp3URI"),
                new AudioFile("Title2","Artist2","AlbumName2",200,"imageURI","mp3URI"),
            });

            var viewModelToTest = mock.Create<AddPlaylistPageViewModel>();

            // act
            await viewModelToTest.Initialise();
            viewModelToTest.PlaylistAudioFiles[0].IsSelected = true;
            viewModelToTest.PlaylistAudioFiles[1].IsSelected = true;
            var selectedAudioFileIDs = viewModelToTest.GetSelectedAudioFileIDs();

            // assert
            Assert.AreEqual(selectedAudioFileIDs.Count, 2);
        }

        [Test]
        public async Task GetPlaylistDuration_Should_Pass_With_Duration_Of_Playlist_AudioFile_Correct()
        {
            // arrange
            int duration1 = 300;
            int expectedDuration = duration1;
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                new AudioFile("Title1","Artist1","AlbumName1",duration1,"imageURI","mp3URI"),
            });

            var viewModelToTest = mock.Create<AddPlaylistPageViewModel>();

            // act
            await viewModelToTest.Initialise();
            viewModelToTest.PlaylistAudioFiles[0].IsSelected = true;
            var playlistDuration = viewModelToTest.GetPlaylistDuration();

            // assert
            Assert.AreEqual(playlistDuration, expectedDuration);
        }

        [Test]
        public async Task GetPlaylistDuration_Should_Pass_With_Duration_Of_Playlist_AudioFiles_Correct()
        {
            // arrange
            int duration1 = 300;
            int duration2 = 200;
            int expectedDuration = duration1 + duration2;
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                new AudioFile("Title1","Artist1","AlbumName1",duration1,"imageURI","mp3URI"),
                new AudioFile("Title2","Artist2","AlbumName2",duration2,"imageURI","mp3URI"),
            });

            var viewModelToTest = mock.Create<AddPlaylistPageViewModel>();

            // act
            await viewModelToTest.Initialise();
            viewModelToTest.PlaylistAudioFiles[0].IsSelected = true;
            viewModelToTest.PlaylistAudioFiles[1].IsSelected = true;
            var playlistDuration = viewModelToTest.GetPlaylistDuration();

            // assert
            Assert.AreEqual(playlistDuration, expectedDuration);
        }

        [Test]
        public async Task GeneratePlaylistFromView_Should_Pass_With_Correct_Playlist_Fields_Set()
        {
            // arrange
            string expectedTitle = "PlaylistName";
            string expectedImage = "clef_music_notes.png";  // default playlist image
            int duration1 = 300;
            int duration2 = 200;
            int expectedDuration = duration1 + duration2;
            List<string> expectedAudioFileIDs = new List<string> { "1", "2" };
            var af1 = new AudioFile("Title1", "Artist1", "AlbumName1", duration1, "imageURI", "mp3URI");
            af1.ID = expectedAudioFileIDs[0];
            var af2 = new AudioFile("Title2", "Artist2", "AlbumName2", duration2, "imageURI", "mp3URI");
            af2.ID = expectedAudioFileIDs[1];
            using var mock = AutoMock.GetStrict();

            var viewModelToTest = mock.Create<AddPlaylistPageViewModel>();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                af1,
                af2,
            });

            // act
            await viewModelToTest.Initialise();
            viewModelToTest.PlaylistAudioFiles[0].IsSelected = true;
            viewModelToTest.PlaylistAudioFiles[1].IsSelected = true;
            viewModelToTest.PlaylistName = expectedTitle;
            var playlist = viewModelToTest.GeneratePlaylistFromView();

            // assert
            Assert.AreEqual(playlist.Title, expectedTitle);
            Assert.AreEqual(playlist.Image, expectedImage);
            Assert.AreEqual(playlist.AudioFileIDs, expectedAudioFileIDs);
            Assert.AreEqual(playlist.NumOfAudioFiles, 2);
            Assert.AreEqual(playlist.TotalDuration, expectedDuration);

        }
    }
}
