using Driving_License_Management_DataAccessLayer;

namespace Driving_License_Management_BusinessLogicLayer
{
    /// <summary>
    /// Represents an application for a driving license.
    /// </summary>
    public class clsApplication
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enum enApplicationType
        {
            NewDriveingLicense = 1, RenewDrivingLicense = 2, ReplaceLostDrivingLicense = 3,
            ReplaceDamagedDrivingLicense = 4, ReleaseDetainedDrivingLicense = 5, NewInternationalLicense = 6, RetakeTest = 7
        };
        public enMode Mode = enMode.AddNew;
        public enum enApplicationStatus { New = 1, Cancelled = 2, Completed = 3 };
        public int ApplicationID { get; set; }
        public int ApplicantPersonID { get; set; }
        public clsPerson ApplicantPersonInfo;
        public string ApplicantfullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }
        public DateTime ApplicationDate { get; set; }
        public int ApplicationTypeID { get; set; }
        public clsApplicationType ApplicationTypeInfo;
        public enApplicationStatus ApplicationStatus { get; set; }
        public string StatusText
        {
            get
            {
                switch (ApplicationStatus)
                {
                    case enApplicationStatus.New:
                        return "New";
                    case enApplicationStatus.Cancelled:
                        return "Cancelled";
                    case enApplicationStatus.Completed:
                        return "Completed";
                    default:
                        return "Unknown";
                }
            }
        }
        public DateTime LastStatusDate { get; set; }
        public float PaidFees { get; set; }
        public int CreatedByUserID { get; set; }
        public clsUser CreatedByUserInfo;

        public clsApplication()
        {
            this.ApplicationID = -1;
            this.ApplicantPersonID = -1;
            this.ApplicationDate = DateTime.Now;
            this.ApplicationTypeID = -1;
            this.ApplicationStatus = enApplicationStatus.New;
            this.LastStatusDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;
        }
        protected clsApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int AppllicationTypeID, enApplicationStatus ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            this.ApplicationID = ApplicationID;
            this.ApplicantPersonID = ApplicantPersonID;
            this.ApplicationDate = ApplicationDate;
            this.ApplicationTypeID = AppllicationTypeID;
            this.ApplicationStatus = ApplicationStatus;
            this.LastStatusDate = LastStatusDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            ApplicationTypeInfo = clsApplicationType.Find(this.ApplicationTypeID);
            CreatedByUserInfo = clsUser.FindByUserID(this.CreatedByUserID);
            ApplicantPersonInfo = clsPerson.Find(this.ApplicantPersonID);
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new application to the database.
        /// </summary>
        /// <returns>True if the application was added successfully, false otherwise.</returns>
        private bool _AddNewApplication()
        {
            //call DataAccess Layer
            this.ApplicationID = clsApplicationData.AddNewApplication(this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);
            return (this.ApplicationID != -1);
        }
        /// <summary>
        /// Updates an existing application in the database.
        /// </summary>
        /// <returns>True if the application was updated successfully, false otherwise.</returns>
        private bool _UpdateApplication()
        {
            //call DataAccess Layer
            return clsApplicationData.UpdateApplication(this.ApplicationID, this.ApplicantPersonID, this.ApplicationDate, this.ApplicationTypeID, (byte)this.ApplicationStatus, this.LastStatusDate, this.PaidFees, this.CreatedByUserID);

        }
        /// <summary>
        /// Finds the base application for the given ApplicationID.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to find.</param>
        /// <returns>The base application if found, otherwise null.</returns>
        public static clsApplication FindBaseApplication(int ApplicationID)
        {
            int ApplicantPersonID = -1, ApplicationTypeID = -1, CreatedByUserID = -1;
            DateTime ApplicationDate = DateTime.MaxValue;
            DateTime LastStatusDate = DateTime.MaxValue;
            float PaidFees = 0;
            byte ApplicationStatus = 0;

            bool isFound = clsApplicationData.GetApplicationInfoByID(ApplicationID, ref ApplicantPersonID, ref ApplicationDate, ref ApplicationTypeID, ref ApplicationStatus, ref LastStatusDate, ref PaidFees, ref CreatedByUserID);
            if (isFound)
            {
                return new clsApplication(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, (enApplicationStatus)ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Cancels the current application by updating its status.
        /// </summary>
        /// <returns>True if the application status was updated successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewApplication())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateApplication();
                default:
                    return false;
            }
        }
        /// <summary>
        /// Cancels the current application.
        /// </summary>
        /// <returns>True if the application was cancelled successfully, false otherwise.</returns>
        public bool Cancel()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, (short)enApplicationStatus.Cancelled);
        }
        /// <summary>
        /// Sets the application status to completed.
        /// </summary>
        /// <returns>True if the status was updated successfully, false otherwise.</returns>
        public bool SetComplete()
        {
            return clsApplicationData.UpdateStatus(ApplicationID, (short)enApplicationStatus.Completed);
        }
        /// <summary>
        /// Deletes the application with the specified ApplicationID.
        /// </summary>
        /// <returns>True if the application was deleted successfully, false otherwise.</returns>
        public bool Delete()
        {
            return clsApplicationData.DeleteApplication(ApplicationID);
        }
        /// <summary>
        /// Checks if an application exists in the database.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to check.</param>
        /// <returns>True if the application exists, false otherwise.</returns>
        public static bool IsApplicationExist(int ApplicationID)
        {
            return clsApplicationData.IsApplicationExist(ApplicationID);
        }
        /// <summary>
        /// Checks if a person has an active application for a specific application type.
        /// </summary>
        /// <param name="PersonID">The ID of the person to check.</param>
        /// <param name="ApplicationTypeID">The ID of the application type to check.</param>
        /// <returns>True if the person has an active application, false otherwise.</returns>
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return clsApplicationData.DoesPersonHaveActiveApplication(PersonID, ApplicationTypeID);
        }
        /// <summary>
        /// Checks if the applicant person has an active application for the given application type.
        /// </summary>
        /// <param name="ApplicationTypeID">The ID of the application type to check.</param>
        /// <returns>True if the person has an active application, false otherwise.</returns>
        public bool DoesPersonHaveActiveApplication(int ApplicationTypeID)
        {
            return clsApplication.DoesPersonHaveActiveApplication(this.ApplicantPersonID, ApplicationTypeID);
        }
        /// <summary>
        /// Retrieves the active application ID for a given person and application type.
        /// </summary>
        /// <param name="PersonID">The ID of the person.</param>
        /// <param name="ApplicationTypeID">The type of application.</param>
        /// <returns>The active application ID.</returns>
        public static int GetActiveApplicationID(int PersonID, clsApplication.enApplicationType ApplicationTypeID)
        {
            return clsApplicationData.GetActiveApplicationID(PersonID, (short)ApplicationTypeID);
        }
        /// <summary>
        /// Retrieves the active application ID for a specific license class.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the application.</param>
        /// <param name="ApplicationTypeID">The type of application.</param>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <returns>The active application ID for the specified license class.</returns>
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, clsApplication.enApplicationType ApplicationTypeID, int LicenseClassID)
        {
            return clsApplicationData.GetActiveApplicationIDForLicenseClass(PersonID, (short)ApplicationTypeID, LicenseClassID);
        }
        /// <summary>
        /// Retrieves the active application ID for the given application type associated with the current applicant person.
        /// </summary>
        /// <param name="ApplicationTypeID">The ID of the application type to retrieve the active application ID for.</param>
        /// <returns>The active application ID for the specified application type.</returns>
        public int GetActiveApplicationID(enApplicationType ApplicationTypeID)
        {
            return clsApplication.GetActiveApplicationID(this.ApplicantPersonID, ApplicationTypeID);
        }

    }
}
