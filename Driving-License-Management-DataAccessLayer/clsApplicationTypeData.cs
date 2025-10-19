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
    /// This class provides data access methods for application types.
    /// </summary>
    public class clsApplicationTypeData
    {
        /// <summary>
        /// Retrieves application type information by ApplicationTypeID.
        /// </summary>
        /// <param name="ApplicationTypeID">The ID of the application type to retrieve.</param>
        /// <param name="ApplicationTypeTitle">The title of the application type.</param>
        /// <param name="ApplicationFees">The fees associated with the application type.</param>
        /// <returns>True if the application type information is retrieved successfully, false otherwise.</returns>
        public static bool GetApplicationTypeInfoByID(int ApplicationTypeID, ref string ApplicationTypeTitle, ref float ApplicationFees)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicationTypeTitle = reader["ApplicationTypeTitle"].ToString();
                    ApplicationFees = float.Parse(reader["ApplicationFees"].ToString());
                    result = true;
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
        /// Retrieves all application types from the database.
        /// </summary>
        /// <returns>A DataTable containing all application types.</returns>
        public static DataTable GetAllApplicationTypes()
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM ApplicationTypes";
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
        /// Adds a new application type to the database and returns the ID of the added application type.
        /// </summary>
        /// <param name="Title">The title of the application type.</param>
        /// <param name="Fees">The fees associated with the application type.</param>
        /// <returns>The ID of the added application type, or -1 if the operation fails.</returns>
        public static int AddNewApplicationType(string Title, float Fees)
        {
            int IDOfAddedApplicationType = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO ApplicationTypes (ApplicationTypeTitle, ApplicationFees) VALUES (@ApplicationTypeTitle, @ApplicationFees); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            try
            {
                connection.Open();
                IDOfAddedApplicationType = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                IDOfAddedApplicationType = -1;
            }
            finally
            {
                connection.Close();
            }
            return IDOfAddedApplicationType;
        }
        /// <summary>
        /// Updates an existing application type in the database.
        /// </summary>
        /// <param name="ApplicationTypeID">The ID of the application type to update.</param>
        /// <param name="Title">The new title of the application type.</param>
        /// <param name="Fees">The new fees of the application type.</param>
        /// <returns>True if the application type is updated successfully, false otherwise.</returns>
        public static bool UpdateApplicationType(int ApplicationTypeID, string Title, float Fees)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE ApplicationTypes SET ApplicationTypeTitle = @ApplicationTypeTitle, ApplicationFees = @ApplicationFees WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeTitle", Title);
            command.Parameters.AddWithValue("@ApplicationFees", Fees);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    result = true;
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
        /// Deletes an application type from the database by ApplicationTypeID.
        /// </summary>
        /// <param name="ApplicationTypeID">The ID of the application type to delete.</param>
        /// <returns>True if the application type is deleted successfully, false otherwise.</returns>
        public static bool DeleteApplicationType(int ApplicationTypeID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM ApplicationTypes WHERE ApplicationTypeID = @ApplicationTypeID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            try
            {
                connection.Open();
                if (command.ExecuteNonQuery() > 0)
                {
                    result = true;
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
    }
}
