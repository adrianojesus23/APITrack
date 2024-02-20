using StorageServices.Helps;

namespace StorageServices.Helps
{
    public static class ExtensionsSetting
    {

        public static async Task<string> GetAppSettings()
        {
            //IConfiguration configuration = new ConfigurationBuilder()
            //  .AddJsonFile("appsettings.json")
            //  .Build();

            //string logFilePath = configuration["Logging:LogLevel:LogFilePath"];

            var configuration = new ConfigurationBuilder()
                               .SetBasePath(Directory.GetCurrentDirectory())
                               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
                               .Build();

            string MyCustomKeyValue = configuration.GetValue<string>("MyCustomKey");
            string LogFilePath1 = configuration.GetValue<string>("LogFilePath1");

            string logFilePath = configuration.GetSection("Temp").GetSection("LogFilePath").Value;

            string logFilePath2 = configuration["Temp:LogFilePath"];
            Console.WriteLine(logFilePath);
            string visits = "Process started" + Environment.NewLine;
            if (logFilePath != null)
                File.WriteAllText(logFilePath + "visits.LOG", visits);

            return await Task.FromResult(logFilePath);
        }
    }
}
