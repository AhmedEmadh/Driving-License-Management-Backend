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
    /// Static class for accessing test type data.
    /// </summary>
    public static class clsTestTypeData
    {
        /// <summary>
        /// Retrieves test type information by TestTypeID.
        /// </summary>
        /// <param name="TestTypeID">The ID of the test type.</param>
        /// <param name="TestTypeTitle">The title of the test type, passed by reference.</param>
        /// <param name="TestTypeDescription">The description of the test type, passed by reference.</param>
        /// <param name="TestTypeFees">The fees associated with the test type, passed by reference.</param>
        /// <returns>True if the test type information is retrieved successfully, false otherwise.</returns>
        public static bool GetTestTypeInfoByID(int TestTypeID, ref string TestTypeTitle, ref string TestTypeDescription, ref float TestTypeFees)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestTypes WHERE TestTypeID = @TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    TestTypeTitle = reader["TestTypeTitle"].ToString();
                    TestTypeDescription = reader["TestTypeDescription"].ToString();
                    TestTypeFees = float.Parse(reader["TestTypeFees"].ToString());
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
        /// Retrieves all test types from the database.
        /// </summary>
        /// <returns>A DataTable containing all test types.</returns>
        public static DataTable GetAllTestTypes()
        {
            DataTable testTypes = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM TestTypes;";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                testTypes.Load(reader);
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
            return testTypes;
        }
        /// <summary>
        /// Adds a new test type to the database and returns the ID of the newly added test type.
        /// </summary>
        /// <param name="Title">The title of the test type.</param>
        /// <param name="Description">The description of the test type.</param>
        /// <param name="Fees">The fees associated with the test type.</param>
        /// <returns>The ID of the newly added test type, or -1 if the operation fails.</returns>
        public static int AddNewTestType(string Title, string Description, float Fees)
        {
            int CreatedID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO TestTypes (TestTypeTitle, TestTypeDescription, TestTypeFees) OUTPUT INSERTED.TestTypeID VALUES (@Title, @Description, @Fees);";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);
            try
            {
                connection.Open();
                CreatedID = (int)command.ExecuteScalar();

            }
            catch (Exception ex)
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
        /// Updates an existing test type in the database.
        /// </summary>
        /// <param name="TestTypeID">The ID of the test type to update.</param>
        /// <param name="Title">The new title of the test type.</param>
        /// <param name="Description">The new description of the test type.</param>
        /// <param name="Fees">The new fees of the test type.</param>
        /// <returns>True if the test type is updated successfully, false otherwise.</returns>
        public static bool UpdateTestType(int TestTypeID, string Title, string Description, float Fees)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE TestTypes SET TestTypeTitle=@Title, TestTypeDescription=@Description, TestTypeFees=@Fees WHERE TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            command.Parameters.AddWithValue("@Title", Title);
            command.Parameters.AddWithValue("@Description", Description);
            command.Parameters.AddWithValue("@Fees", Fees);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isSuccess = true;
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
            return isSuccess;
        }
        /// <summary>
        /// Deletes a test type from the database.
        /// </summary>

        public static bool DeleteTestType(int TestTypeID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM TestTypes WHERE TestTypeID=@TestTypeID;";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@TestTypeID", TestTypeID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                if (rowsAffected > 0)
                {
                    isSuccess = true;
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
            return isSuccess;
        }
    }
}
