using Driving_License_Management_DataAccessLayer;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace UnitTests.InitalizationClasses
{
    public class InitalizationCode
    {
        public static void InitalizeDataAccessLayer()
        {
            // Build configuration from appsettings.json
            var configuration = new ConfigurationBuilder()
                .SetBasePath(AppContext.BaseDirectory)
                .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
                .Build();

            // Initialize connection string
            clsDataAccessSettings.Initialize(configuration);
        }
    }
}
