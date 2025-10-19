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
    /// Provides data access methods for test appointments.
    /// </summary>
    public static class clsTestAppointmentData
    {
        /// <summary>
        /// Retrieves the test appointment information by TestAppointmentID.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="AppointmentDate">The appointment date.</param>
        /// <param name="PaidFees">The paid fees.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test appointment.</param>
        /// <param name="IsLocked">A flag indicating whether the test appointment is locked.</param>
        /// <param name="RetakeTestApplicationID">The ID of the retake test application.</param>
        /// <returns>True if the test appointment information is successfully retrieved; otherwise, false.</returns>
        public static bool GetTestAppointmentInfoByID(int TestAppointmentID, ref int TestTypeID, ref int LocalDrivingLicenseApplicationID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsSuccess = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand("SELECT TestAppointmentID, TestTypeID, LocalDrivingLicenseApplicationID, AppointmentDate, PaidFees, CreatedByUserID, IsLocked, RetakeTestApplicationID FROM TestAppointments WHERE TestAppointmentID = @TestAppointmentID", conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TestTypeID = reader.GetInt32(1);
                    LocalDrivingLicenseApplicationID = reader.GetInt32(2);
                    AppointmentDate = reader.GetDateTime(3);
                    PaidFees = (float)reader.GetDecimal(4);
                    CreatedByUserID = reader.GetInt32(5);
                    IsLocked = reader.GetBoolean(6);
                    RetakeTestApplicationID = reader.IsDBNull(7) ? -1 : reader.GetInt32(7);

                    IsSuccess = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return IsSuccess;
        }
        /// <summary>
        /// Retrieves the last test appointment information for a given local driving license application ID and test type ID.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <param name="TestAppointmentID">The ID of the test appointment.</param>
        /// <param name="AppointmentDate">The date of the appointment.</param>
        /// <param name="PaidFees">The paid fees for the test appointment.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the test appointment.</param>
        /// <param name="IsLocked">A flag indicating whether the test appointment is locked.</param>
        /// <param name="RetakeTestApplicationID">The ID of the retake test application.</param>
        /// <returns>True if the last test appointment information is successfully retrieved; otherwise, false.</returns>
        public static bool GetLastTestAppointment(int LocalDrivingLicenseApplicationID, int TestTypeID, ref int TestAppointmentID, ref DateTime AppointmentDate, ref float PaidFees, ref int CreatedByUserID, ref bool IsLocked, ref int RetakeTestApplicationID)
        {
            bool IsSuccess = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                SELECT       top 1 *
                                FROM            TestAppointments
                                WHERE        (TestTypeID = @TestTypeID) 
                                AND (LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID) 
                                order by TestAppointmentID Desc
";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TestAppointmentID = reader["TestAppointmentID"] is DBNull ? -1 : (int)reader["TestAppointmentID"];
                    AppointmentDate = reader["AppointmentDate"] is DBNull ? DateTime.MaxValue : (DateTime)reader["AppointmentDate"];
                    PaidFees = reader["PaidFees"] is DBNull ? -1f : Convert.ToSingle(reader["PaidFees"]);
                    CreatedByUserID = reader["CreatedByUserID"] is DBNull ? -1 : (int)reader["CreatedByUserID"];
                    IsLocked = reader["IsLocked"] is DBNull ? false : (bool)reader["IsLocked"];
                    RetakeTestApplicationID = reader["RetakeTestApplicationID"] is DBNull ? -1 : (int)reader["RetakeTestApplicationID"];
                    IsSuccess = true;
                }
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return IsSuccess;
        }
        /// <summary>
        /// Retrieves all test appointments from the database.
        /// </summary>
        /// <returns>A DataTable containing the test appointments.</returns>
        public static DataTable GetAllTestAppointments()
        {
            DataTable testAppointments = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestAppointments";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                testAppointments.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return testAppointments;
        }
        /// <summary>
        /// Retrieves all test appointments for a given local driving license application ID and test type ID.
        /// </summary>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <returns>A DataTable containing the test appointments.</returns>
        public static DataTable GetApplicationTestAppointmentsPerTestType(int LocalDrivingLicenseApplicationID, int TestTypeID)
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                SELECT TestAppointmentID,AppointmentDate,PaidFees,IsLocked FROM TestAppointments INNER JOIN TestTypes ON TestAppointments.TestTypeID = TestTypes.TestTypeID WHERE TestAppointments.LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID AND TestAppointments.TestTypeID = @Test
                                ORDER BY TestAppointments.AppointmentDate DESC";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@Test", TestTypeID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return dataTable;
        }
        /// <summary>
        /// Adds a new test appointment to the database and returns the ID of the newly added appointment.
        /// </summary>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="AppointmentDate">The date of the appointment.</param>
        /// <param name="PaidFees">The paid fees for the appointment.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the appointment.</param>
        /// <param name="RetakeTestApplicationID">The ID of the retake test application, or -1 if not applicable.</param>
        /// <returns>The ID of the newly added test appointment, or -1 if the operation fails.</returns>
        public static int AddNewTestAppointment(int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, int RetakeTestApplicationID)
        {
            int TestAppointmentID = -1;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = @"Insert Into TestAppointments (TestTypeID,LocalDrivingLicenseApplicationID,AppointmentDate,PaidFees,CreatedByUserID,IsLocked,RetakeTestApplicationID)
                            Values (@TestTypeID,@LocalDrivingLicenseApplicationID,@AppointmentDate,@PaidFees,@CreatedByUserID,0,@RetakeTestApplicationID);
                
                            SELECT SCOPE_IDENTITY();";

            SqlCommand command = new SqlCommand(query, connection);


            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            command.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);

            if (RetakeTestApplicationID == -1)

                command.Parameters.AddWithValue("@RetakeTestApplicationID", DBNull.Value);
            else
                command.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID);





            try
            {
                connection.Open();

                object result = command.ExecuteScalar();

                if (result != null && int.TryParse(result.ToString(), out int insertedID))
                {
                    TestAppointmentID = insertedID;
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


            return TestAppointmentID;

        }
        /// <summary>
        /// Updates an existing test appointment in the database.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment to update.</param>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <param name="LocalDrivingLicenseApplicationID">The ID of the local driving license application.</param>
        /// <param name="AppointmentDate">The date of the appointment.</param>
        /// <param name="PaidFees">The paid fees for the appointment.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the appointment.</param>
        /// <param name="IsLocked">A flag indicating whether the test appointment is locked.</param>
        /// <param name="RetakeTestApplicationID">The ID of the retake test application, or -1 if not applicable.</param>
        /// <returns>True if the test appointment is successfully updated; otherwise, false.</returns>
        public static bool UpdateTestAppointment(int TestAppointmentID, int TestTypeID, int LocalDrivingLicenseApplicationID, DateTime AppointmentDate, float PaidFees, int CreatedByUserID, bool IsLocked, int RetakeTestApplicationID)
        {
            bool IsSuccess = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand("UPDATE TestAppointments SET TestTypeID = @TestTypeID, LocalDrivingLicenseApplicationID = @LocalDrivingLicenseApplicationID, AppointmentDate = @AppointmentDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID, IsLocked = @IsLocked, RetakeTestApplicationID = @RetakeTestApplicationID WHERE TestAppointmentID = @TestAppointmentID", conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            cmd.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            cmd.Parameters.AddWithValue("@LocalDrivingLicenseApplicationID", LocalDrivingLicenseApplicationID);
            cmd.Parameters.AddWithValue("@AppointmentDate", AppointmentDate);
            cmd.Parameters.AddWithValue("@PaidFees", PaidFees);
            cmd.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            cmd.Parameters.AddWithValue("@IsLocked", IsLocked);
            cmd.Parameters.AddWithValue("@RetakeTestApplicationID", RetakeTestApplicationID != -1 ? RetakeTestApplicationID : (object)DBNull.Value);
            try
            {
                conn.Open();
                int rowsAffected = cmd.ExecuteNonQuery();
                IsSuccess = rowsAffected > 0;

            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return IsSuccess;
        }
        /// <summary>
        /// Retrieves the TestID associated with a given TestAppointmentID.
        /// </summary>
        /// <param name="TestAppointmentID">The ID of the test appointment.</param>
        /// <returns>The ID of the test, or -1 if no test is found.</returns>
        public static int GetTestID(int TestAppointmentID)
        {
            int TestID = -1;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            SqlCommand cmd = new SqlCommand("select TestID from Tests where TestAppointmentID=@TestAppointmentID", conn);
            cmd.Parameters.AddWithValue("@TestAppointmentID", TestAppointmentID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    TestID = reader.GetInt32(0);
                }
                reader.Close();

            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return TestID;
        }
    }
}
