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
    /// Provides data access methods for driver information.
    /// </summary>
    public static class clsDriverData
    {
        /// <summary>
        /// Retrieves driver information by DriverID.
        /// </summary>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <param name="PersonID">The ID of the person associated with the driver.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the driver record.</param>
        /// <param name="CreatedDate">The date the driver record was created.</param>
        /// <returns>True if the driver information is retrieved successfully, false otherwise.</returns>
        public static bool GetDriverInfoByDriverID(int DriverID, ref int PersonID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
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
        /// Retrieves driver information by PersonID.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the driver.</param>
        /// <param name="DriverID">The ID of the driver.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the driver record.</param>
        /// <param name="CreatedDate">The date the driver record was created.</param>
        /// <returns>True if the driver information is retrieved successfully, false otherwise.</returns>
        public static bool GetDriverInfoByPersonID(int PersonID, ref int DriverID, ref int CreatedByUserID, ref DateTime CreatedDate)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    DriverID = Convert.ToInt32(reader["DriverID"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    CreatedDate = Convert.ToDateTime(reader["CreatedDate"]);
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
        /// Retrieves all drivers from the database.
        /// </summary>
        /// <returns>A DataTable containing all drivers.</returns>
        public static DataTable GetAllDrivers()
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Drivers_View";
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
        /// Adds a new driver to the database and returns the ID of the added driver.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the driver.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the driver record.</param>
        /// <returns>The ID of the added driver, or -1 if the operation fails.</returns>
        public static int AddNewDriver(int PersonID, int CreatedByUserID)
        {
            int DriverID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Drivers (PersonID, CreatedByUserID, CreatedDate) VALUES (@PersonID, @CreatedByUserID, @CreatedDate); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@CreatedDate", DateTime.Now);
            try
            {
                connection.Open();
                DriverID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                DriverID = -1;
            }
            finally
            {
                connection.Close();
            }
            return DriverID;
        }
        /// <summary>
        /// Updates an existing driver record in the database.
        /// </summary>
        /// <param name="DriverID">The ID of the driver to be updated.</param>
        /// <param name="PersonID">The ID of the person associated with the driver.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the driver record.</param>
        /// <returns>True if the driver record is updated successfully, false otherwise.</returns>
        public static bool UpdateDriver(int DriverID, int PersonID, int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Drivers SET PersonID = @PersonID, CreatedByUserID = @CreatedByUserID WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@DriverID", DriverID);
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
        /// Deletes a driver record from the database based on the specified driver ID.
        /// </summary>
        /// <remarks>This method attempts to delete the driver record with the specified <paramref
        /// name="DriverId"/> from the database. If the specified driver ID does not exist, the method returns <see
        /// langword="false"/> without throwing an exception.</remarks>
        /// <param name="DriverId">The unique identifier of the driver to be deleted. Must correspond to an existing driver record in the
        /// database.</param>
        /// <returns><see langword="true"/> if the driver record was successfully deleted; otherwise, <see langword="false"/>.</returns>
        public static bool DeleteDriver(int DriverId)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Drivers WHERE DriverID = @DriverID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DriverID", DriverId);
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
