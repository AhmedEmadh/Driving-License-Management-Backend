using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsDriver
    {
        enum enMode { AddNew, Update };
        enMode Mode = enMode.AddNew;
        public int DriverID { get; set; }
        public int PersonID { get; set; }
        public clsPerson PersonInfo { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo { get; set; }
        public DateTime CreatedDate { get; set; }
        /// <summary>
        /// Initializes a new instance of the clsDriver class with default values.
        /// </summary>
        public clsDriver()
        {
            this.DriverID = -1;
            this.PersonID = -1;
            this.CreatedByUserID = -1;
            this.CreatedDate = DateTime.Now;
            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsDriver class with the specified driver ID, person ID, created by user ID, and created date.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <param name="PersonID">The ID of the person associated with the driver.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the driver.</param>
        /// <param name="CreatedDate">The date when the driver was created.</param>
        private clsDriver(int DriverID, int PersonID, int CreatedByUserID, DateTime CreatedDate)
        {
            this.DriverID = DriverID;
            this.PersonID = PersonID;
            this.CreatedByUserID = CreatedByUserID;
            this.CreatedDate = CreatedDate;
            PersonInfo = clsPerson.Find(PersonID);
            CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new driver to the system.
        /// </summary>
        /// <returns>True if the driver was added successfully, false otherwise.</returns>
        private bool _AddNewDriver()
        {
            this.DriverID = clsDriverData.AddNewDriver(this.PersonID, this.CreatedByUserID);
            return (this.DriverID > 0);
        }
        /// <summary>
        /// Updates an existing driver in the system.
        /// </summary>
        /// <returns>True if the driver was updated successfully, false otherwise.</returns>
        private bool _UpdateDriver()
        {
            return clsDriverData.UpdateDriver(this.DriverID, this.PersonID, this.CreatedByUserID);
        }
        /// <summary>
        /// Finds a driver by its ID.
        /// </summary>
        /// <param name="DriverID">The ID of the driver to find.</param>
        /// <returns>A clsDriver object if found, otherwise null.</returns>
        public static clsDriver FindByDriverID(int DriverID)
        {
            int PersonID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.MaxValue;
            bool result = clsDriverData.GetDriverInfoByDriverID(DriverID, ref PersonID, ref CreatedByUserID, ref CreatedDate);
            if(!result)
            {
                return null;
            }
            return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
        }
        /// <summary>
        /// Finds a driver by its associated person ID.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the driver.</param>
        /// <returns>A clsDriver object if found, otherwise null.</returns>
        public static clsDriver FindByPersonID(int PersonID)
        {
            int DriverID = -1, CreatedByUserID = -1;
            DateTime CreatedDate = DateTime.MaxValue;

            if (clsDriverData.GetDriverInfoByPersonID(PersonID, ref DriverID, ref CreatedByUserID, ref CreatedDate))
            {
                return new clsDriver(DriverID, PersonID, CreatedByUserID, CreatedDate);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieves all drivers from the database.
        /// </summary>
        /// <returns>A DataTable containing all drivers.</returns>
        public static DataTable GetAllDrivers()
        {
            return clsDriverData.GetAllDrivers();
        }
        public static List<clsDriver> GetAllDriversList()
        {
            DataTable dt = clsDriverData.GetAllDrivers();
            List<clsDriver> drivers = new List<clsDriver>();
            foreach (DataRow row in dt.Rows)
            {
                int DriverID = Convert.ToInt32(row["DriverID"]);
                clsDriver driver = FindByDriverID(DriverID);
                drivers.Add(driver);
            }
            return drivers;
        }

        /// <summary>
        /// Saves the driver to the database.
        /// </summary>
        /// <returns>True if the driver was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDriver())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDriver();
            }
            return false;
        }
        /// <summary>
        /// Retrieves the licenses associated with a driver from the database.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <returns>A DataTable containing the licenses associated with the driver.</returns>
        public static DataTable GetLicenses(int DriverID)
        {
            return clsLicense.GetDriverLicenses(DriverID);
        }
        /// <summary>
        /// Retrieves the international licenses associated with a driver from the database.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <returns>A DataTable containing the international licenses associated with the driver.</returns>
        public static DataTable GetInternationalLicenses(int DriverID)
        {
            return clsInternationalLicense.GetDriverInternationalLicenses(DriverID);
        }
        /// <summary>
        /// Deletes the driver from the database.
        /// </summary>
        public bool Delete()
        {
            return clsDriverData.DeleteDriver(this.DriverID);
        }
    }
}
