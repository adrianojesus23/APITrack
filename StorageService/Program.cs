
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using StorageServices.Helps;
using StorageServices.Services;

namespace StorageServices
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
            builder.Services.AddTransient<IStorageService, StorageService>();
            string logFilePath = builder.Configuration.GetValue<string>("LogFilePath");
            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapGet("GetStore", async ()
                       =>
            {
                return await Task.FromResult(new AppSettings { LogFilePath = logFilePath });
            });


            app.MapPost("CreateStore", async (IStorageService storageService, HttpContext context) =>
            {
                await storageService.CreateStore(context, logFilePath);

                context.Response.StatusCode = 200;

                return Results.Ok("Write log");
            });


            app.Run();
        }
    }
}
