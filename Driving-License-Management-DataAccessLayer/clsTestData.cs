using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_DataAccessLayer
{
    /// <summary>
    /// Provides methods for managing test data.
    /// </summary>
    public static class clsTestData
    {
        /// <summary>
        /// Retrieves the test information by TestID.
        /// </summary>
        /// <param name="TestID">The ID of the test.</param>
        /// <param name="TestAppointmentID">The ID of the test appointment.</param>
        /// <param name="TestResult">The result of the test.</param>
        /// <param name="Notes">The notes of the test.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test.</param>
        /// <returns>True if the test information is successfully retrieved; otherwise, false.</returns>
        public static bool GetTestInfoByID(int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Tests WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    TestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                    TestResult = reader.GetBoolean(reader.GetOrdinal("TestResult"));
                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                    result = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Retrieves the test information by TestAppointmentID.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment.</param>
        /// <param name="TestID">The ID of the test, passed by reference.</param>
        /// <param name="TestResult">The result of the test, passed by reference.</param>
        /// <param name="Notes">The notes of the test, passed by reference.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test, passed by reference.</param>
        /// <returns>True if the test information is successfully retrieved; otherwise, false.</returns>
        public static bool GetTestInfoByAppointmentID(int TestAppointmentID, ref int TestID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Tests WHERE TestAppointmentID = @TestAppointmentID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    TestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                    TestResult = reader.GetBoolean(reader.GetOrdinal("TestResult"));
                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                    TestID = reader.GetInt32(reader.GetOrdinal("TestID"));
                    result = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Retrieves the last test by person, test type, and license class.
        /// </summary>
        /// <param name="PersonID">The ID of the person.</param>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <param name="TestID">The ID of the test, passed by reference.</param>
        /// <param name="TestAppointmentID">The ID of the test appointment, passed by reference.</param>
        /// <param name="TestResult">The result of the test, passed by reference.</param>
        /// <param name="Notes">The notes of the test, passed by reference.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test, passed by reference.</param>
        /// <returns>True if the last test is successfully retrieved; otherwise, false.</returns>
        public static bool GetLastTestByPersonAndTestTypeAndLicenseClass(int PersonID, int LicenseClassID, int TestTypeID, ref int TestID, ref int TestAppointmentID, ref bool TestResult, ref string Notes, ref int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                SELECT  top 1 Tests.TestID, 
                                Tests.TestAppointmentID, Tests.TestResult, 
			                    Tests.Notes, Tests.CreatedByUserID, Applications.ApplicantPersonID
                                FROM            
                                LocalDrivingLicenseApplications INNER JOIN
                                Tests INNER JOIN
                                TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                Applications ON LocalDrivingLicenseApplications.ApplicationID = Applications.ApplicationID
                                WHERE
                                (Applications.ApplicantPersonID = @PersonID) 
                                AND (LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID)
                                AND ( TestAppointments.TestTypeID=@TestTypeID)
                                ORDER BY Tests.TestAppointmentID DESC
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Read();
                    TestID = reader.GetInt32(reader.GetOrdinal("TestID"));
                    TestAppointmentID = reader.GetInt32(reader.GetOrdinal("TestAppointmentID"));
                    TestResult = reader.GetBoolean(reader.GetOrdinal("TestResult"));
                    Notes = reader.IsDBNull(reader.GetOrdinal("Notes")) ? null : reader.GetString(reader.GetOrdinal("Notes"));
                    CreatedByUserID = reader.GetInt32(reader.GetOrdinal("CreatedByUserID"));
                    isSuccess = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves all tests from the database.
        /// </summary>
        /// <returns>A DataTable containing all tests.</returns>
        public static DataTable GetAllTests()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Tests";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    dataTable.Load(reader);
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return dataTable;
        }
        /// <summary>
        /// Adds a new test to the database and returns the ID of the newly added test.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment associated with the test.</param>
        /// <param name="TestResult">The result of the test.</param>
        /// <param name="Notes">Any additional notes about the test.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test record.</param>
        /// <returns>The ID of the newly added test, or -1 if the operation fails.</returns>
        public static int AddNewTest(int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            int IDOfAddedTest = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Tests (TestAppointmentID, TestResult, Notes, CreatedByUserID) VALUES (@TestAppointmentID, @TestResult, @Notes, @CreatedByUserID); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                IDOfAddedTest = Convert.ToInt32(command.ExecuteScalar());
                connection.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return IDOfAddedTest;
        }
        /// <summary>
        /// Updates an existing test record in the database.
        /// </summary>
        /// <param name="TestID">The ID of the test to be updated.</param>
        /// <param name="TestAppointmentID">The ID of the test appointment associated with the test.</param>
        /// <param name="TestResult">The result of the test.</param>
        /// <param name="Notes">Any additional notes about the test.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test record.</param>
        /// <returns>True if the test record is updated successfully, false otherwise.</returns>
        public static bool UpdateTest(int TestID, int TestAppointmentID, bool TestResult, string Notes, int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Tests SET TestAppointmentID = @TestAppointmentID, TestResult = @TestResult, Notes = @Notes, CreatedByUserID = @CreatedByUserID WHERE TestID = @TestID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestID", TestID);
            command.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            command.Parameters.AddWithValue("@TestResult", TestResult);
            command.Parameters.AddWithValue("@Notes", Notes);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isSuccess = (bool)(rowsAffected > 0);
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves the total number of passed tests for a local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <returns>The total number of passed tests as a byte value.</returns>
        public static byte GetPassedTestCount(int LocalDrivingLicenseApplicationID)
        {
            byte PassedTestCount = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                SELECT PassedTestCount = count(TestTypeID)
                                FROM Tests INNER JOIN
                                TestAppointments ON Tests.TestAppointmentID = TestAppointments.TestAppointmentID
						        where LocalDrivingLicenseApplicationID =@LocalDrivingLicenseApplicationID and TestResult=1
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                PassedTestCount = Convert.ToByte(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return PassedTestCount;
        }

    }
}
