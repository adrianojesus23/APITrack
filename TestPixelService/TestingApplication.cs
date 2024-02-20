using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using PixelServices;
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
    }
}
