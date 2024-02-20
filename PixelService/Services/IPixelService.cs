using PixelServices.Models;

namespace PixelServices.Services
{
    public interface IPixelService
    {
        Task<MemoryStream> GetMemoryImage(HttpContext context);
        Task<PixelModel> GetPixel(HttpContext context);
        Task WritePixel(PixelModel resultContext);
    }
}