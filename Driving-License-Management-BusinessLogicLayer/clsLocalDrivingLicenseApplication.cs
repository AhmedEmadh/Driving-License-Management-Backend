using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsLocalDrivingLicenseApplication : clsApplication
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;
        public int LocalDrivingLicenseApplicationID { set; get; }
        public int LicenseClassID { set; get; }
        public clsLicenseClass LicenseClassInfo;
        public string PersonFullName
        {
            get
            {
                return clsPerson.Find(ApplicantPersonID).FullName;
            }
        }
        /// <summary>
        /// Initializes a new instance of the clsLocalDrivingLicenseApplication class.
        /// </summary>
        /// <remarks>
        /// This constructor sets the LocalDrivingLicenseApplicationID and LicenseClassID to -1, 
        /// LicenseClassInfo to null, and Mode to enMode.AddNew.
        /// </remarks>
        public clsLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = -1;
            this.LicenseClassID = -1;
            LicenseClassInfo = null;
            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsLocalDrivingLicenseApplication class.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="ApplicationID">The ID of the application.</param>
        /// <param name="ApplicantPersonID">The ID of the applicant person.</param>
        /// <param name="ApplicationDate">The date of the application.</param>
        /// <param name="ApplicationTypeID">The type ID of the application.</param>
        /// <param name="ApplicationStatus">The status of the application.</param>
        /// <param name="LastStatusDate">The date of the last status update.</param>
        /// <param name="PaidFees">The paid fees for the application.</param>
        /// <param name="CreatedByUsereID">The ID of the user who created the application.</param>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        private clsLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int ApplicantPersonID,
            DateTime ApplicationDate, int ApplicationTypeID,
            enApplicationStatus ApplicationStatus, DateTime LastStatusDate,
            float PaidFees, int CreatedByUsereID, int LicenseClassID) : base(ApplicationID, ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUsereID)
        {
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.LicenseClassID = LicenseClassID;
            this.LicenseClassInfo = clsLicenseClass.Find(this.LicenseClassID);
            this.Mode = enMode.Update;
        }

        /// <summary>
        /// Adds a new local driving license application to the database.
        /// </summary>
        /// <returns>True if the application was added successfully, false otherwise.</returns>
        private bool _AddNewLocalDrivingLicenseApplication()
        {
            this.LocalDrivingLicenseApplicationID = clsLocalDrivingLicenseApplicationData.AddNewLocalDrivingLicenseApplication
                (this.ApplicationID, this.LicenseClassID);
            return (this.LocalDrivingLicenseApplicationID != -1);
        }
        /// <summary>
        /// Updates an existing local driving license application in the database.
        /// </summary>
        /// <returns>True if the local driving license application was updated successfully, false otherwise.</returns>
        private bool _UpdateLocalDrivingLicenseApplication()
        {
            return clsLocalDrivingLicenseApplicationData.UpdateLocalDrivingLicenseApplication
                (
                    this.LocalDrivingLicenseApplicationID, this.ApplicationID, this.LicenseClassID
                );

        }
        /// <summary>
        /// Finds a local driving license application by its ID.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application to find.</param>
        /// <returns>A clsLocalDrivingLicenseApplication object if found, otherwise null.</returns>
        public static clsLocalDrivingLicenseApplication FindByLocalDrivingAppLicenseID(int LocalDrivingLicenseApplicationID)
        {
            int ApplicationID = -1, LicenseClassID = -1;
            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByID
                (LocalDrivingLicenseApplicationID, ref ApplicationID, ref LicenseClassID);
            if (IsFound)
            {
                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);

                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);

            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Finds a local driving license application by its associated application ID.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to find.</param>
        /// <returns>A clsLocalDrivingLicenseApplication object if found, otherwise null.</returns>
        public static clsLocalDrivingLicenseApplication FindByApplicationID(int ApplicationID)
        {
            int LocalDrivingLicenseApplicationID = -1, LicenseClassID = -1;

            bool IsFound = clsLocalDrivingLicenseApplicationData.GetLocalDrivingLicenseApplicationInfoByApplicationID
                (ApplicationID, ref LocalDrivingLicenseApplicationID, ref LicenseClassID);


            if (IsFound)
            {

                clsApplication Application = clsApplication.FindBaseApplication(ApplicationID);


                return new clsLocalDrivingLicenseApplication(
                    LocalDrivingLicenseApplicationID, Application.ApplicationID,
                    Application.ApplicantPersonID,
                                     Application.ApplicationDate, Application.ApplicationTypeID,
                                    (enApplicationStatus)Application.ApplicationStatus, Application.LastStatusDate,
                                     Application.PaidFees, Application.CreatedByUserID, LicenseClassID);
            }
            else
                return null;

        }
        /// <summary>
        /// Saves the local driving license application to the database.
        /// </summary>
        /// <returns>True if the application was saved successfully, false otherwise.</returns>
        public new bool Save()
        {
            base.Mode = (clsApplication.enMode)Mode;
            if (!base.Save())
                return false;

            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewLocalDrivingLicenseApplication())
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateLocalDrivingLicenseApplication();
                default:
                    return false;
            }
        }
        /// <summary>
        /// Retrieves all local driving license applications from the database.
        /// </summary>
        /// <returns>A DataTable containing all local driving license applications.</returns>
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            return clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
        }
        public static List<clsLocalDrivingLicenseApplication> GetAllLocalDrivingLicenseApplicationsList()
        {
            DataTable dt = clsLocalDrivingLicenseApplicationData.GetAllLocalDrivingLicenseApplications();
            List<clsLocalDrivingLicenseApplication> LocalDrivingLicenseApplicationsList = new List<clsLocalDrivingLicenseApplication>();
            foreach (DataRow row in dt.Rows)
            {
                clsLocalDrivingLicenseApplication LocalDrivingLicenseApplication = FindByLocalDrivingAppLicenseID(Convert.ToInt32(row["LocalDrivingLicenseApplicationID"]));
                if (LocalDrivingLicenseApplication != null)
                {
                    LocalDrivingLicenseApplicationsList.Add(LocalDrivingLicenseApplication);
                }
            }
            return LocalDrivingLicenseApplicationsList;
        }
        /// <summary>
        /// Deletes the local driving license application and its base application.
        /// </summary>
        /// <returns>True if both the local driving license application and its base application were deleted successfully, false otherwise.</returns>
        public new bool Delete()
        {
            bool IsLocalDrivingApplicationDeleted = false;
            bool IsBaseApplicationDeleted = false;
            IsLocalDrivingApplicationDeleted = clsLocalDrivingLicenseApplicationData.DeleteLocalDrivingLicenseApplication(this.LocalDrivingLicenseApplicationID);
            if (!IsLocalDrivingApplicationDeleted)
            {
                return false;
            }
            IsBaseApplicationDeleted = base.Delete();
            return IsBaseApplicationDeleted;
        }
        /// <summary>
        /// Checks if the local driving license application has passed a specific test type.
        /// </summary>
        /// <param name="TestTypeID">The type of test to check.</param>
        /// <returns>True if the test has been passed, false otherwise.</returns>
        public bool DoesPassTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        /// <summary>
        /// Checks if the applicant has passed the required previous test for the given test type.
        /// </summary>
        /// <param name="CurrentTestType">The type of test to check for previous test requirements.</param>
        /// <returns>True if the applicant has passed the required previous test, false otherwise.</returns>
        public bool DoesPassPreviousTest(clsTestType.enTestType CurrentTestType)
        {

            switch (CurrentTestType)
            {
                case clsTestType.enTestType.VisionTest:
                    //in this case no required prvious test to pass.
                    return true;

                case clsTestType.enTestType.WrittenTest:
                    //Written Test, you cannot sechdule it before person passes the vision test.
                    //we check if pass visiontest 1.

                    return this.DoesPassTestType(clsTestType.enTestType.VisionTest);


                case clsTestType.enTestType.StreetTest:

                    //Street Test, you cannot sechdule it before person passes the written test.
                    //we check if pass Written 2.
                    return this.DoesPassTestType(clsTestType.enTestType.WrittenTest);

                default:
                    return false;
            }
        }
        /// <summary>
        /// Checks if a local driving license application has passed a specific test type.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The type of test to check.</param>
        /// <returns>True if the application has passed the test, false otherwise.</returns>
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesPassTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        /// <summary>
        /// Checks if the applicant has attended a specific test type.
        /// </summary>
        /// <param name="TestTypeID">The type of test to check.</param>
        /// <returns>True if the applicant has attended the test, false otherwise.</returns>
        public bool DoesAttendTestType(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.DoesAttendTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        /// <summary>
        /// Retrieves the total number of trials for a specific test type.
        /// </summary>
        /// <param name="TestTypeID">The type of test.</param>
        /// <returns>The total number of trials for the specified test type.</returns>
        public byte TotalTrialsPerTest(clsTestType.enTestType TestTypeID)
        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        /// <summary>
        /// Returns the total number of trials for a specific test type.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The type of test.</param>
        /// <returns>The total number of trials for the specified test type.</returns>
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        /// <summary>
        /// Checks if a driving license applicant has attended a specific test.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the driving license application.</param>
        /// <param name="TestTypeID">The type of test to check for attendance.</param>
        /// <returns>True if the applicant has attended the test, false otherwise.</returns>
        public static bool AttendedTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        /// <summary>
        /// Checks if the applicant has attended a test of the specified type.
        /// </summary>
        /// <param name="TestTypeID">The type of test to check for attendance.</param>
        /// <returns>True if the applicant has attended the test, false otherwise.</returns>
        public bool AttendedTest(clsTestType.enTestType TestTypeID)

        {
            return clsLocalDrivingLicenseApplicationData.TotalTrialsPerTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID) > 0;
        }
        /// <summary>
        /// Checks if there is an active scheduled test for the given LocalDrivingLicenseApplicationID and TestTypeID.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The type of test.</param>
        /// <returns>True if there is an active scheduled test, false otherwise.</returns>
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        /// <summary>
        /// Checks if there is an active scheduled test for the given test type.
        /// </summary>
        /// <param name="TestTypeID">The type of test to check for active scheduled tests.</param>
        /// <returns>True if there is an active scheduled test, false otherwise.</returns>
        public bool IsThereAnActiveScheduledTest(clsTestType.enTestType TestTypeID)

        {

            return clsLocalDrivingLicenseApplicationData.IsThereAnActiveScheduledTest(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);
        }
        /// <summary>
        /// Retrieves the last test taken by the applicant for the specified test type.
        /// </summary>
        /// <param name="TestTypeID">The type of test to retrieve the last test for.</param>
        /// <returns>The last test taken by the applicant for the specified test type, or null if no test is found.</returns>
        public clsTest GetLastTestPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTest.FindLastTestPerPersonAndLicenseClass(this.ApplicantPersonID, this.LicenseClassID, TestTypeID);
        }
        /// <summary>
        /// Returns the number of passed tests for the current local driving license application.
        /// </summary>
        /// <returns>byte: The number of passed tests.</returns>
        public byte GetPassedTestCount()
        {
            return clsTest.GetPassedTestCount(this.LocalDrivingLicenseApplicationID);
        }
        /// <summary>
        /// Returns the number of passed tests for a local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <returns>The number of passed tests for the specified local driving license application.</returns>
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTest.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        /// <summary>
        /// Checks if the local driving license application has passed all required tests.
        /// </summary>
        /// <returns>True if the application has passed all required tests, false otherwise.</returns>
        public bool PassedAllTests()
        {
            return clsTest.PassedAllTests(this.LocalDrivingLicenseApplicationID);
        }
        /// <summary>
        /// Checks if all tests have been passed for the given local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <returns>True if all tests have been passed, false otherwise.</returns>
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return clsTest.PassedAllTests(LocalDrivingLicenseApplicationID);
        }
        /// <summary>
        /// Issues a driving license for the first time. This method checks if a driver record exists for the applicant, 
        /// creates a new driver record if necessary, and then creates a new license record. 
        /// It returns the ID of the newly issued license if successful, or -1 if an error occurs.
        /// </summary>
        /// <param name="Notes">Additional notes for the license issuance.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the license.</param>
        /// <returns>The ID of the newly issued license, or -1 if an error occurs.</returns>
        public int IssueLicenseForTheFirstTime(string Notes, int CreatedByUserID)
        {
            int DriverID = -1;

            clsDriver Driver = clsDriver.FindByPersonID(this.ApplicantPersonID);

            if (Driver == null)
            {
                //we check if the driver already there for this person.
                Driver = new clsDriver();

                Driver.PersonID = this.ApplicantPersonID;
                Driver.CreatedByUserID = CreatedByUserID;
                if (Driver.Save())
                {
                    DriverID = Driver.DriverID;
                }
                else
                {
                    return -1;
                }
            }
            else
            {
                DriverID = Driver.DriverID;
            }
            //now we diver is there, so we add new licesnse

            clsLicense License = new clsLicense();
            License.ApplicationID = this.ApplicationID;
            License.DriverID = DriverID;
            License.LicenseClass = this.LicenseClassID;
            License.IssueDate = DateTime.Now;
            License.ExpirationDate = DateTime.Now.AddYears(this.LicenseClassInfo.DefaultValidityLength);
            License.Notes = Notes;
            License.PaidFees = this.LicenseClassInfo.ClassFees;
            License.IsActive = true;
            License.IssueReason = clsLicense.enIssueReason.FirstTime;
            License.CreatedByUserID = CreatedByUserID;

            if (License.Save())
            {
                //now we should set the application status to complete.
                this.SetComplete();

                return License.LicenseID;
            }

            else
                return -1;
        }
        /// <summary>
        /// Checks if a license has been issued for the current application.
        /// </summary>
        /// <returns>True if a license has been issued, false otherwise.</returns>
        public bool IsLicenseIssued()
        {
            return (GetActiveLicenseID() != -1);
        }
        /// <summary>
        /// Retrieves the active license ID associated with the current application.
        /// </summary>
        /// <returns>The active license ID if found, otherwise a default value.</returns>
        public int GetActiveLicenseID()
        {//this will get the license id that belongs to this application
            return clsLicense.GetActiveLicenseIDByPersonID(this.ApplicantPersonID, this.LicenseClassID);
        }

    }
}
