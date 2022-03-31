using Autofac.Extras.Moq;
using Moq;
using MyAudio;
using MyAudio.Interfaces;
using MyAudio.Models;
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


    }
}
