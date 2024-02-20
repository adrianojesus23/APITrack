namespace PixelServices.Services
{
    public interface IPixelService
    {
        Task PixelRequest(HttpContext context);
        Task SendToStorage(string refer, string user, string ip);
    }
}