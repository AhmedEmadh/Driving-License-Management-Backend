using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_DataAccessLayer
{
    public class clsLogger
    {
        private static string sourceName = "DrivingLicenseManagementDesktopApp";

        static clsLogger()
        {
            
        }


        /// <summary>
        /// Logs an exception to the Windows Event Viewer with the specified entry type.
        /// The default entry type is Error if not specified.
        /// </summary>
        /// <param name="ex">The exception to log.</param>
        public static void Log(Exception ex)
        {
            // Logging Method
        }



        private static string FormatErrorMessage(Exception ex)
        {

            // this is an example
            /*
                --- Exception Log ---
                Timestamp: 3/3/2025 6:45:32 PM
                Message: File not found.
                Inner Exception: File path was null.
                Stack Trace: at MyApp.Program.Main() in C:\Project\MyApp\Program.cs:line 24
                Source: MyApp
                -----------------------
             */

            string message =

                 $"--- Exception Log ---\n" +
                 $"Timestamp: {DateTime.Now}\n\n" +
                 $"Message:\n {ex.Message}\n\n" +
                 $"Inner Exception: \n{(ex.InnerException != null ? ex.InnerException.Message : "N/A")}\n\n" +
                 $"Stack Trace: \n{ex.StackTrace}\n\n" +
                 $"Source: \n{ex.Source}\n\n" +
                 $"-----------------------";

            return message;
        }


    }
}
