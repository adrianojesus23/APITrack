using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Moq;
using Newtonsoft.Json;
using PixelServices.Models;
using StorageServices.Services;
using System.Text;
using TestPixelServices;

namespace TestStorageServices
{
    public class TestStorageService
    {
        [Fact]
        public async Task CreateStore_Post_Returns200StatusCode()
        {
            // Arrange
            var mockStorageService = new Mock<IStorageService>();

            await using var application = new TestingApplication();

            var mockHttpContext = new DefaultHttpContext();

            var client = application.CreateClient();

            PixelModel pixelModel = TestingApplication.GetPixel();

            StringContent content = new StringContent(JsonConvert.SerializeObject(pixelModel),
                                                         Encoding.UTF8, "application/json");
            // Act
            await client.PostAsync("CreateStore", content);

            // Assert
            Assert.Equal(200, mockHttpContext.Response.StatusCode);

            mockStorageService.Verify(service => service.CreateStore(mockHttpContext, "https://localhost:7105/createStore"), Times.Once);
        }
    }
}