using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsTestAppointment
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;

        public int TestAppointmentID { set; get; }
        public clsTestType.enTestType TestTypeID { set; get; }
        public clsTestType TestTypeInfo { set; get; }
        int _LocalDrivingLicenseApplicationID;
        public int LocalDrivingLicenseApplicationID
        {
            set
            {
                LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(value);
                if (LocalDrivingLicenseApplicationInfo != null)
                    _LocalDrivingLicenseApplicationID = value;
                else
                    _LocalDrivingLicenseApplicationID = -1;
            }
            get { return _LocalDrivingLicenseApplicationID; }
        }
        public clsLocalDrivingLicenseApplication LocalDrivingLicenseApplicationInfo = null;
        public DateTime AppointmentDate { set; get; }
        public float PaidFees { set; get; }
        public int CreatedByUserID { set; get; }
        public bool IsLocked { set; get; }
        public int RetakeTestApplicationID { set; get; }
        public clsApplication RetakeTestAppInfo { set; get; }

        public int TestID
        {
            get { return _GetTestID(); }

        }
        /// <summary>
        /// Initializes a new instance of the clsTestAppointment class with default values.
        /// </summary>
        public clsTestAppointment()

        {
            this.TestAppointmentID = -1;
            this.TestTypeID = clsTestType.enTestType.VisionTest;
            this.AppointmentDate = DateTime.Now;
            this.PaidFees = 0;
            this.CreatedByUserID = -1;
            this.RetakeTestApplicationID = -1;
            Mode = enMode.AddNew;

            TestTypeInfo = clsTestType.Find((int)this.TestTypeID);
            LocalDrivingLicenseApplicationInfo = null;
        }
        /// <summary>
        /// Initializes a new instance of the clsTestAppointment class.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment.</param>
        /// <param name="TestTypeID">The type of test.</param>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="AppointmentDate">The date of the appointment.</param>
        /// <param name="PaidFees">The paid fees for the appointment.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the appointment.</param>
        /// <param name="IsLocked">A flag indicating whether the appointment is locked.</param>
        /// <param name="RetakeTestApplicationID">The ID of the retake test application.</param>
        public clsTestAppointment(int TestAppointmentID, clsTestType.enTestType TestTypeID,
           int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees,
           int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)

        {
            this.TestAppointmentID = TestAppointmentID;
            this.TestTypeID = TestTypeID;
            this.LocalDrivingLicenseApplicationID = LocalDrivingLicenseApplicationID;
            this.AppointmentDate = AppointmentDate;
            this.PaidFees = PaidFees;
            this.CreatedByUserID = CreatedByUserID;
            this.IsLocked = IsLocked;
            this.RetakeTestApplicationID = RetakeTestApplicationID;
            this.RetakeTestAppInfo = clsApplication.FindBaseApplication(RetakeTestApplicationID);

            TestTypeInfo = clsTestType.Find((int)this.TestTypeID);
            LocalDrivingLicenseApplicationInfo = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(this.LocalDrivingLicenseApplicationID);
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new test appointment to the data storage.
        /// </summary>
        /// <returns>True if the test appointment was added successfully, false otherwise.</returns>
        private bool _AddNewTestAppointment()
        {
            //call DataAccess Layer 

            this.TestAppointmentID = clsTestAppointmentData.AddNewTestAppointment((int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.RetakeTestApplicationID);

            return (this.TestAppointmentID != -1);
        }
        /// <summary>
        /// Updates an existing test appointment in the database.
        /// </summary>
        /// <returns>True if the test appointment was updated successfully, false otherwise.</returns>
        private bool _UpdateTestAppointment()
        {
            //call DataAccess Layer 

            return clsTestAppointmentData.UpdateTestAppointment(this.TestAppointmentID, (int)this.TestTypeID, this.LocalDrivingLicenseApplicationID,
                this.AppointmentDate, this.PaidFees, this.CreatedByUserID, this.IsLocked, this.RetakeTestApplicationID);
        }
        /// <summary>
        /// Finds a test appointment by its ID.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment to find.</param>
        /// <returns>A clsTestAppointment object if found, otherwise null.</returns>
        public static clsTestAppointment Find(int TestAppointmentID)
        {
            int TestTypeID = 1; int LocalDrivingLicenseApplicationID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.GetTestAppointmentInfoByID(TestAppointmentID, ref TestTypeID, ref LocalDrivingLicenseApplicationID,
            ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, (clsTestType.enTestType)TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }
        /// <summary>
        /// Retrieves the last test appointment for a specific local driving license application and test type.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The type of test for which to retrieve the last appointment.</param>
        /// <returns>The last test appointment if found, otherwise null.</returns>
        public static clsTestAppointment GetLastTestAppointment(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            int TestAppointmentID = -1;
            DateTime AppointmentDate = DateTime.Now; float PaidFees = 0;
            int CreatedByUserID = -1; bool IsLocked = false; int RetakeTestApplicationID = -1;

            if (clsTestAppointmentData.GetLastTestAppointment(LocalDrivingLicenseApplicationID, (int)TestTypeID,
                ref TestAppointmentID, ref AppointmentDate, ref PaidFees, ref CreatedByUserID, ref IsLocked, ref RetakeTestApplicationID))

                return new clsTestAppointment(TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID,
             AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID);
            else
                return null;

        }
        /// <summary>
        /// Retrieves all test appointments from the database.
        /// </summary>
        /// <returns>A DataTable containing the test appointments.</returns>
        public static DataTable GetAllTestAppointments()
        {
            return clsTestAppointmentData.GetAllTestAppointments();

        }
        /// <summary>
        /// Retrieves all test appointments for a specific local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <returns>A DataTable containing the test appointments.</returns>
        public static List<clsTestAppointment> GetAllTestAppointmentsList()
        {
            DataTable dt = clsTestAppointmentData.GetAllTestAppointments();
            List<clsTestAppointment> list = new List<clsTestAppointment>();
            foreach (DataRow dr in dt.Rows)
            {
                list.Add(Find(Convert.ToInt32(dr["TestAppointmentID"])));
            }
            return list;
        }
        public DataTable GetApplicationTestAppointmentsPerTestType(clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(this.LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        /// <summary>
        /// Retrieves all test appointments for a specific local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <returns>A DataTable containing the test appointments.</returns>
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, clsTestType.enTestType TestTypeID)
        {
            return clsTestAppointmentData.GetApplicationTestAppointmentsPerTestType(LocalDrivingLicenseApplicationID, (int)TestTypeID);

        }
        /// <summary>
        /// Saves the test appointment to the database.
        /// </summary>
        /// <returns>True if the test appointment was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestAppointment())
                    {

                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:

                    return _UpdateTestAppointment();

            }

            return false;
        }
        /// <summary>
        /// Retrieves the ID of the test associated with the test appointment.
        /// </summary>
        /// <returns>The ID of the test.</returns>
        private int _GetTestID()
        {
            return clsTestAppointmentData.GetTestID(TestAppointmentID);
        }
    }
}
