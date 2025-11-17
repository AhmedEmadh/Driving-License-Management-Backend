using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsTest
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestID { set; get; }
        public int TestAppointmentID { set; get; }
        public clsTestAppointment TestAppointmentInfo { set; get; }
        public bool TestResult { set; get; }
        public string Notes { set; get; }
        public int CreatedByUserID { set; get; }
        /// <summary>
        /// Initializes a new instance of the clsTest class, setting all properties to their default values.
        /// </summary>
        public clsTest()

        {
            this.TestID = -1;
            this.TestAppointmentID = -1;
            this.TestResult = false;
            this.Notes = "";
            this.CreatedByUserID = -1;

            Mode = enMode.AddNew;

        }
        /// <summary>
        /// Initializes a new instance of the clsTest class.
        /// </summary>
        /// <param name="TestID">The ID of the test.</param>
        /// <param name="TestAppointmentID">The ID of the test appointment.</param>
        /// <param name="TestResult">The result of the test.</param>
        /// <param name="Notes">Any notes related to the test.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test.</param>
        /// <returns>A new instance of the clsTest class.</returns>
        public clsTest(int TestID, int TestAppointmentID,
            bool TestResult, string Notes, int CreatedByUserID)

        {
            this.TestID = TestID;
            this.TestAppointmentID = TestAppointmentID;
            this.TestAppointmentInfo = clsTestAppointment.Find(TestAppointmentID);
            this.TestResult = TestResult;
            this.Notes = Notes;
            this.CreatedByUserID = CreatedByUserID;

            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new test to the data storage.
        /// </summary>
        /// <returns>True if the test was added successfully, false otherwise.</returns>
        private bool _AddNewTest()
        {
            //call DataAccess Layer 

            this.TestID = clsTestData.AddNewTest(this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);


            return (this.TestID != -1);
        }
        /// <summary>
        /// Updates an existing test in the database.
        /// </summary>
        /// <returns>True if the test was updated successfully, false otherwise.</returns>
        private bool _UpdateTest()
        {
            //call DataAccess Layer 

            return clsTestData.UpdateTest(this.TestID, this.TestAppointmentID,
                this.TestResult, this.Notes, this.CreatedByUserID);
        }
        /// <summary>
        /// Finds a test by its ID.
        /// </summary>
        /// <param name="TestID">The ID of the test to find.</param>
        /// <returns>A clsTest object if found, otherwise null.</returns>
        public static clsTest Find(int TestID)
        {
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestData.GetTestInfoByID(TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }
        /// <summary>
        /// Finds a test by its test appointment ID.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment to find.</param>
        /// <returns>A clsTest object if found, otherwise null.</returns>
        public static clsTest FindByTestAppointmentID(int TestAppointmentID)
        {
            int TestID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;
            if (clsTestData.GetTestInfoByAppointmentID(TestAppointmentID,
            ref TestID, ref TestResult,
            ref Notes, ref CreatedByUserID))
                return new clsTest(TestID,
                            TestAppointmentID, TestResult,
                            Notes, CreatedByUserID);
            else
                return null;
        }
        /// <summary>
        /// Finds the last test taken by a person for a specific license class and test type.
        /// </summary>
        /// <param name="PersonID">The ID of the person who took the test.</param>
        /// <param name="LicenseClassID">The ID of the license class for which the test was taken.</param>
        /// <param name="TestTypeID">The type of test that was taken.</
        public static clsTest FindLastTestPerPersonAndLicenseClass
            (int PersonID, int LicenseClassID, clsTestType.enTestType TestTypeID)
        {
            int TestID = -1;
            int TestAppointmentID = -1;
            bool TestResult = false; string Notes = ""; int CreatedByUserID = -1;

            if (clsTestData.GetLastTestByPersonAndTestTypeAndLicenseClass
                (PersonID, LicenseClassID, (int)TestTypeID, ref TestID,
            ref TestAppointmentID, ref TestResult,
            ref Notes, ref CreatedByUserID))

                return new clsTest(TestID,
                        TestAppointmentID, TestResult,
                        Notes, CreatedByUserID);
            else
                return null;

        }
        /// <summary>
        /// Retrieves all tests from the database.
        /// </summary>
        /// <returns>A DataTable containing all tests.</returns>
        public static DataTable GetAllTests()
        {
            return clsTestData.GetAllTests();

        }
        public static List<clsTest> GetAllTestsList()
        {
            DataTable dt = GetAllTests();
            List<clsTest> tests = new List<clsTest>();
            foreach (DataRow row in dt.Rows)
            {
                clsTest test = new clsTest(
                    Convert.ToInt32(row["TestID"]),
                    Convert.ToInt32(row["TestAppointmentID"]),
                    Convert.ToBoolean(row["TestResult"]),
                    row["Notes"].ToString(),
                    Convert.ToInt32(row["CreatedByUserID"])
                );
                tests.Add(test);
            }
            return tests;
        }
        /// <summary>
        /// Saves the current test to the database based on its current mode.
        /// </summary>
        /// <returns>True if the test was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTest())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTest();

            }

            return false;
        }
        /// <summary>
        /// Retrieves the number of passed tests for a local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <returns>The number of passed tests for the specified local driving license application.</returns>
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            return clsTestData.GetPassedTestCount(LocalDrivingLicenseApplicationID);
        }
        /// <summary>
        /// Checks if the local driving license application has passed all required tests.
        /// </summary>
        /// <returns>True if the application has passed all required tests, false otherwise.</returns>
        public static bool PassedAllTests(int LocalDrivingLicenseApplicationID)
        {
            //if total passed test less than 3 it will return false otherwise will return true
            return GetPassedTestCount(LocalDrivingLicenseApplicationID) == 3;
        }
        public bool Delete()
        {
            return clsTestData.DeleteTest(TestID);
        }
    }
}
