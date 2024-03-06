using NUnit.Framework;

namespace StorageAPI.Scripts.UnitTest
{
    public class TestClass
    {
        public string ProcessRequest(string request)
        {
            if (string.IsNullOrWhiteSpace(request))
            {
                return "Empty";
            }
            else if (request == "ValidRequest")
            {
                return "Success";
            }
            else
            {
                return "Error";
            }
        }
    }

    [TestFixture]
    public class ServerTests
    {
        [Test]
        public void ServerShouldReturnSuccessResponse()
        {
            // Arrange
            TestClass myServer = new TestClass();

            // Act
            string response = myServer.ProcessRequest("ValidRequest");

            // Assert
            Assert.Equal("Success", response); // Make sure to import NUnit.Framework for the Assert class
        }

        [Test]
        public void ServerShouldHandleErrorResponse()
        {
            // Arrange
            TestClass myServer = new TestClass();

            // Act
            string response = myServer.ProcessRequest("InvalidRequest");

            // Assert
            Assert.Equal("Error", response); // Import NUnit.Framework for the Assert class
        }

        [Test]
        public void ServerShouldHandleEmptyRequest()
        {
            // Arrange
            TestClass myServer = new TestClass();

            // Act
            string response = myServer.ProcessRequest("");

            // Assert
            Assert.Equal("Empty", response); // Import NUnit.Framework for the Assert class
        }
    }
}