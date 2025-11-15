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
    /// Provides data access methods for licenses.
    /// </summary>
    public static class clsLicenseData
    {
        /// <summary>
        /// Retrieves the license information by License ID.
        /// </summary>
        /// <param name="LicenseID">The ID of the license to retrieve information for.</param>
        /// <param name="ApplicationID">The ID of the application associated with the license.</param>
        /// <param name="DriverID">The ID of the driver associated with the license.</param>
        /// <param name="LicenseClass">The class of the license.</param>
        /// <param name="IssueDate">The date the license was issued.</param>
        /// <param name="ExpirationDate">The date the license expires.</param>
        /// <param name="Notes">Any additional notes associated with the license.</param>
        /// <param name="PaidFees">The amount of fees paid for the license.</param>
        /// <param name="IsActive">A flag indicating whether the license is active.</param>
        /// <param name="IssueReason">The reason the license was issued.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the license record.</param>
        /// <returns>True if the license information was successfully retrieved; otherwise, false.</returns>
        public static bool GetLicenseInfoByID(int LicenseID, ref int ApplicationID, ref int DriverID, ref int LicenseClass, ref DateTime IssueDate, ref DateTime ExpirationDate, ref string Notes, ref float PaidFees, ref bool IsActive, ref byte IssueReason, ref int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationID = Convert.ToInt32(reader["ApplicationID"]);
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    LicenseClass = Convert.ToInt32(reader["LicenseClass"]);
                    IssueDate = Convert.ToDateTime(reader["IssueDate"]);
                    ExpirationDate = Convert.ToDateTime(reader["ExpirationDate"]);
                    Notes = reader["Notes"].ToString();
                    PaidFees = float.Parse(reader["PaidFees"].ToString());
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    IssueReason = Convert.ToByte(reader["IssueReason"]);
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
        /// Retrieves all licenses from the database.
        /// </summary>
        /// <returns>A DataTable containing all licenses.</returns>
        public static DataTable GetAllLicenses()
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Licenses";
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
            return Result;
        }
        /// <summary>
        /// Retrieves a DataTable of licenses associated with a specific driver.
        /// </summary>
        /// <param name="DriverID">The ID of the driver to retrieve licenses for.</param>
        /// <returns>A DataTable containing the licenses for the specified driver.</returns>
        public static DataTable GetDriverLicenses(int DriverID)
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Licenses WHERE DriverID = @DriverID";
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
            return Result;
        }
        /// <summary>
        /// Adds a new license to the database.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application associated with the license.</param>
        /// <param name="DriverID">The ID of the driver associated with the license.</param>
        /// <param name="LicenseClass">The class of the license.</param>
        /// <param name="IssueDate">The date the license was issued.</param>
        /// <param name="ExpirationDate">The date the license expires.</param>
        /// <param name="Notes">Any additional notes about the license.</param>
        /// <param name="PaidFees">The fees paid for the license.</param>
        /// <param name="IsActive">A flag indicating whether the license is active.</param>
        /// <param name="IssueReason">The reason the license was issued.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the license record.</param>
        /// <returns>The ID of the newly added license, or -1 if the operation fails.</returns>
        public static int AddNewLicense(int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Licenses (ApplicationID, DriverID, LicenseClass, IssueDate, ExpirationDate, Notes, PaidFees, IsActive, IssueReason, CreatedByUserID) VALUES (@ApplicationID, @DriverID, @LicenseClass, @IssueDate, @ExpirationDate, @Notes, @PaidFees, @IsActive, @IssueReason, @CreatedByUserID); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? (object)DBNull.Value : (object)Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                LicenseID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                LicenseID = -1;
            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }
        /// <summary>
        /// Updates an existing license in the database.
        /// </summary>
        /// <param name="LicenseID"></param>
        /// <param name="ApplicationID"></param>
        /// <param name="DriverID"></param>
        /// <param name="LicenseClass"></param>
        /// <param name="IssueDate"></param>
        /// <param name="ExpirationDate"></param>
        /// <param name="Notes"></param>
        /// <param name="PaidFees"></param>
        /// <param name="IsActive"></param>
        /// <param name="IssueReason"></param>
        /// <param name="CreatedByUserID"></param>
        /// <returns>True if the license was successfully updated; otherwise, false.</returns>
        public static bool UpdateLicense(int LicenseID, int ApplicationID, int DriverID, int LicenseClass, DateTime IssueDate, DateTime ExpirationDate, string Notes, float PaidFees, bool IsActive, byte IssueReason, int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Licenses SET ApplicationID = @ApplicationID, DriverID = @DriverID, LicenseClass = @LicenseClass, IssueDate = @IssueDate, ExpirationDate = @ExpirationDate, Notes = @Notes, PaidFees = @PaidFees, IsActive = @IsActive, IssueReason = @IssueReason, CreatedByUserID = @CreatedByUserID WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            command.Parameters.AddWithValue("@LicenseClass", LicenseClass);
            command.Parameters.AddWithValue("@IssueDate", IssueDate);
            command.Parameters.AddWithValue("@ExpirationDate", ExpirationDate);
            command.Parameters.AddWithValue("@Notes", string.IsNullOrEmpty(Notes) ? (object)DBNull.Value : Notes);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            command.Parameters.AddWithValue("@IssueReason", IssueReason);
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
        /// Retrieves the ID of an active license for a given person and license class.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the license.</param>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <returns>The ID of the active license, or -1 if not found.</returns>
        public static int GetActiveLicenseIDByPersonID(int PersonID, int LicenseClassID)
        {
            int LicenseID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            //string query = "SELECT LicenseID FROM Licenses WHERE DriverID = (SELECT DriverID FROM Drivers WHERE PersonID = @PersonID) AND LicenseClass = @LicenseClassID AND IsActive = 1";
            string query = @"SELECT Licenses.*,Drivers.PersonID FROM Licenses
                             INNER JOIN Drivers ON Licenses.DriverID = Drivers.DriverID 
                             WHERE Drivers.PersonID = @PersonID AND LicenseClass = @LicenseClassID AND IsActive = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = Convert.ToInt32(reader["LicenseID"]);
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                LicenseID = -1;
            }
            finally
            {
                connection.Close();
            }
            return LicenseID;
        }
        /// <summary>
        /// Deactivates a license in the database.
        /// </summary>
        /// <param name="LicenseID"></param>
        /// <returns>True if the license was successfully deactivated; otherwise, false.</returns>
        public static bool DeactivateLicense(int LicenseID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Licenses SET IsActive = 0 WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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

        public static bool DeleteLicense(int LicenseID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Licenses WHERE LicenseID = @LicenseID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
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
    }
}
