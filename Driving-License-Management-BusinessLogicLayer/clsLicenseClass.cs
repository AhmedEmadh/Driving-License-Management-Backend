using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsLicenseClass
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        public int LicenseClassID { set; get; }
        public string ClassName { set; get; }
        public string ClassDescription { set; get; }
        public byte MinimumAllowedAge { set; get; }
        public byte DefaultValidityLength { set; get; }
        public float ClassFees { set; get; }
        /// <summary>
        /// Initializes a new instance of the clsLicenseClass class, setting default values for its properties.
        /// </summary>
        public clsLicenseClass()
        {
            LicenseClassID = -1;
            ClassName = string.Empty;
            ClassDescription = string.Empty;
            MinimumAllowedAge = 18;
            DefaultValidityLength = 10;
            ClassFees = 0;
            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsLicenseClass class with the provided parameters.
        /// </summary>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <param name="ClassName">The name of the license class.</param>
        /// <param name="ClassDescription">The description of the license class.</param>
        /// <param name="MinimumAllowedAge">The minimum allowed age for the license class.</param>
        /// <param name="DefaultValidityLength">The default validity length of the license class.</param>
        /// <param name="ClassFees">The fees associated with the license class.</param>
        private clsLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            this.LicenseClassID = LicenseClassID;
            this.ClassName = ClassName;
            this.ClassDescription = ClassDescription;
            this.MinimumAllowedAge = MinimumAllowedAge;
            this.DefaultValidityLength = DefaultValidityLength;
            this.ClassFees = ClassFees;
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new license class to the database.
        /// </summary>
        /// <returns>True if the license class was added successfully, false otherwise.</returns>
        private bool _AddNewLicenseClass()
        {
            //call DataAccess Layer
            int AddedID = clsLicenseClassData.AddNewLicenseClass(this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
            return (AddedID != -1);
        }
        /// <summary>
        /// Updates an existing license class in the database.
        /// </summary>
        /// <returns>True if the license class was updated successfully, false otherwise.</returns>
        private bool _UpdateLicenseClass()
        {
            //call DataAccess Layer
            return clsLicenseClassData.UpdateLicenseClass(this.LicenseClassID, this.ClassName, this.ClassDescription, this.MinimumAllowedAge, this.DefaultValidityLength, this.ClassFees);
        }
        /// <summary>
        /// Finds a license class by its ID.
        /// </summary>
        /// <param name="LicenseClassID">The ID of the license class to find.</param>
        /// <returns>A clsLicenseClass object if found, otherwise null.</returns>
        public static clsLicenseClass Find(int LicenseClassID)
        {
            string ClassName = string.Empty;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            float ClassFees = 0;
            if (clsLicenseClassData.GetLicenseClassInfoByID(LicenseClassID, ref ClassName, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Finds a license class by its class name.
        /// </summary>
        /// <param name="ClassName">The name of the license class to find.</param>
        /// <returns>A clsLicenseClass object if found, otherwise null.</returns>
        public static clsLicenseClass Find(string ClassName)
        {
            int LicenseClassID = -1;
            string ClassDescription = string.Empty;
            byte MinimumAllowedAge = 0;
            byte DefaultValidityLength = 0;
            float ClassFees = 0;
            if (clsLicenseClassData.GetLicenseClassInfoByClassName(ClassName, ref LicenseClassID, ref ClassDescription, ref MinimumAllowedAge, ref DefaultValidityLength, ref ClassFees))
            {
                return new clsLicenseClass(LicenseClassID, ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieves all license classes from the database.
        /// </summary>
        /// <returns>A DataTable containing all license classes.</returns>
        public static DataTable GetAllLicenseClasses()
        {
            return clsLicenseClassData.GetAllLicenseClasses();
        }
        public static List<clsLicenseClass> GetAllLicenseClassesList()
        {
            DataTable dt = clsLicenseClassData.GetAllLicenseClasses();
            List<clsLicenseClass> licenseClasses = new List<clsLicenseClass>();
            foreach (DataRow row in dt.Rows)
            {
                clsLicenseClass licenseClass = new clsLicenseClass
                {
                    LicenseClassID = Convert.ToInt32(row["LicenseClassID"]),
                    ClassName = row["ClassName"].ToString(),
                    ClassDescription = row["ClassDescription"].ToString(),
                    MinimumAllowedAge = Convert.ToByte(row["MinimumAllowedAge"]),
                    DefaultValidityLength = Convert.ToByte(row["DefaultValidityLength"]),
                    ClassFees = Convert.ToSingle(row["ClassFees"]),
                    Mode = enMode.Update
                };
                licenseClasses.Add(licenseClass);
            }
            return licenseClasses;
        }
        /// <summary>
        /// Saves the current license class to the database based on its current mode.
        /// </summary>
        /// <returns>True if the license class was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicenseClass())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicenseClass();
            }
            return false;
        }
    }
}
