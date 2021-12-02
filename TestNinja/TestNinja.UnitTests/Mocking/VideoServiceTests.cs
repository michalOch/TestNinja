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
        [Test]
        public void ReadVideoTitle_EmptyFile_ReturnError()
        {
            var service = new VideoService(new FakeFileReader());

            var result = service.ReadVideoTitle();

            var expected = "Error parsing the video.";
            Assert.That(result, Is.EqualTo(expected));
            Assert.That(result, Does.Contain("error").IgnoreCase);
        }
    }
}
