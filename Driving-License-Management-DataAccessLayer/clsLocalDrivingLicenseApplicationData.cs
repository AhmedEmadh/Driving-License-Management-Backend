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
    /// Provides data access methods for local driving license applications.
    /// </summary>
    public static class clsLocalDrivingLicenseApplicationData
    {
        /// <summary>
        /// Retrieves local driving license application information by ID.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application to retrieve.</param>
        /// <param name="ApplicationID">The ID of the application associated with the local driving license application.</param>
        /// <param name="LicenseClassID">The ID of the license class associated with the local driving license application.</param>
        /// <returns>True if the local driving license application information is retrieved successfully, false otherwise.</returns>
        public static bool GetLocalDrivingLicenseApplicationInfoByID(int LocalDrivingLicenseApplicationID, ref int ApplicationID, ref int LicenseClassID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                isSuccess = false;
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves local driving license application information by Application ID.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to retrieve local driving license application information for.</param>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application associated with the Application ID.</param>
        /// <param name="LicenseClassID">The ID of the license class associated with the local driving license application.</param>
        /// <returns>True if the local driving license application information is retrieved successfully, false otherwise.</returns>
        public static bool GetLocalDrivingLicenseApplicationInfoByApplicationID(int ApplicationID, ref int LocalDrivingLicenseApplicationID, ref int LicenseClassID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LocalDrivingLicenseApplicationID = Convert.ToInt32(reader["LocalDrivingLicenseApplicationID"]);
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    isSuccess = true;
                }
                else
                {
                    isSuccess = false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                isSuccess = false;
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves all local driving license applications from the database.
        /// </summary>
        /// <returns>A DataTable containing the local driving license applications.</returns>
        public static DataTable GetAllLocalDrivingLicenseApplications()
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LocalDrivingLicenseApplications_View";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Result.Load(reader);
                Result.Columns[0].DataType = typeof(int);
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                Result = null;
            }
            return Result;
        }
        /// <summary>
        /// Adds a new local driving license application to the database.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application.</param>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <returns>The ID of the newly added local driving license application, or -1 if the operation fails.</returns>
        public static int AddNewLocalDrivingLicenseApplication(int ApplicationID, int LicenseClassID)
        {
            int LocalDrivingLicenseApplicationID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO LocalDrivingLicenseApplications (ApplicationID, LicenseClassID) VALUES (@ApplicationID, @LicenseClassID); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                LocalDrivingLicenseApplicationID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                LocalDrivingLicenseApplicationID = -1;
            }
            finally
            {
                connection.Close();
            }
            return LocalDrivingLicenseApplicationID;
        }
        /// <summary>
        /// Updates an existing local driving license application in the database.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application to update.</param>
        /// <param name="ApplicationID">The ID of the application associated with the local driving license application.</param>
        /// <param name="LicenseClassID">The ID of the license class associated with the local driving license application.</param>
        /// <returns>True if the local driving license application is updated successfully, false otherwise.</returns>
        public static bool UpdateLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID, int ApplicationID, int LicenseClassID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE LocalDrivingLicenseApplications SET ApplicationID = @ApplicationID, LicenseClassID = @LicenseClassID WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isSuccess = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                isSuccess = false;
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Deletes a local driving license application from the database based on the provided LocalDrivingLicenseApplicationID.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application to delete.</param>
        /// <returns>True if the deletion is successful, false otherwise.</returns>
        public static bool DeleteLocalDrivingLicenseApplication(int LocalDrivingLicenseApplicationID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM LocalDrivingLicenseApplications WHERE LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isSuccess = rowsAffected > 0;
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                isSuccess = false;
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Checks if a local driving license application has passed a specific test type.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The ID of the test type to check.</param>
        /// <returns></returns>
        public static bool DoesPassTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                SELECT top 1 TestResult
                                FROM LocalDrivingLicenseApplications INNER JOIN
                                TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                WHERE
                                (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                                AND(TestAppointments.TestTypeID = @TestTypeID)
                                ORDER BY TestAppointments.TestAppointmentID desc
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    bool testResult = (bool)reader["TestResult"];
                    result = testResult;
                }
                else
                {
                    result = false;
                }
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
        /// Checks if a local driving license application has attended a specific test type.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <returns>True if the application has attended the test type, false otherwise.</returns>
        public static bool DoesAttendTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                SELECT TOP 1 FOUND=1 FROM LocalDrivingLicenseApplications INNER JOIN
                                TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                TESTS ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                WHERE LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID = @TestTypeID;
";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                result = Convert.ToBoolean(command.ExecuteScalar());
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
        /// Retrieves the total number of trials for a specific test type associated with a local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <returns>The total number of trials as a byte value.</returns>
        public static byte TotalTrialsPerTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            byte result = 0;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                    SELECT TotalTrialsPerTest = count(TestID)
                                    FROM LocalDrivingLicenseApplications INNER JOIN
                                         TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID INNER JOIN
                                         Tests ON TestAppointments.TestAppointmentID = Tests.TestAppointmentID
                                    WHERE
                                    (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                                    AND(TestAppointments.TestTypeID = @TestTypeID)
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                result = Convert.ToByte(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                result = 0;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Checks if there is an active scheduled test for a local driving license application.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <returns>True if there is an active scheduled test, false otherwise.</returns>
        public static bool IsThereAnActiveScheduledTest(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {

            bool Result = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @" 
                            SELECT top 1 Found=1
                            FROM LocalDrivingLicenseApplications INNER JOIN
                                 TestAppointments ON LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = TestAppointments.LocalDrivingLicenseApplicationID 
                            WHERE
                            (LocalDrivingLicenseApplications.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID)  
                            AND(TestAppointments.TestTypeID = @TestTypeID) and isLocked=0
                            ORDER BY TestAppointments.TestAppointmentID desc
";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);

            try
            {
                connection.Open();

                object result = command.ExecuteScalar();


                if (result != null)
                {
                    Result = true;
                }

            }

            catch (Exception ex)
            {
                clsLogger.Log(ex);

            }

            finally
            {
                connection.Close();
            }

            return Result;

        }
    }
}
