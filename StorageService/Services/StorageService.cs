
using StorageServices.Models;

namespace StorageServices.Services
{
    public class StorageService : IStorageService
    {
        public async Task CreateStore(HttpContext resultContext, string logFilePath)
        {
            string data = await new StreamReader(resultContext.Request.Body).ReadToEndAsync();

            await File.AppendAllLinesAsync(logFilePath + "visits.log", new[] { data });
        }
    }
}
