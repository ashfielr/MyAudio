using Autofac.Extras.Moq;
using Id3;
using Moq;
using MyAudio;
using MyAudio.Interfaces;
using MyAudio.Models;
using MyAudio.Services;
using MyAudio.ViewModels;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace MyAudioTests.ViewModels
{
    public class AudioFilesPageViewModelTests
    {
        [SetUp]
        public void Setup()
        {
            IocProvider.Init();
        }

        [Test]
        public async Task Initialise_Should_Pass_When_Service_Returns_AudioFiles_From_DataAccess()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {
                new AudioFile("Title1","Artist1","AlbumName1",300,"imageURI","mp3URI"),
                new AudioFile("Title2","Artist2","AlbumName2",200,"imageURI","mp3URI"),
            });

            var viewModelToTest = mock.Create<AudioFilesPageViewModel>();

            // act
            await viewModelToTest.Initialise();

            // assert
            Assert.IsTrue(viewModelToTest.AudioFilesListViewModel.AudioFiles.Count == 2);
        }

        [Test]
        public async Task Initialise_Should_Pass_When_Service_Returns_No_AudioFiles_From_DataAccess()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetAudioFilesAsync()).ReturnsAsync(new List<AudioFile>
            {

            });

            var viewModelToTest = mock.Create<AudioFilesPageViewModel>();

            // act
            await viewModelToTest.Initialise();

            // assert
            Assert.IsTrue(viewModelToTest.AudioFilesListViewModel.AudioFiles.Count == 0);
        }

        [Test]
        public async Task GetID3AudioFileData_Should_Pass_WithFullTagData_With_valid_mp3()
        {
            FileResult mp3FileResult = new FileResult("TestAudio/Full_ID3_Data.mp3");
            string timestampStr = "12345";
            string expectedTitle = "Run Away (With Vocals)";
            string expectedArtist = "Liborio Conti";
            string expectedAlbum = "Run Away";
            string expectedImg = "nonDefaultImg.png";

            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IFileService>().Setup(x => x.SaveImage(It.IsAny<string>(),
                                                             It.IsAny<byte[]>(), 
                                                             It.IsAny<string>()))
                                                    .ReturnsAsync(expectedImg);
            var viewModelToTest = mock.Create<AudioFilesPageViewModel>();

            // act
            var audioFile = await viewModelToTest.GetID3AudioFileData(mp3FileResult, timestampStr);

            // assert
            Assert.AreEqual(expectedTitle, audioFile.Title);
            Assert.AreEqual(expectedArtist, audioFile.Artist);
            Assert.AreEqual(expectedAlbum, audioFile.AlbumName);
            Assert.AreEqual(expectedImg, audioFile.Image);
        }

        [Test]
        public async Task GetID3AudioFileData_Should_Pass_TagDataButNoImage_With_valid_mp3()
        {
            FileResult mp3FileResult = new FileResult("TestAudio/NoImage.mp3");
            string timestampStr = "12345";
            string expectedTitle = "Fearless";
            string expectedArtist = "TULE";
            string expectedAlbum = "Unknown";
            string expectedImg = "clef_music_notes.png"; // default image

            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IFileService>().Setup(x => x.SaveImage(It.IsAny<string>(),
                                                             It.IsAny<byte[]>(),
                                                             It.IsAny<string>()))
                                                    .ReturnsAsync(expectedImg);
            var viewModelToTest = mock.Create<AudioFilesPageViewModel>();

            // act
            var audioFile = await viewModelToTest.GetID3AudioFileData(mp3FileResult, timestampStr);

            // assert
            Assert.AreEqual(expectedTitle, audioFile.Title);
            Assert.AreEqual(expectedArtist, audioFile.Artist);
            Assert.AreEqual(expectedAlbum, audioFile.AlbumName);
            Assert.AreEqual(expectedImg, audioFile.Image);
        }

        [Test]
        public async Task GetID3AudioFileData_Should_Pass_NoTagData_With_valid_mp3()
        {
            FileResult mp3FileResult = new FileResult("TestAudio/No_ID3_Data.mp3");
            string timestampStr = "12345";
            string expectedTitle = "No_ID3_Data.mp3";
            string expectedArtist = "Unknown";
            string expectedAlbum = "Unknown";
            string expectedImg = "clef_music_notes.png"; // default image

            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IFileService>().Setup(x => x.SaveImage(It.IsAny<string>(),
                                                             It.IsAny<byte[]>(),
                                                             It.IsAny<string>()))
                                                    .ReturnsAsync(expectedImg);
            var viewModelToTest = mock.Create<AudioFilesPageViewModel>();

            // act
            var audioFile = await viewModelToTest.GetID3AudioFileData(mp3FileResult, timestampStr);

            // assert
            Assert.AreEqual(expectedTitle, audioFile.Title);
            Assert.AreEqual(expectedArtist, audioFile.Artist);
            Assert.AreEqual(expectedAlbum, audioFile.AlbumName);
            Assert.AreEqual(expectedImg, audioFile.Image);
        }
    }
}
