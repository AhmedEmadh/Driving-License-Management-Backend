using Driving_License_Management_DataAccessLayer;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsLicense
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;
        public enum enIssueReason { FirstTime = 1, Renew = 2, DamagedReplacement = 3, LostReplacement = 4 };
        public clsDriver DriverInfo;
        public int LicenseID { set; get; }
        public int ApplicationID { set; get; }
        public clsApplication ApplicationInfo;
        public int DriverID { set; get; }
        public int LicenseClass { set; get; }
        public clsLicenseClass licenseClassInfo;
        public DateTime IssueDate { private set; get; }
        public DateTime ExpirationDate { set; get; }
        public string Notes { set; get; }
        public float PaidFees { set; get; }
        public bool IsActive { set; get; }
        public enIssueReason IssueReason { set; get; }
        public string IssueReasonText
        {
            get
            {
                return GetIssueReasonText(this.IssueReason);
            }
        }
        //public clsDetainedLicense DetainedInfo { set; get; }
        public int CreatedByUserID { set; get; }
        public clsUser CreatedByUserInfo { set; get; }
        public bool IsDetained
        {
            get
            {
                return clsDetainedLicense.IsLicenseDetained(this.LicenseID);
            }
        }
        /// <summary>
        /// Initializes a new instance of the clsLicense class, setting default values for its properties.
        /// </summary>
        public clsLicense()
        {
            this.LicenseID = -1;
            this.ApplicationID = -1;
            this.DriverID = -1;
            this.LicenseClass = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;
            this.Notes = string.Empty;
            this.PaidFees = 0;
            this.IsActive = false;
            this.IssueReason = enIssueReason.FirstTime;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsLicense class.
        /// </summary>
        /// <param name="LicenseID">The ID of the license.</param>
        /// <param name="ApplicationID">The ID of the application.</param>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <param name="LicenseClass">The class of the license.</param>
        /// <param name="IssueDate">The date of issue for the license.</param>
        /// <param name="ExpirationDate">The expiration date for the license.</param>
        /// <param name="Notes">Any additional notes for the license.</param>
        /// <param name="PaidFees">The paid fees for the license.</param>
        /// <param name="IsActive">A flag indicating whether the license is active.</param>
        /// <param name="IssueReason">The reason for issuing the license.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the license.</param>
        private clsLicense(int LicenseID, int ApplicationID, int DriverID,
            int LicenseClass, DateTime IssueDate, DateTime ExpirationDate,
            string Notes, float PaidFees, bool IsActive, enIssueReason IssueReason,
            int CreatedByUserID)
        {
            this.LicenseID = LicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.LicenseClass = LicenseClass;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.Notes = Notes;
            this.PaidFees = PaidFees;
            this.IsActive = IsActive;
            this.IssueReason = IssueReason;
            this.CreatedByUserID = CreatedByUserID;

            _FillRelatedObjects();
            Mode = enMode.Update;
        }
        private void _FillRelatedObjects()
        {
            this.ApplicationInfo = clsApplication.FindBaseApplication(this.ApplicationID);
            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.licenseClassInfo = clsLicenseClass.Find(this.LicenseClass);
            //this.DetainedInfo = clsDetainedLicense.FindByLicenseID(this.LicenseID);
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
        }
        /// <summary>
        /// Adds a new license to the database.
        /// </summary>
        /// <returns>True if the license was added successfully, false otherwise.</returns>
        private bool _AddNewLicense()
        {
            this.IssueDate = DateTime.Now;
            licenseClassInfo = clsLicenseClass.Find(this.LicenseClass);
            if (licenseClassInfo != null) {
                this.ExpirationDate = this.IssueDate.AddYears(licenseClassInfo.DefaultValidityLength);
            }
            int LicenseID = clsLicenseData.AddNewLicense(this.ApplicationID, this.DriverID, this.LicenseClass, this.IssueDate, this.ExpirationDate, this.Notes, this.PaidFees, this.IsActive, (byte)this.IssueReason, this.CreatedByUserID);
            if (LicenseID > 0)
            {
                this.LicenseID = LicenseID;
                _FillRelatedObjects();
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Updates an existing license in the database.
        /// </summary>
        /// <returns>True if the license was updated successfully, false otherwise.</returns>
        private bool _UpdateLicense()
        {
            this.ExpirationDate = this.IssueDate.AddYears(licenseClassInfo.DefaultValidityLength);
            return clsLicenseData.UpdateLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (byte)IssueReason, CreatedByUserID); ;
        }
        /// <summary>
        /// Finds a license by its ID.
        /// </summary>
        /// <param name="LicenseID">The ID of the license to find.</param>
        /// <returns>A clsLicense object if found, otherwise null.</returns>
        public static clsLicense Find(int LicenseID)
        {
            int ApplicationID = -1, DriverID = -1, LicenseClass = -1, CreatedByUserID = -1;
            DateTime IssueDate = DateTime.MaxValue, ExpirationDate = DateTime.MaxValue;
            string Notes = string.Empty;
            float PaidFees = 0;
            bool IsActive = false;
            byte IssueReason = 0;
            if (clsLicenseData.GetLicenseInfoByID(LicenseID, ref ApplicationID, ref DriverID, ref LicenseClass, ref IssueDate, ref ExpirationDate, ref Notes, ref PaidFees, ref IsActive, ref IssueReason, ref CreatedByUserID))
            {
                return new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieves all licenses from the database.
        /// </summary>
        /// <returns>A DataTable containing all licenses.</returns>
        public static DataTable GetAllLicenses()
        {
            return clsLicenseData.GetAllLicenses();
        }
        public static List<clsLicense> GetAllLicensesList()
        {
            DataTable dt = clsLicenseData.GetAllLicenses();
            List<clsLicense> licenses = new List<clsLicense>();
            foreach (DataRow row in dt.Rows)
            {
                int LicenseID = Convert.ToInt32(row["LicenseID"]);
                int ApplicationID = Convert.ToInt32(row["ApplicationID"]);
                int DriverID = Convert.ToInt32(row["DriverID"]);
                int LicenseClass = Convert.ToInt32(row["LicenseClass"]);
                DateTime IssueDate = Convert.ToDateTime(row["IssueDate"]);
                DateTime ExpirationDate = Convert.ToDateTime(row["ExpirationDate"]);
                string Notes = row["Notes"].ToString();
                float PaidFees = float.Parse(row["PaidFees"].ToString());
                bool IsActive = Convert.ToBoolean(row["IsActive"]);
                byte IssueReason = Convert.ToByte(row["IssueReason"]);
                int CreatedByUserID = Convert.ToInt32(row["CreatedByUserID"]);
                clsLicense license = new clsLicense(LicenseID, ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, (enIssueReason)IssueReason, CreatedByUserID);
                licenses.Add(license);
            }
            return licenses;
        }
        /// <summary>
        /// Saves the license to the database.
        /// </summary>
        /// <returns>True if the license was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLicense())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLicense();
                default:
                    return false;
            }
        }
        /// <summary>
        /// Checks if a license exists for a given person ID and license class ID.
        /// </summary>
        /// <param name="PersonID">The ID of the person to check.</param>
        /// <param name="lIcenseClassID">The ID of the license class to check.</param>
        /// <returns>True if a license exists, false otherwise.</returns>
        public static bool IsLicenseExistByPersonID(int PersonID, int lIcenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, lIcenseClassID) > 0;
        }
        /// <summary>
        /// Retrieves the active license ID for a given person and license class.
        /// </summary>
        /// <param name="PersonID">The ID of the person.</param>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <returns>The active license ID if found, otherwise -1</returns>
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            return clsLicenseData.GetActiveLicenseIDByPersonID(PersonID, LicenseClassID);
        }
        /// <summary>
        /// Retrieves the licenses associated with a driver from the database.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <returns>A DataTable containing the licenses associated with the driver.</returns>
        public static DataTable GetDriverLicenses(int DriverID)
        {
            return clsLicenseData.GetDriverLicenses(DriverID);
        }
        /// <summary>
        /// Checks if a license is expired.
        /// </summary>
        /// <returns>True if the license is expired, false otherwise.</returns>
        public bool IsLicenseExpired()
        {
            return (this.ExpirationDate < DateTime.Now);
        }
        /// <summary>
        /// Deactivates the current license.
        /// </summary>
        /// <returns>True if the license was deactivated successfully, false otherwise.</returns>
        public bool DeactivateCurrentLicense()
        {
            return clsLicenseData.DeactivateLicense(this.LicenseID);
        }
        /// <summary>
        /// Retrieves the text representation of an issue reason.
        /// </summary>
        /// <param name="IssueReason">The issue reason.</param>
        /// <returns>The text representation of the issue reason.</returns>
        public static string GetIssueReasonText(enIssueReason IssueReason)
        {
            switch (IssueReason)
            {
                case enIssueReason.FirstTime:
                    return "First Time";
                case enIssueReason.Renew:
                    return "Renew";
                case enIssueReason.DamagedReplacement:
                    return "Damaged Replacement";
                case enIssueReason.LostReplacement:
                    return "Lost Replacement";
                default:
                    return "First Time";
            }
        }
        /// <summary>
        /// Detains a license.
        /// </summary>
        /// <param name="FineFees">The fine fees.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the detainment.</param>
        /// <returns>The detainment ID if successful, otherwise -1.</returns>
        public int Detain(float FineFees, int CreatedByUserID)
        {
            clsDetainedLicense detainedLicense = new clsDetainedLicense();
            detainedLicense.LicenseID = this.LicenseID;
            detainedLicense.FineFees = FineFees;
            detainedLicense.CreatedByUserID = CreatedByUserID;
            if (detainedLicense.Save())
            {
                return detainedLicense.DetainID;
            }
            else
            {
                return -1;
            }

        }
        /// <summary>
        /// Releases a detained license.
        /// </summary>
        /// <param name="ReleasedByUserID">The ID of the user releasing the license.</param>
        /// <param name="ApplicationID">The ID of the application used to release the license.</param>
        /// <returns>True if the license was released successfully, false otherwise.</returns>
        public bool ReleaseDetainedLicense(int ReleasedByUserID, ref int ApplicationID)
        {
            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.ReleaseDetainedDrivingLicense).Fees;
            Application.CreatedByUserID = ReleasedByUserID;

            if (!Application.Save())
            {
                ApplicationID = -1;
                return false;

            }
            ApplicationID = Application.ApplicationID;
            return clsDetainedLicense.FindByLicenseID(this.LicenseID).ReleaseDetainedLicense(ReleasedByUserID, Application.ApplicationID);

        }
        /// <summary>
        /// Renews a license.
        /// </summary>
        /// <param name="Notes">The notes for the renewal.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the renewal.</param>
        /// <returns>The renewed license if successful, otherwise null.</returns>
        public clsLicense RenewLicense(string Notes, int CreatedByUserID)
        {
            clsApplication Application = new clsApplication();
            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;
            Application.ApplicationTypeID = (int)clsApplication.enApplicationType.RenewDrivingLicense;
            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find((int)clsApplication.enApplicationType.RenewDrivingLicense).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }
            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;

            int DefaultValidityLength = this.licenseClassInfo.DefaultValidityLength;

            NewLicense.ExpirationDate = DateTime.Now.AddYears(DefaultValidityLength);
            NewLicense.Notes = Notes;
            NewLicense.PaidFees = this.licenseClassInfo.ClassFees;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = clsLicense.enIssueReason.Renew;
            NewLicense.CreatedByUserID = CreatedByUserID;


            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();

            return NewLicense;

        }
        /// <summary>
        /// Replaces a license.
        /// </summary>
        /// <param name="IssueReason">The issue reason.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the replacement.</param>
        /// <returns>The replaced license if successful, otherwise null.</returns>
        public clsLicense Replace(enIssueReason IssueReason, int CreatedByUserID)
        {

            clsApplication Application = new clsApplication();

            Application.ApplicantPersonID = this.DriverInfo.PersonID;
            Application.ApplicationDate = DateTime.Now;

            Application.ApplicationTypeID = (IssueReason == enIssueReason.DamagedReplacement) ?
                (int)clsApplication.enApplicationType.ReplaceDamagedDrivingLicense :
                (int)clsApplication.enApplicationType.ReplaceLostDrivingLicense;

            Application.ApplicationStatus = clsApplication.enApplicationStatus.Completed;
            Application.LastStatusDate = DateTime.Now;
            Application.PaidFees = clsApplicationType.Find(Application.ApplicationTypeID).Fees;
            Application.CreatedByUserID = CreatedByUserID;

            if (!Application.Save())
            {
                return null;
            }

            clsLicense NewLicense = new clsLicense();

            NewLicense.ApplicationID = Application.ApplicationID;
            NewLicense.DriverID = this.DriverID;
            NewLicense.LicenseClass = this.LicenseClass;
            NewLicense.IssueDate = DateTime.Now;
            NewLicense.ExpirationDate = this.ExpirationDate;
            NewLicense.Notes = this.Notes;
            NewLicense.PaidFees = 0;
            NewLicense.IsActive = true;
            NewLicense.IssueReason = IssueReason;
            NewLicense.CreatedByUserID = CreatedByUserID;

            if (!NewLicense.Save())
            {
                return null;
            }

            DeactivateCurrentLicense();

            return NewLicense;
        }

        public bool Delete()
        {
            return clsLicenseData.DeleteLicense(this.LicenseID);
        }
    }
}
