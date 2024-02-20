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
using System.Net;

namespace TestPixelServices
{
    public class TestPixelServiceController: WebApplicationFactory<Program>
    {
        [Fact]
        public async Task TrackGet_ReturnsGifImage()
        {
            // Arrange
            await using var application = new TestingApplication();

            var client = application.CreateClient();

            var transparentGif = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");

            // Act
            var response = await client.GetAsync("/track");

            var content = await response.Content.ReadAsByteArrayAsync();

            // Assert
            Assert.Equal(transparentGif, content);

            Assert.Equal(HttpStatusCode.OK, response.StatusCode);

        }

      
    }
}