using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace QuartzExample.Helpers
{
    public static class ConfigurationHelper
    {
        public static IConfiguration Configuration { get; set; }
        public const int MaxFileSize = 20480000;

        static ConfigurationHelper()
        {
            var builder = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);

            Configuration = builder.Build();
        }
    }
}
