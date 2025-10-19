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
    /// Provides data access methods for detained licenses.
    /// </summary>
    public class clsDetainedLicenseData
    {
        /// <summary>
        /// Retrieves the detained license information by Detain ID.
        /// </summary>
        /// <param name="DetainID">The ID of the detained license to retrieve information for.</param>
        /// <param name="LicenseID">The ID of the license associated with the detained license.</param>
        /// <param name="DetainDate">The date the license was detained.</param>
        /// <param name="FineFees">The fine fees associated with the detained license.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the detained license record.</param>
        /// <param name="IsReleased">A flag indicating whether the detained license has been released.</param>
        /// <param name="ReleaseDate">The date the detained license was released.</param>
        /// <param name="ReleasedByUserID">The ID of the user who released the detained license.</param>
        /// <param name="ReleaseApplicationID">The ID of the application used to release the detained license.</param>
        /// <returns>True if the detained license information was successfully retrieved; otherwise, false.</returns>
        public static bool GetDetainedLicenseInfoByID(int DetainID, ref int LicenseID, ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM DetainedLicenses WHERE DetainID = @DetainID";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@DetainID", DetainID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    LicenseID = (int)reader["LicenseID"];
                    DetainDate = (DateTime)reader["DetainDate"];
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = (int)reader["CreatedByUserID"];
                    IsReleased = (bool)reader["IsReleased"];

                    ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? DateTime.MaxValue : (DateTime)reader["ReleaseDate"];
                    ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? -1 : (int)reader["ReleasedByUserID"];
                    ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? -1 : (int)reader["ReleaseApplicationID"];
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves the detained license information by License ID.
        /// </summary>
        /// <param name="LicenseID">The ID of the license to retrieve information for.</param>
        /// <param name="DetainID">The ID of the detained license.</param>
        /// <param name="DetainDate">The date the license was detained.</param>
        /// <param name="FineFees">The fine fees associated with the detained license.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the detained license record.</param>
        /// <param name="IsReleased">A flag indicating whether the detained license has been released.</param>
        /// <param name="ReleaseDate">The date the detained license was released.</param>
        /// <param name="ReleasedByUserID">The ID of the user who released the detained license.</param>
        /// <param name="ReleaseApplicationID">The ID of the application used to release the detained license.</param>
        /// <returns>True if the detained license information was successfully retrieved; otherwise, false.</returns>
        public static bool GetDetainedLicenseInfoByLicenseID(int LicenseID, ref int DetainID, ref DateTime DetainDate, ref float FineFees, ref int CreatedByUserID, ref bool IsReleased, ref DateTime ReleaseDate, ref int ReleasedByUserID, ref int ReleaseApplicationID)
        {
            bool isSuccess = false;
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT top 1 * FROM DetainedLicenses WHERE LicenseID = @LicenseID order by DetainID desc";
            SqlCommand cmd = new SqlCommand(query, conn);
            cmd.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.Read())
                {
                    DetainID = Convert.ToInt32(reader["DetainID"]);
                    DetainDate = Convert.ToDateTime(reader["DetainDate"]);
                    FineFees = Convert.ToSingle(reader["FineFees"]);
                    CreatedByUserID = Convert.ToInt32(reader["CreatedByUserID"]);
                    IsReleased = Convert.ToBoolean(reader["IsReleased"]);
                    /* Can be DBNull */
                    ReleaseDate = reader["ReleaseDate"] == DBNull.Value ? DateTime.MaxValue : (DateTime)reader["ReleaseDate"];
                    ReleasedByUserID = reader["ReleasedByUserID"] == DBNull.Value ? -1 : (int)reader["ReleasedByUserID"];
                    ReleaseApplicationID = reader["ReleaseApplicationID"] == DBNull.Value ? -1 : (int)reader["ReleaseApplicationID"];
                    isSuccess = true;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                conn.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Retrieves all detained licenses from the database.
        /// </summary>
        /// <returns>A DataTable containing the detained licenses.</returns>
        public static DataTable GetAllDetainedLicenses()
        {
            DataTable dataTable = new DataTable();
            SqlConnection conn = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM DetainedLicenses_View";
            SqlCommand cmd = new SqlCommand(query, conn);
            try
            {
                conn.Open();
                SqlDataReader reader = cmd.ExecuteReader();
                dataTable.Load(reader);
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
        /// Adds a new detained license to the database and returns the ID of the created license.
        /// </summary>
        /// <param name="LicenseID">The ID of the license to be detained.</param>
        /// <param name="DetainDate">The date the license was detained.</param>
        /// <param name="FineFees">The fine fees associated with the detained license.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the detained license record.</param>
        /// <returns>The ID of the created detained license.</returns>
        public static int AddNewDetainedLicense(int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            int IDOfCreatedLicense = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                INSERT INTO DetainedLicenses
                                       (LicenseID
                                       ,DetainDate
                                       ,FineFees
                                       ,CreatedByUserID
                                       ,IsReleased
                                       ,ReleaseDate
                                       ,ReleasedByUserID
                                       ,ReleaseApplicationID)
                                 VALUES
                                       (@LicenseID
                                       ,@DetainDate
                                       ,@FineFees
                                       ,@CreatedByUserID
                                       ,@IsReleased
                                       ,@ReleaseDate
                                       ,@ReleasedByUserID
                                       ,@ReleaseApplicationID);
                                SELECT SCOPE_IDENTITY() AS DetainID;
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@IsReleased", 0);
            command.Parameters.AddWithValue("@ReleaseDate", DBNull.Value);
            command.Parameters.AddWithValue("@ReleasedByUserID", DBNull.Value);
            command.Parameters.AddWithValue("@ReleaseApplicationID", DBNull.Value);
            try
            {
                connection.Open();
                IDOfCreatedLicense = int.Parse(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return IDOfCreatedLicense;
        }
        /// <summary>
        /// Updates an existing detained license in the database.
        /// </summary>
        /// <param name="DetainID">The ID of the detained license to update.</param>
        /// <param name="LicenseID">The ID of the license to be.detained.</param>
        /// <param name="DetainDate">The date the license was.detained.</param>
        /// <param name="FineFees">The fine fees associated with the.detained license.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the.detained license record.</param>
        /// <returns>True if the.detained license is updated successfully; otherwise, false.</returns>
        public static bool UpdateDetainedLicense(int DetainID, int LicenseID, DateTime DetainDate, float FineFees, int CreatedByUserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                UPDATE DetainedLicenses
                                   SET LicenseID = @LicenseID
                                      ,DetainDate = @DetainDate
                                      ,FineFees = @FineFees
                                      ,CreatedByUserID = @CreatedByUserID
                                 WHERE DetainID = @DetainID;
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            command.Parameters.AddWithValue("@DetainDate", DetainDate);
            command.Parameters.AddWithValue("@FineFees", FineFees);
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
            }
            finally
            {
                connection.Close();
            }
            return isSuccess;
        }
        /// <summary>
        /// Releases a detained license from the database.
        /// </summary>
        /// <param name="DetainID">The ID of the detained license to release.</param>
        /// <param name="ReleasedByUserID">The ID of the user who released the detained license.</param>
        /// <param name="ReleaseApplicationID">The ID of the release application associated with the release.</param>
        /// <returns>True if the detained license is released successfully; otherwise, false.</returns>
        public static bool ReleaseDetainedLicense(int DetainID, int ReleasedByUserID, int ReleaseApplicationID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                UPDATE DetainedLicenses
                                   SET IsReleased = 1
                                      ,ReleaseDate = GETDATE()
                                      ,ReleasedByUserID = @ReleasedByUserID
                                      ,ReleaseApplicationID = @ReleaseApplicationID
                                 WHERE DetainID = @DetainID;
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@DetainID", DetainID);
            command.Parameters.AddWithValue("@ReleasedByUserID", ReleasedByUserID);
            command.Parameters.AddWithValue("@ReleaseApplicationID", ReleaseApplicationID);
            try
            {
                connection.Open();
                int rowsAffected = command.ExecuteNonQuery();
                isSuccess = rowsAffected > 0;
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
        /// Checks if a license is detained in the database.
        /// </summary>
        /// <param name="LicenseID">The ID of the license to check.</param>
        /// <returns>True if the license is detained; otherwise, false.</returns>
        public static bool IsLicenseDetained(int LicenseID)
        {
            bool isDetained = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM DetainedLicenses WHERE LicenseID = @LicenseID AND IsReleased = 0";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@LicenseID", LicenseID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isDetained = true;
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
            return isDetained;
        }

    }
}
