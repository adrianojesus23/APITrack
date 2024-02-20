
using Microsoft.AspNetCore.Mvc;
using PixelServices.Services;
using StorageServices.Services;
using System.Net;

namespace PixelServices
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddAuthorization();

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();
            builder.Services.AddTransient<IPixelService, PixelService>();
            builder.Services.AddTransient<IStorageService, StorageService>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            //Controllers:
            app.MapGet("/track", async ([FromServices] IPixelService pixelService,
                                        [FromServices] IStorageService storageService,
                                         HttpContext context) =>
            {
                //Get information by http context
                var resultContext = await pixelService.GetPixel(context);

                //Send information to service storage
                await pixelService.WritePixel(resultContext);

                var image = await pixelService.GetMemoryImage(context);

                // Return a transparent 1-pixel image in GIF format
                return Results.File(image, "image/gif");
            });

            app.Run();
        }
    }
}
