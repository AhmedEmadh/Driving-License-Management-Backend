// See https://aka.ms/new-console-template for more information

using Driving_License_Management_DataAccessLayer;
using System.Data;
using Microsoft.Extensions.Configuration;
using Driving_License_Management_BusinessLogicLayer;

// Build configuration from appsettings.json
var configuration = new ConfigurationBuilder()
    .SetBasePath(AppContext.BaseDirectory)
    .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
    .Build();

// Initialize connection string
clsDataAccessSettings.Initialize(configuration);


//Test Get AllApplications method
void Test()
{

    var Data = clsDetainedLicense.GetAllDetainedLicensesList();
    if (Data != null)
    {
        foreach (clsDetainedLicense item in Data)
        {
            Console.WriteLine($"LicenseID: {item.LicenseID}");
        }
    }
    else
    {
        Console.WriteLine("No Data Found");
    }
}




Test();

//wait for user input before closing
Console.WriteLine("Press any key to exit...");
Console.ReadKey();