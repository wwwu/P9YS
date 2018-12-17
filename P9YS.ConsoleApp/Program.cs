using Microsoft.Extensions.Configuration;
using System;
using System.IO;

namespace P9YS.ConsoleApp
{
    class Program
    {
        private static IConfiguration _configuration;

        static void Main(string[] args)
        {
            var fileName = "appsettings.json";
            var directory = AppContext.BaseDirectory;

            var filePath = $"{directory}/{fileName}";
            if (!File.Exists(filePath))
            {
                var length = directory.IndexOf("\\P9YS.ConsoleApp");
                filePath = $"{directory.Substring(0, length)}/P9YS.Web/{fileName}";
            }
            _configuration = new ConfigurationBuilder()
                .AddJsonFile(filePath, false, true)
                .Build();

            var s = _configuration["AppSettings:MovieResourceContext"];
        }
    }
}
