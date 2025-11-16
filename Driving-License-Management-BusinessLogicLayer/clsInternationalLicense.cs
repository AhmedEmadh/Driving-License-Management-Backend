using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsInternationalLicense : clsApplication
    {

        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public clsDriver DriverInfo;
        public int InternationalLicenseID { set; get; }
        public int DriverID { set; get; }
        public int IssuedUsingLocalLicenseID { set; get; }
        public DateTime IssueDate { set; get; }
        public DateTime ExpirationDate { set; get; }
        public bool IsActive { set; get; }

        /// <summary>
        /// Default Constructor
        /// </summary>
        public clsInternationalLicense()

        {
            //here we set the applicaiton type to New International License.
            this.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;

            this.InternationalLicenseID = -1;
            this.DriverID = -1;
            this.IssuedUsingLocalLicenseID = -1;
            this.IssueDate = DateTime.Now;
            this.ExpirationDate = DateTime.Now;

            this.IsActive = true;


            Mode = enMode.AddNew;

        }
        /// <summary>
        /// Initializes a new instance of the clsInternationalLicense class.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application.</param>
        /// <param name="ApplicantPersonID">The ID of the applicant person.</param>
        /// <param name="ApplicationDate">The date of the application.</param>
        /// <param name="ApplicationStatus">The status of the application.</param>
        /// <param name="LastStatusDate">The date of the last status update.</param>
        /// <param name="PaidFees">The paid fees for the application.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the application.</param>
        /// <param name="InternationalLicenseID">The ID of the international license.</param>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <param name="IssuedUsingLocalLicenseID">The ID of the local license used to issue the international license.</param>
        /// <param name="IssueDate">The date of issue for the international license.</param>
        /// <param name="ExpirationDate">The expiration date for the international license.</param>
        /// <param name="IsActive">A flag indicating whether the international license is active.</param>
        /// <returns>A new instance of the clsInternationalLicense class.</returns>
        public clsInternationalLicense(int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate,
             enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
             float PaidFees, int CreatedByUserID,
             int InternationalLicenseID, int DriverID, int IssuedUsingLocalLicenseID,
            DateTime IssueDate, DateTime ExpirationDate, bool IsActive)

        {
            //this is for the base clase
            base.ApplicationID = ApplicationID;
            base.ApplicantPersonID = ApplicantPersonID;
            base.ApplicationDate = ApplicationDate;
            base.ApplicationTypeID = (int)clsApplication.enApplicationType.NewInternationalLicense;
            base.ApplicationStatus = ApplicationStatus;
            base.LastStatusDate = LastStatusDate;
            base.PaidFees = PaidFees;
            base.CreatedByUserID = CreatedByUserID;

            this.InternationalLicenseID = InternationalLicenseID;
            this.ApplicationID = ApplicationID;
            this.DriverID = DriverID;
            this.IssuedUsingLocalLicenseID = IssuedUsingLocalLicenseID;
            this.IssueDate = IssueDate;
            this.ExpirationDate = ExpirationDate;
            this.IsActive = IsActive;
            this.CreatedByUserID = CreatedByUserID;

            this.DriverInfo = clsDriver.FindByDriverID(this.DriverID);
            this.ApplicantPersonInfo = clsPerson.Find(this.ApplicantPersonID);
            this.CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new international license to the database.
        /// </summary>
        /// <returns>True if the international license was added successfully, false otherwise.</returns>
        private bool _AddNewInternationalLicense()
        {
            //call DataAccess Layer 

            this.InternationalLicenseID =
                clsInternationalLicenseData.AddNewInternationalLicense(this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);


            return (this.InternationalLicenseID != -1);
        }
        /// <summary>
        /// Updates an existing international license in the database.
        /// </summary>
        /// <returns>True if the international license was updated successfully, false otherwise.</returns>
        private bool _UpdateInternationalLicense()
        {
            //call DataAccess Layer 

            return clsInternationalLicenseData.UpdateInternationalLicense(
                this.InternationalLicenseID, this.ApplicationID, this.DriverID, this.IssuedUsingLocalLicenseID,
               this.IssueDate, this.ExpirationDate,
               this.IsActive, this.CreatedByUserID);
        }

        /// <summary>
        /// Finds an international license by its ID.
        /// </summary>
        /// <param name="InternationalLicenseID">The ID of the international license to find.</param>
        /// <returns>A clsInternationalLicense object if found, otherwise null.</returns>
        public static clsInternationalLicense Find(int InternationalLicenseID)
        {
            int ApplicationID = -1;
            int DriverID = -1; int IssuedUsingLocalLicenseID = -1;
            DateTime IssueDate = DateTime.Now; DateTime ExpirationDate = DateTime.Now;
            bool IsActive = true; int CreatedByUserID = 1;

            if (clsInternationalLicenseData.GetInternationalLicenseInfoByID(InternationalLicenseID, ref ApplicationID, ref DriverID,
                ref IssuedUsingLocalLicenseID,
            ref IssueDate, ref ExpirationDate, ref IsActive, ref CreatedByUserID))
            {
                //now we find the base application
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);


                return new clsInternationalLicense(Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID,
                                     InternationalLicenseID, DriverID, IssuedUsingLocalLicenseID,
                                         IssueDate, ExpirationDate, IsActive);

            }

            else
                return null;

        }

        /// <summary>
        /// Retrieves all international licenses from the database.
        /// </summary>
        /// <returns>A DataTable containing all international licenses.</returns>
        public static DataTable GetAllInternationalLicenses()
        {
            return clsInternationalLicenseData.GetAllInternationalLicenses();

        }
        public static List<clsInternationalLicense> GetAllInternationalLicensesList()
        {
            DataTable dt = GetAllInternationalLicenses();
            List<clsInternationalLicense> list = new List<clsInternationalLicense>();
            foreach (DataRow row in dt.Rows)
            {
                int InternationalLicenseID = Convert.ToInt32(row["InternationalLicenseID"]);
                clsInternationalLicense license = Find(InternationalLicenseID);
                if (license != null)
                {
                    list.Add(license);
                }
            }
            return list;
        }
        /// <summary>
        /// Saves the current international license to the database.
        /// </summary>
        /// <returns>True if the international license was saved successfully, false otherwise.</returns>
        public bool Save()
        {

            //Because of inheritance first we call the save method in the base class,
            //it will take care of adding all information to the application table.
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewInternationalLicense())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateInternationalLicense();

            }

            return false;
        }
        /// <summary>
        /// Retrieves the ID of the active international license associated with a driver from the database.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <returns>The ID of the active international license associated with the driver, or -1 if not found.</returns>
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {

            return clsInternationalLicenseData.GetActiveInternationalLicenseIDByDriverID(DriverID);

        }
        /// <summary>
        /// Retrieves the international licenses associated with a driver from the database.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <returns>A DataTable containing the international licenses associated with the driver.</returns>
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            return clsInternationalLicenseData.GetDriverInternationalLicenses(DriverID);
        }
        public bool Delete()
        {
            return clsInternationalLicenseData.DeleteInternationalLicense(this.InternationalLicenseID);
        }
    }
}
