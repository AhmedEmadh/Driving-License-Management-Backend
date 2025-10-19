using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsDetainedLicense
    {
        enum enMode { AddNew, Update };
        enMode Mode = enMode.AddNew;

        public int DetainID { get; set; }
        public int LicenseID { get; set; }
        DateTime DetainDate { get; set; }
        public float FineFees { get; set; }
        public int CreatedByUserID { get; set; }
        clsUser CreatedByUserInfo { get; set; }
        public bool IsReleased { get; set; }
        DateTime ReleaseDate { get; set; }
        int ReleasedByUserID { get; set; }
        clsUser ReleasedByUserInfo { get; set; }
        int ReleaseApplicationID { get; set; }

        public clsDetainedLicense()
        {
            this.DetainID = -1;
            this.LicenseID = -1;
            this.DetainDate = DateTime.Now;
            this.FineFees = 0;
            this.CreatedByUserID = -1;
            this.IsReleased = false;
            this.CreatedByUserInfo = null;
            this.ReleaseDate = DateTime.MaxValue;
            this.ReleasedByUserID = -1;
            this.ReleasedByUserInfo = null;
            this.ReleaseApplicationID = -1;

            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsDetainedLicense class.
        /// </summary>
        /// <param name="DetainID">The ID of the detained license.</param>
        /// <param name="LicenseID">The ID of the license that is being detained.</param>
        /// <param name="DetainDate">The date the license was detained.</param>
        /// <param name="FineFees">The fine fees associated with the detained license.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the detained license.</param>
        /// <param name="IsReleased">A boolean indicating whether the license has been released.</param>
        /// <param name="ReleaseDate">The date the license was released.</param>
        /// <param name="ReleasedByUserID">The ID of the user who released the license.</param>
        /// <param name="ReleaseApplicationID">The ID of the application used to release the license.</param>
        /// <returns>A new instance of the clsDetainedLicense class.</returns>
        public clsDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID, bool IsReleased, DateTime ReleaseDate, int ReleasedByUserID, int ReleaseApplicationID)
        {
            this.DetainID = DetainID;
            this.LicenseID = LicenseID;
            this.DetainDate = DetainDate;
            this.FineFees = FineFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsReleased = IsReleased;
            this.ReleaseDate = ReleaseDate;
            this.ReleasedByUserID = ReleasedByUserID;
            this.ReleaseApplicationID = ReleaseApplicationID;
            CreatedByUserInfo = clsUser.FindByUserID(CreatedByUserID);
            ReleasedByUserInfo = clsUser.FindByUserID(ReleasedByUserID);
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new detained license to the database.
        /// </summary>
        /// <returns>True if the detained license was added successfully, false otherwise.</returns>
        private bool _AddNewDetainedLicense()
        {
            this.DetainID = clsDetainedLicenseData.AddNewDetainedLicense(this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
            return (this.DetainID > 0);
        }
        /// <summary>
        /// Updates the detained license information in the database.
        /// </summary>
        /// <returns>True if the update was successful, false otherwise.</returns>
        private bool _UpdateDetainedLicense()
        {
            return clsDetainedLicenseData.UpdateDetainedLicense(this.DetainID, this.LicenseID, this.DetainDate, this.FineFees, this.CreatedByUserID);
        }
        /// <summary>
        /// Finds a detained license by its ID.
        /// </summary>
        /// <param name="DetainID">The ID of the detained license to find.</param>
        /// <returns>A clsDetainedLicense object if found, otherwise null.</returns>
        public static clsDetainedLicense Find(int DetainID)
        {
            int LicenseID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.MaxValue, ReleaseDate = DateTime.MaxValue;
            float FineFees = 0;
            bool IsReleased = false;
            if (clsDetainedLicenseData.GetDetainedLicenseInfoByID(DetainID, ref LicenseID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }
        /// <summary>
        /// Retrieves all detained licenses from the database.
        /// </summary>
        /// <returns>A DataTable containing all detained licenses.</returns>
        public static DataTable GetAllDetainedLicenses()
        {
            return clsDetainedLicenseData.GetAllDetainedLicenses();
        }
        public static List<clsDetainedLicense> GetAllDetainedLicensesList()
        {
            DataTable dt = GetAllDetainedLicenses();
            List<clsDetainedLicense> detainedLicenses = new List<clsDetainedLicense>();
            foreach (DataRow row in dt.Rows)
            {
                int DetainID = Convert.ToInt32(row["DetainID"]);
                int LicenseID = Convert.ToInt32(row["LicenseID"]);
                DateTime DetainDate = Convert.ToDateTime(row["DetainDate"]);
                float FineFees = float.Parse(row["FineFees"].ToString());
                int CreatedByUserID = Convert.ToInt32(row["CreatedByUserID"]);
                bool IsReleased = Convert.ToBoolean(row["IsReleased"]);
                DateTime ReleaseDate = Convert.ToDateTime(row["ReleaseDate"]);
                int ReleasedByUserID = Convert.ToInt32(row["ReleasedByUserID"]);
                int ReleaseApplicationID = Convert.ToInt32(row["ReleaseApplicationID"]);
                clsDetainedLicense detainedLicense = new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
                detainedLicenses.Add(detainedLicense);
            }
            return detainedLicenses;
        }
        /// <summary>
        /// Finds a detained license by its associated license ID.
        /// </summary>
        /// <param name="LicenseID">The ID of the license to find the detained license for.</param>
        /// <returns>A clsDetainedLicense object if found, otherwise null.</returns>
        public static clsDetainedLicense FindByLicenseID(int LicenseID)
        {
            int DetainID = -1, CreatedByUserID = -1, ReleasedByUserID = -1, ReleaseApplicationID = -1;
            DateTime DetainDate = DateTime.MaxValue, ReleaseDate = DateTime.MaxValue;
            float FineFees = 0;
            bool IsReleased = false;
            if (clsDetainedLicenseData.GetDetainedLicenseInfoByLicenseID(LicenseID, ref DetainID, ref DetainDate, ref FineFees, ref CreatedByUserID, ref IsReleased, ref ReleaseDate, ref ReleasedByUserID, ref ReleaseApplicationID))
                return new clsDetainedLicense(DetainID, LicenseID, DetainDate, FineFees, CreatedByUserID, IsReleased, ReleaseDate, ReleasedByUserID, ReleaseApplicationID);
            else
                return null;
        }
        /// <summary>
        /// Saves the detained license information to the database based on its current mode.
        /// </summary>
        /// <returns>True if the detained license information was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewDetainedLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateDetainedLicense();
            }
            return false;
        }
        /// <summary>
        /// Checks if a license is detained.
        /// </summary>
        /// <param name="LicenseID">The ID of the license to check.</param>
        /// <returns>True if the license is detained, false otherwise.</returns>
        public static bool IsLicenseDetained(int LicenseID)
        {
            return clsDetainedLicenseData.IsLicenseDetained(LicenseID);
        }
        /// <summary>
        /// Releases a detained license.
        /// </summary>
        /// <param name="ReleasedByUserID">The ID of the user releasing the license.</param>
        /// <param name="ReleaseApplicationID">The ID of the application used to release the license.</param>
        /// <returns>True if the license was released successfully, false otherwise.</returns>
        public bool ReleaseDetainedLicense(int ReleasedByUserID, int ReleaseApplicationID)
        {
            return clsDetainedLicenseData.ReleaseDetainedLicense(this.DetainID, ReleasedByUserID, ReleaseApplicationID);
        }
    }
}
