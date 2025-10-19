using Microsoft.Data.SqlClient;
using System.Data;

namespace Driving_License_Management_DataAccessLayer
{
    /// <summary>
    /// Provides data access methods for application data.
    /// </summary>
    public static class clsApplicationData
    {
        /// <summary>
        /// Enumerates the possible application statuses.
        /// </summary>
        enum ApplicationStatus
        {
            New = 1,
            Cancelled = 2,
            Completed = 3
        }
        /// <summary>
        /// Retrieves application information by application ID.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application.</param>
        /// <param name="ApplicantPersonID">The ID of the applicant person.</param>
        /// <param name="ApplicationDate">The date of the application.</param>
        /// <param name="ApplicationTypeID">The type ID of the application.</param>
        /// <param name="ApplicationStatus">The status of the application.</param>
        /// <param name="LastStatusDate">The date of the last status update.</param>
        /// <param name="PaidFees">The paid fees for the application.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the application.</param>
        /// <returns>True if the application information is retrieved successfully, false otherwise.</returns>
        public static bool GetApplicationInfoByID(int ApplicationID, ref int ApplicantPersonID, ref DateTime ApplicationDate, ref int ApplicationTypeID, ref byte ApplicationStatus, ref DateTime LastStatusDate, ref float PaidFees, ref int CreatedByUserID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    ApplicantPersonID = int.Parse(reader["ApplicantPersonID"].ToString());
                    ApplicationDate = DateTime.Parse(reader["ApplicationDate"].ToString());
                    ApplicationTypeID = int.Parse(reader["ApplicationTypeID"].ToString());
                    ApplicationStatus = byte.Parse(reader["ApplicationStatus"].ToString());
                    LastStatusDate = DateTime.Parse(reader["LastStatusDate"].ToString());
                    PaidFees = float.Parse(reader["PaidFees"].ToString());
                    CreatedByUserID = int.Parse(reader["CreatedByUserID"].ToString());
                    result = true;
                }
                else
                {
                    result = false;
                }
            }
            catch (Exception ex)
            {
                result = false;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Retrieves all applications.
        /// </summary>
        /// <returns>A DataTable containing all applications.</returns>
        public static DataTable GetAllApplications()
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications";
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
        /// Adds a new application to the database and returns the ID of the added application.
        /// </summary>
        /// <param name="ApplicantPersonID">The ID of the applicant person.</param>
        /// <param name="ApplicationDate">The date of the application.</param>
        /// <param name="ApplicationTypeID">The type ID of the application.</param>
        /// <param name="ApplicationStatus">The status of the application.</param>
        /// <param name="LastStatusDate">The date of the last status update.</param>
        /// <param name="PaidFees">The paid fees for the application.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the application.</param>
        /// <returns>The ID of the added application, or -1 if the operation fails.</returns>
        public static int AddNewApplication(int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            int IDOfAddedApplication = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Applications (ApplicantPersonID, ApplicationDate, ApplicationTypeID, ApplicationStatus, LastStatusDate, PaidFees, CreatedByUserID) VALUES (@ApplicantPersonID, @ApplicationDate, @ApplicationTypeID, @ApplicationStatus, @LastStatusDate, @PaidFees, @CreatedByUserID); SELECT SCOPE_IDENTITY()";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            try
            {
                connection.Open();
                IDOfAddedApplication = int.Parse(command.ExecuteScalar().ToString());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                IDOfAddedApplication = -1;
            }
            finally
            {
                connection.Close();
            }
            return IDOfAddedApplication;
        }
        /// <summary>
        /// Updates an existing application in the database.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to update.</param>
        /// <param name="ApplicantPersonID">The ID of the applicant person.</param>
        /// <param name="ApplicationDate">The date of the application.</param>
        /// <param name="ApplicationTypeID">The type ID of the application.</param>
        /// <param name="ApplicationStatus">The status of the application.</param>
        /// <param name="LastStatusDate">The date of the last status update.</param>
        /// <param name="PaidFees">The paid fees for the application.</param>
        /// <param name="CreatedByUserID">The ID of the user who created the application.</param>
        /// <returns>True if the application is updated successfully, false otherwise.</returns>
        public static bool UpdateApplication(int ApplicationID, int ApplicantPersonID, DateTime ApplicationDate, int ApplicationTypeID, byte ApplicationStatus, DateTime LastStatusDate, float PaidFees, int CreatedByUserID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Applications SET ApplicantPersonID = @ApplicantPersonID, ApplicationDate = @ApplicationDate, ApplicationTypeID = @ApplicationTypeID, ApplicationStatus = @ApplicationStatus, LastStatusDate = @LastStatusDate, PaidFees = @PaidFees, CreatedByUserID = @CreatedByUserID WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", ApplicantPersonID);
            command.Parameters.AddWithValue("@ApplicationDate", ApplicationDate);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", ApplicationStatus);
            command.Parameters.AddWithValue("@LastStatusDate", LastStatusDate);
            command.Parameters.AddWithValue("@PaidFees", PaidFees);
            command.Parameters.AddWithValue("@CreatedByUserID", CreatedByUserID);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        /// Deletes an application from the database by ApplicationID.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to delete.</param>
        /// <returns>True if the application is deleted successfully, false otherwise.</returns>
        public static bool DeleteApplication(int ApplicationID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
        /// Checks if an application exists in the database by ApplicationID.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to check.</param>
        /// <returns>True if the application exists, false otherwise.</returns>
        public static bool IsApplicationExist(int ApplicationID)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM Applications WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
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
        /// Checks if a person has an active application based on the provided PersonID and ApplicationTypeID.
        /// </summary>
        /// <param name="PersonID">The ID of the person to check.</param>
        /// <param name="ApplicationTypeID">The type ID of the application to check.</param>
        /// <returns>True if the person has an active application, false otherwise.</returns>
        public static bool DoesPersonHaveActiveApplication(int PersonID, int ApplicationTypeID)
        {
            return GetActiveApplicationID(PersonID, ApplicationTypeID) != -1;
        }
        /// <summary>
        /// Retrieves the active application ID for a given person and application type.
        /// </summary>
        /// <param name="PersonID">The ID of the person to retrieve the active application for.</param>
        /// <param name="ApplicationTypeID">The type ID of the application to retrieve.</param>
        /// <returns>The active application ID, or -1 if no active application is found.</returns>
        public static int GetActiveApplicationID(int PersonID, int ApplicationTypeID)
        {
            int result = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT ApplicationID FROM Applications WHERE ApplicantPersonID = @ApplicantPersonID AND ApplicationTypeID = @ApplicationTypeID AND ApplicationStatus = @ApplicationStatus";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@ApplicationStatus", (byte)ApplicationStatus.New);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = int.Parse(reader["ApplicationID"].ToString());
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                result = -1;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Retrieves the active application ID for a given person, application type, and license class.
        /// </summary>
        /// <param name="PersonID">The ID of the person to retrieve the active application for.</param>
        /// <param name="ApplicationTypeID">The type ID of the application to retrieve.</param>
        /// <param name="LicenseClassID">The ID of the license class to retrieve.</param>
        /// <returns>The active application ID, or -1 if no active application is found.</returns>
        public static int GetActiveApplicationIDForLicenseClass(int PersonID, int ApplicationTypeID, int LicenseClassID)
        {
            int result = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"   SELECT ActiveApplicationID= Applications.ApplicationID FROM Applications
                                INNER JOIN LocalDrivingLicenseApplications
                                ON Applications.ApplicationID = LocalDrivingLicenseApplications.ApplicationID
                                WHERE 
                                Applications.ApplicantPersonID = @ApplicantPersonID AND
                                Applications.ApplicationTypeID = @ApplicationTypeID AND
                                LocalDrivingLicenseApplications.LicenseClassID = @LicenseClassID AND
                                Applications.ApplicationStatus = 1";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicantPersonID", PersonID);
            command.Parameters.AddWithValue("@ApplicationTypeID", ApplicationTypeID);
            command.Parameters.AddWithValue("@LicenseClassID", LicenseClassID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    result = int.Parse(reader["ActiveApplicationID"].ToString());
                }
                else
                {
                    result = -1;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                result = -1;
            }
            finally
            {
                connection.Close();
            }
            return result;
        }
        /// <summary>
        /// Updates the status of an application in the database.
        /// </summary>
        /// <param name="ApplicationID">The ID of the application to update.</param>
        /// <param name="NewStatus">The new status of the application.</param>
        /// <returns>True if the update is successful, false otherwise.</returns>
        public static bool UpdateStatus(int ApplicationID, short NewStatus)
        {
            bool result = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Applications SET ApplicationStatus = @ApplicationStatus, LastStatusDate = @LastStatusDate WHERE ApplicationID = @ApplicationID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@ApplicationStatus", NewStatus);
            command.Parameters.AddWithValue("@LastStatusDate", DateTime.Now);
            command.Parameters.AddWithValue("@ApplicationID", ApplicationID);
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
