using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_DataAccessLayer
{
    /// <summary>
    /// Provides settings for data access operations.
    /// </summary>
    public static class clsDataAccessSettings
    {
        public static string ConnectionString { get; private set; }

        public static void Initialize(IConfiguration configuration)
        {
            ConnectionString = configuration.GetConnectionString("DefaultConnection")!;
        }
    }
}
