using StorageServices.Helps;
using StorageServices.Models;

namespace StorageServices.Services
{
    public interface IStorageService
    {
        Task CreateStore(HttpContext resultContext, string logFilePath);
    }
}
