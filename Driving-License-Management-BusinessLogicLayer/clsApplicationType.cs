using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    /// <summary>
    /// Represents an application type with properties and methods for CRUD operations.
    /// </summary>
    public class clsApplicationType
    {
        /// <summary>
        /// Enumerates the modes for application type operations.
        /// </summary>
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enType
        {
            NewLocalDrivingLicenseService = 1, RenewDrivingLicenseService = 2, ReplacementForALostDrivingLicense = 3,
            ReplacementForADamagedDrivingLicense = 4, ReleaseDetainedDrivingLicsense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public int ID { set; get; }
        public string Title { set; get; }
        public float Fees { set; get; }
        /// <summary>
        /// Initializes a new instance of the clsApplicationType class.
        /// </summary>
        public clsApplicationType()
        {
            this.ID = -1;
            this.Title = string.Empty;
            this.Fees = 0;
            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsApplicationType class with the specified ID, Title, and Fees.
        /// </summary>
        /// <param name="ID">The unique identifier of the application type.</param>
        /// <param name="Title">The title of the application type.</param>
        /// <param name="Fees">The fees associated with the application type.</param>
        private clsApplicationType(int ID, string Title, float Fees)
        {
            this.ID = ID;
            this.Title = Title;
            this.Fees = Fees;
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new application type to the database.
        /// </summary>
        /// <returns>True if the application type was added successfully, false otherwise.</returns>
        private bool _AddNewApplicationType()
        {
            //call DataAccess Layer
            this.ID = clsApplicationTypeData.AddNewApplicationType(this.Title, this.Fees);
            return (this.ID != -1);
        }
        /// <summary>
        /// Updates an existing application type in the database.
        /// </summary>
        /// <returns>True if the application type was updated successfully, false otherwise.</returns>
        private bool _UpdateApplicationType()
        {
            //call DataAccess Layer
            return clsApplicationTypeData.UpdateApplicationType(this.ID, this.Title, this.Fees);
        }
        /// <summary>
        /// Retrieves an application type by its unique identifier.
        /// </summary>
        /// <param name="ID">The unique identifier of the application type to find.</param>
        /// <returns>The application type with the specified ID, or null if not found.</returns>
        public static clsApplicationType Find(int ID)
        {
            string Title = string.Empty;
            float Fees = 0;
            if (clsApplicationTypeData.GetApplicationTypeInfoByID(ID, ref Title, ref Fees))
            {
                return new clsApplicationType(ID, Title, Fees);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieves all application types from the database.
        /// </summary>
        /// <returns>A DataTable containing all application types.</returns>
        public static DataTable GetAllApplicationTypes()
        {
            return clsApplicationTypeData.GetAllApplicationTypes();
        }
        public static List<clsApplicationType> GetAllApplicationTypesList()
        {
            DataTable dt = clsApplicationTypeData.GetAllApplicationTypes();
            List<clsApplicationType> list = new List<clsApplicationType>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(new clsApplicationType(
                    Convert.ToInt32(dr["ApplicationTypeID"]),
                    dr["ApplicationTypeTitle"].ToString(),
                    float.Parse(dr["ApplicationFees"].ToString())
                    ));
            }
            return list;
        }
        /// <summary>
        /// Deletes the application type from the database.
        /// </summary>
        /// <returns>
        /// True if the application type was deleted successfully, false otherwise.
        /// </returns>
        public bool Delete()
        {
            return clsApplicationTypeData.DeleteApplicationType(this.ID);
        }
        /// <summary>
        /// Saves the application type to the database based on its current mode.
        /// </summary>
        /// <returns>True if the application type was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplicationType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplicationType();
            }
            return false;
        }

    }

}
