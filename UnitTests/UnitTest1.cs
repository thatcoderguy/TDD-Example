using TDD_Example;
using Moq;
using HTTPConnection;

namespace UnitTests
{
    public class StatusCheckTests
    {
        private StatusCheck _statusChecker;
        private Mock<IClient> _mockedClient;

        [SetUp]
        public void Setup()
        {
            _mockedClient = new Mock<IClient>();
            _mockedClient.Setup(p => p.GetAsync("http://www.google.com")).Returns(new HttpResponseMessage(System.Net.HttpStatusCode.OK));
            _mockedClient.Setup(p => p.GetAsync("http://www.someinvalidurl.com")).Throws(new System.AggregateException());
            _mockedClient.Setup(p => p.GetAsync("")).Throws(new InvalidOperationException());
            _statusChecker = new StatusCheck(_mockedClient.Object);
        }

        [Test]
        public void Valid_Website_Check()
        {
            bool result = _statusChecker.WebsiteIsRunning("http://www.google.com");
            Assert.IsTrue(result);
        }


        [Test]
        public void Website_Doesnt_Exist()
        {
            bool result = _statusChecker.WebsiteIsRunning("http://www.someinvalidurl.com");
            Assert.IsFalse(result);
        }

        [Test]
        public void Empty_Url()
        {
            bool result = _statusChecker.WebsiteIsRunning("");
            Assert.IsFalse(result);
        }

        [Test]
        public void Invalid_URL()
        {
            bool result = _statusChecker.WebsiteIsRunning("hjh://blah");
            Assert.IsFalse(result);
        }

        [Test]
        public void Invalid_URL2()
        {
            bool result = _statusChecker.WebsiteIsRunning("http://blah*d!s");
            Assert.IsFalse(result);
        }
    }
}