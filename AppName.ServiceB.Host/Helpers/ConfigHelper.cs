using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace AppName.ServiceB.Host.Helpers
{
    public static class ConfigHelper
    {
        public static IConfigurationRoot Configuration { get; private set; }
        public static void LoadConfig(string[] args)
        {
            var configBuilder = new ConfigurationBuilder()
                .SetBasePath(Path.Combine(Directory.GetCurrentDirectory(), "Configuration"))
                .AddJsonFile("settings.json", optional: true, reloadOnChange: true);
            
            Configuration = configBuilder.Build();
        }
    }
}
