
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;
using PixelServices.Models;
using StorageServices.Helps;
using System.Net;
using System.Net.Http;
using System.Text;

namespace PixelServices.Services
{
    public class PixelService : IPixelService
    {
        private const string Ip = "8.8.8.8";

        private const string StorageServiceUrl = "https://localhost:7105/";

        public Task<MemoryStream> GetMemoryImage(HttpContext context)
        {
            var transparentGif = Convert.FromBase64String("R0lGODlhAQABAIAAAAAAAP///yH5BAEAAAAALAAAAAABAAEAAAIBRAA7");

            var image = new MemoryStream(transparentGif);

            return Task.FromResult(image);
        }

        /// <summary>
        /// Get http context (Referrer, User-Agent, IP)
        /// </summary>
        /// <param name="context"></param>
        /// <returns></returns>
        public Task<PixelModel> GetPixel(HttpContext context)
        {
            // Get information (Referrer, User-Agent, IP)
            var referrer = context.Request.Headers["Referer"];
            var userAgent = context.Request.Headers["User-Agent"];
            var ipAddress = context.Connection.RemoteIpAddress;

            return Task.FromResult(new PixelModel
            {
                IpAddress = ipAddress is null ? Ip : ipAddress.ToString(),
                Referrer = string.IsNullOrEmpty(referrer) ? string.Empty : referrer.ToString(),
                UserAgent = string.IsNullOrEmpty(userAgent) ? string.Empty : userAgent.ToString(),
            });
        }


        public async Task WritePixel(PixelModel resultContext)
        {
            using (var httpClient = new HttpClient())
            {
                StringContent content = new StringContent(JsonConvert.SerializeObject(resultContext), 
                                                          Encoding.UTF8, "application/json");

                await httpClient.PostAsync($"{StorageServiceUrl}createStore", content);
            }
        }

    }
}
