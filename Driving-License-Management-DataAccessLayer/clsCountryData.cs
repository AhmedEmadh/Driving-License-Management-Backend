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
    /// Provides data access methods for country-related operations.
    /// </summary>
    public static class clsCountryData
    {
        /// <summary>
        /// Retrieves country information by ID.
        /// </summary>
        /// <param name="ID">The unique identifier of the country.</param>
        /// <param name="CountryName">The name of the country.</param>
        /// <returns>True if the country information is retrieved successfully, false otherwise.</returns>
        public static bool GetCountryInfoByID(int ID, ref string CountryName)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryID = @CountryID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryID", ID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    CountryName = reader["CountryName"].ToString();
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves country information by name.
        /// </summary>
        /// <param name="CountryName">The name of the country.</param>
        /// <param name="ID">The unique identifier of the country.</param>
        /// <returns>True if the country information is retrieved successfully, false otherwise.</returns>
        public static bool GetCountryInfoByName(string CountryName, ref int ID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries WHERE CountryName = @CountryName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@CountryName", CountryName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ID = Convert.ToInt32(reader["CountryID"]);
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                isSuccess = false;
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves all countries from the database.
        /// </summary>
        /// <returns>A DataTable containing all countries.</returns>
        public static DataTable GetAllCountries()
        {
            DataTable table = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Countries";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                table.Load(reader);
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return table;
        }
    }
}
