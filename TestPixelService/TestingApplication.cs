using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PixelServices;
using PixelServices.Models;
using PixelServices.Services;
using StorageServices.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestPixelServices
{
    public class TestingApplication : WebApplicationFactory<Program>
    {
        protected override IHost CreateHost(IHostBuilder builder)
        {
            builder.ConfigureServices(services =>
            {
                services.AddScoped<IPixelService, PixelService>();
                services.AddScoped<IStorageService, StorageService>();
            });

            return base.CreateHost(builder);
        }

        public static PixelModel GetPixel()
        {
            return new PixelModel()
            {
                IpAddress = "127.0.0.1",
                Referrer = "https://localhost:7019/swagger/index.html",
                UserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:122.0) Gecko/20100101 Firefox/122.0"
            };
        }
    }
}
