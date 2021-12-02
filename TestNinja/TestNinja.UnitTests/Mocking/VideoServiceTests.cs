using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class VideoServiceTests
    {
        private VideoService _videoService; 
        private new Mock<IFileReader> _fileReader;
        private Mock<IVideoRepository> _repository;

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _fileReader = new Mock<IFileReader>();
            _repository = new Mock<IVideoRepository>();
            _videoService = new VideoService(_fileReader.Object, _repository.Object);
        }

        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            // Arrange
            _fileReader.Setup(fr => fr.Read("video.txt")).Returns("");
            
            // Act
            var result = _videoService.ReadVideoTitle();

            // Assert
            var expected = "Error parsing the video.";
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }


        // Test cases
        // Repository return empty list [] => ""
        // Repository return a list of video [{id =1 }, {id = 2}, {id =3}] => "1,2,3"

        [Test]
        public void GetUnprocessedVideosAsCsv_AllVideosAreProcessed_ReturnAnEmptyString()
        {
            // Arrange
            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(new List<Video>());

            // Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Is.EqualTo(""));
        }

        [Test]
        public void GetUnprocessedVideosAsCsv_AFewUnprocessedVideos_ReturnAStringWithIdOfUnprocessedVideos()
        {
            // Arrange
            var videos = new List<Video>()
            {
                new Video {Id = 1},
                new Video {Id = 2},
                new Video {Id = 3},
            };

            _repository.Setup(r => r.GetUnprocessedVideos()).Returns(videos);

            // Act
            var result = _videoService.GetUnprocessedVideosAsCsv();

            // Assert
            Assert.That(result, Is.EqualTo("1,2,3"));
        }
    }
}
