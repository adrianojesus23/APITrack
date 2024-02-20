
namespace PixelServices.Services
{
    public class PixelService : IPixelService
    {
        private readonly IConfiguration _configuration;

        public PixelService(IConfiguration  configuration)
        {
            _configuration = configuration;
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
