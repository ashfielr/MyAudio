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
    public  class PlaylistsPageViewModelTests
    {
        [SetUp]
        public void Setup()
        {
            IocProvider.Init();
        }

        [Test]
        public async Task Initialise_Should_Pass_When_Service_Returns_Playlists_From_DataAccess()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetPlaylists()).ReturnsAsync(new List<Playlist>
            {
                new Playlist("PlaylistTitle",1,"Image.jpg",123),
                new Playlist("PlaylistTitle2",2,"Image2.jpg",125),
            });

            var viewModelToTest = mock.Create<PlaylistsPageViewModel>();

            // act
            await viewModelToTest.Initialise();

            // assert
            Assert.IsTrue(viewModelToTest.Playlists.Count == 2);
        }

        [Test]
        public async Task Initialise_Should_Pass_When_Service_Returns_No_Playlists_From_DataAccess()
        {
            // arrange
            using var mock = AutoMock.GetStrict();
            mock.Mock<IMyAudioDataAccess>().Setup(x => x.GetPlaylists()).ReturnsAsync(new List<Playlist>
            {

            });

            var viewModelToTest = mock.Create<PlaylistsPageViewModel>();

            // act
            await viewModelToTest.Initialise();

            // assert
            Assert.IsTrue(viewModelToTest.Playlists.Count == 0);
        }
    }
}
