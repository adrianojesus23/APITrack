
namespace PixelServices.Services
{
    public class PixelService : IPixelService
    {
        private readonly string storageServiceUrl;
        public PixelService(IConfiguration  configuration)
        {
            configuration = new ConfigurationBuilder()
           .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
           .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
           .Build();

            string logFilePath = configuration["LogFilePath"];

            Console.WriteLine($"LogFilePath: {logFilePath}");
            storageServiceUrl = logFilePath;
        }
        public Task PixelRequest(HttpContext context)
        {
            throw new NotImplementedException();
        }

        public Task SendToStorage(string refer, string user, string ip)
        {
            throw new NotImplementedException();
        }
    }
}
