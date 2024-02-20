//using Microsoft.AspNetCore.Http;
//using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Testing;
//using Microsoft.VisualStudio.TestPlatform.TestHost;
//using Moq;

using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Moq;
using PixelServices;
using PixelServices.Models;
using PixelServices.Services;
using StorageServices.Services;
using System;

namespace TestPixelServices
{
    public class TestPixelServiceController: WebApplicationFactory<Program>
    {
        [Fact]
        public async Task TrackGet_ReturnsGifImage()
        {
            // Arrange
            var mockPixelService = new Mock<IPixelService>();
            var mockStorageService = new Mock<IStorageService>();
            var mockHttpContext = new DefaultHttpContext();

            var transparentGif = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");

            var image = new MemoryStream(transparentGif);
            mockPixelService.Setup(service => service.GetMemoryImage(It.IsAny<HttpContext>())).ReturnsAsync(image);

            mockStorageService.Setup(service => service.CreateStore(It.IsAny<HttpContext>(), "https://localhost:7105/CreateStore"));

            var pixelModel = new PixelModel()
            {
                 IpAddress = "127.0.0.1",
                 Referrer = "https://localhost:7019/swagger/index.html",
                 UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:122.0) Gecko/20100101 Firefox/122.0"
            };
            mockPixelService.Setup(service => service.GetPixel(It.IsAny<HttpContext>())).ReturnsAsync(pixelModel);

            await using var application = new TestingApplication();

            var client = application.CreateClient();

            // Act
            var result = await client.GetAsync("/track");

            // Assert
            var fileResult = Assert.IsType<FileResult>(result);
            Assert.Equal("image/gif", fileResult.ContentType);
           // Assert.Equal(image, fileResult.c);

            mockPixelService.Verify(service => service.GetPixel(mockHttpContext), Times.Once);
            mockPixelService.Verify(service => service.WritePixel(pixelModel), Times.Once);
            mockPixelService.Verify(service => service.GetMemoryImage(mockHttpContext), Times.Once);
        }
    }
}