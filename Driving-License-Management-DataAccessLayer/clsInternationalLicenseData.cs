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
    /// Provides data access operations for international licenses.
    /// </summary>
    public static class clsInternationalLicenseData
    {
        /// <summary>
        /// Retrieves international license information by ID.
        /// </summary>
        /// <param name="InternationalLicenseID">The ID of the international license to retrieve.</param>
        /// <param name="ApplicationID">The ID of the application associated with the international license.</param>
        /// <param name="DriverID">The ID of the driver associated with the international license.</param>
        /// <param name="IssuedUsingLocalLicenseID">The ID of the local license used to issue the international license.</param>
        /// <param name="IssueDate">The date the international license was issued.</param>
        /// <param name="ExpirationDate">The date the international license expires.</param>
        /// <param name="IsActive">A flag indicating whether the international license is active.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the international license record.</param>
        /// <returns>True if the international license information is retrieved successfully, false otherwise.</returns>
        public static bool GetInternationalLicenseInfoByID(int InternationalLicenseID, ref int ApplicationID, ref int DriverID, ref int IssuedUsingLocalLicenseID, ref DateTime IssueDate, ref DateTime ExpirationDate, ref bool IsActive, ref int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM InternationalLicenses WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    IssuedUsingLocalLicenseID = Convert.ToInt32(reader["IssuedUsingLocalLicenseID"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
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
        /// Retrieves all international licenses from the database.
        /// </summary>
        /// <returns>A DataTable containing all international licenses.</returns>
        public static DataTable GetAllInternationalLicenses()
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM InternationalLicenses";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Result.Load(reader);
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                Result = null;
            }
            finally
            {
                connection.Close();
            }
            return Result;
        }
        /// <summary>
        /// Retrieves a DataTable of international licenses associated with a specific driver.
        /// </summary>
        /// <param name="DriverID">The ID of the driver to retrieve international licenses for.</param>
        /// <returns>A DataTable containing the international licenses for the specified driver.</returns>
        public static DataTable GetDriverInternationalLicenses(int DriverID)
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM InternationalLicenses WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Result.Load(reader);
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                Result = null;
            }
            finally
            {
                connection.Close();
            }
            return Result;
        }
        /// <summary>
        /// Adds a new international license to the database.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application associated with the international license.</param>
        /// <param name="DriverID">The ID of the driver associated with the international license.</param>
        /// <param name="IssuedUsingLocalLicenseID">The ID of the local license used to issue the international license.</param>
        /// <param name="IssueDate">The date the international license was issued.</param>
        /// <param name="ExpirationDate">The date the international license expires.</param>
        /// <param name="IsActive">A flag indicating whether the international license is active.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the international license record.</param>
        /// <returns>The ID of the newly added international license, or -1 if the operation fails.</returns>
        public static int AddNewInternationalLicense(int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO InternationalLicenses (ApplicationID, DriverID, IssuedUsingLocalLicenseID, IssueDate, ExpirationDate, IsActive, CreatedByUserID) VALUES (@ApplicationID, @DriverID, @IssuedUsingLocalLicenseID, @IssueDate, @ExpirationDate, @IsActive, @CreatedByUserID); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                ID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                ID = -1;
            }
            finally
            {
                connection.Close();
            }
            return ID;
        }
        /// <summary>
        /// Updates an existing international license in the database.
        /// </summary>
        /// <param name="InternationalLicenseID">The ID of the international license to update.</param>
        /// <param name="ApplicationID">The ID of the application associated with the international license.</param>
        /// <param name="DriverID">The ID of the driver associated with the international license.</param>
        /// <param name="IssuedUsingLocalLicenseID">The ID of the local license used to issue the international license.</param>
        /// <param name="IssueDate">The date the international license was issued.</param>
        /// <param name="ExpirationDate">The date the international license expires.</param>
        /// <param name="IsActive">A flag indicating whether the international license is active.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the international license record.</param>
        /// <returns>True if the international license is updated successfully, false otherwise.</returns>
        public static bool UpdateInternationalLicense(int InternationalLicenseID, int ApplicationID, int DriverID, int IssuedUsingLocalLicenseID, DateTime IssueDate, DateTime ExpirationDate, bool IsActive, int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE InternationalLicenses SET ApplicationID = @ApplicationID, DriverID = @DriverID, IssuedUsingLocalLicenseID = @IssuedUsingLocalLicenseID, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, IsActive = @IsActive, CreatedByUserID = @CreatedByUserID WHERE InternationalLicenseID = @InternationalLicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@InternationalLicenseID", InternationalLicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@IssuedUsingLocalLicenseID", IssuedUsingLocalLicenseID);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
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
        /// Retrieves the ID of an active international license for a given driver.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <returns>The ID of the active international license, or -1 if not found.</returns>
        public static int GetActiveInternationalLicenseIDByDriverID(int DriverID)
        {
            int ID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT InternationalLicenseID FROM InternationalLicenses WHERE DriverID = @DriverID AND IsActive = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                ID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                ID = -1;
            }
            finally
            {
                connection.Close();
            }
            return ID;
        }
    }
}
