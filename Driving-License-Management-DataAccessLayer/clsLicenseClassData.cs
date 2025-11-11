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
    /// This class is responsible for data access operations related to the LicenseClass table.
    /// </summary>
    public static class clsLicenseClassData
    {
        /// <summary>
        /// Retrieves license class information by LicenseClassID.
        /// </summary>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <param name="ClassName">The name of the license class.</param>
        /// <param name="ClassDescription">The description of the license class.</param>
        /// <param name="MinimumAllowedAge">The minimum allowed age for the license class.</param>
        /// <param name="DefaultValidityLength">The default validity length of the license class.</param>
        /// <param name="ClassFees">The fees associated with the license class.</param>
        /// <returns>True if the license class information is retrieved successfully, false otherwise.</returns>
        public static bool GetLicenseClassInfoByID(int LicenseClassID, ref string ClassName, ref string ClassDescription, ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ClassName = reader["ClassName"].ToString();
                    ClassDescription = reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                    isFound = true;
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        /// <summary>
        /// Retrieves license class information by ClassName.
        /// </summary>
        /// <param name="ClassName">The name of the license class.</param>
        /// <param name="LicenseClassID">The ID of the license class.</param>
        /// <param name="ClassDescription">The description of the license class.</param>
        /// <param name="MinimumAllowedAge">The minimum allowed age for the license class.</param>
        /// <param name="DefaultValidityLength">The default validity length of the license class.</param>
        /// <param name="ClassFees">The fees associated with the license class.</param>
        /// <returns>True if the license class information is retrieved successfully, false otherwise.</returns>
        public static bool GetLicenseClassInfoByClassName(string ClassName, ref int LicenseClassID, ref string ClassDescription, ref byte MinimumAllowedAge, ref byte DefaultValidityLength, ref float ClassFees)
        {
            bool isFound = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses WHERE ClassName = @ClassName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    LicenseClassID = Convert.ToInt32(reader["LicenseClassID"]);
                    ClassDescription = reader["ClassDescription"].ToString();
                    MinimumAllowedAge = Convert.ToByte(reader["MinimumAllowedAge"]);
                    DefaultValidityLength = Convert.ToByte(reader["DefaultValidityLength"]);
                    ClassFees = Convert.ToSingle(reader["ClassFees"]);
                    isFound = true;
                    reader.Close();
                }
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isFound;
        }
        /// <summary>
        /// Retrieves all license class information from the database.
        /// </summary>
        /// <returns>A DataTable containing the license class information.</returns>
        /// <exception cref="Exception"></exception>
        public static DataTable GetAllLicenseClasses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM LicenseClasses";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataAdapter adapter = new SqlDataAdapter(command);
                adapter.Fill(dataTable);

            }
            catch (SqlException ex)
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
        /// Adds a new license class to the database.
        /// </summary>
        /// <param name="ClassName">The name of the license class.</param>
        /// <param name="ClassDescription">The description of the license class.</param>
        /// <param name="MinimumAllowedAge">The minimum allowed age for the license class.</param>
        /// <param name="DefaultValidityLength">The default validity length of the license class.</param>
        /// <param name="ClassFees">The fees associated with the license class.</param>
        /// <returns>The ID of the newly added license class if added successfully, returns -1 otherwise</returns>
        public static int AddNewLicenseClass(string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            int CreatedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO LicenseClasses (ClassName, ClassDescription, MinimumAllowedAge, DefaultValidityLength, ClassFees) VALUES (@ClassName, @ClassDescription, @MinimumAllowedAge, @DefaultValidityLength, @ClassFees); SELECT SCOPE_IDENTITY();";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            try
            {
                connection.Open();
                CreatedID = Convert.ToInt32(command.ExecuteScalar());

            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return CreatedID;
        }
        /// <summary>
        /// Updates an existing license class in the database.
        /// </summary>
        /// <param name="LicenseClassID">The ID of the license class to update.</param>
        /// <param name="ClassName">The name of the license class.</param>
        /// <param name="ClassDescription">The description of the license class.</param>
        /// <param name="MinimumAllowedAge">The minimum allowed age for the license class.</param>
        /// <param name="DefaultValidityLength">The default validity length of the license class.</param>
        /// <param name="ClassFees">The fees associated with the license class.</param>
        /// <returns>True if the update was successful, false otherwise.</returns>
        public static bool UpdateLicenseClass(int LicenseClassID, string ClassName, string ClassDescription, byte MinimumAllowedAge, byte DefaultValidityLength, float ClassFees)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE LicenseClasses SET ClassName = @ClassName, ClassDescription = @ClassDescription, MinimumAllowedAge = @MinimumAllowedAge, DefaultValidityLength = @DefaultValidityLength, ClassFees = @ClassFees WHERE LicenseClassID = @LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            command.Parameters.AddWithValue("@ClassName", ClassName);
            command.Parameters.AddWithValue("@ClassDescription", ClassDescription);
            command.Parameters.AddWithValue("@MinimumAllowedAge", MinimumAllowedAge);
            command.Parameters.AddWithValue("@DefaultValidityLength", DefaultValidityLength);
            command.Parameters.AddWithValue("@ClassFees", ClassFees);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isSuccess = rowsAffected > 0;
            }
            catch (SqlException ex)
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
        ///  
        /// </summary>
        /// <param name="LicenseClassID">The ID of the license class to update.</param>
        /// <returns>True if the delete was successful, false otherwise.</returns>
        public static bool DeleteLicenseClass(int LicenseClassID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM LicenseClasses WHERE LicenseClassID = @LicenseClassID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isSuccess = rowsAffected > 0;
            }
            catch (SqlException ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
    }
}
