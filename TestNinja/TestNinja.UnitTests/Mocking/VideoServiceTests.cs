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

        [SetUp]
        public void SetUp()
        {
            // Arrange
            _fileReader = new Mock<IFileReader>();
            _videoService = new VideoService(_fileReader.Object);
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
    }
}
