using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace CamundaWebAPI.Core.Common
{
    public class ConfigSettings
    {
        public static string ConnectionString { get; private set; }
        public static void SetupConfig(IConfigurationRoot configuration)
        {
            ConnectionString = configuration.GetSection("Data").GetSection("ConnectionString").Value;
        }
    }
}
