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
    /// Class containing methods for user data access.
    /// </summary>
    public class clsUserData
    {
        /// <summary>
        /// Retrieves user information by UserID.
        /// </summary>
        /// <param name="UserID">The ID of the user to retrieve information for.</param>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <param name="UserName">The username of the user.</param>
        /// <param name="Password">The password of the user.</param>
        /// <param name="IsActive">A flag indicating whether the user is active.</param>
        /// <returns>True if the user information is retrieved successfully; otherwise, false.</returns>
        public static bool GetUserInfoByUserID(int UserID, ref int PersonID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT PersonID, UserName, Password, IsActive FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    isSuccess = true;
                    reader.Close();
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
        /// Retrieves user information by PersonID.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <param name="UserID">The ID of the user.</param>
        /// <param name="UserName">The username of the user.</param>
        /// <param name="Password">The password of the user.</param>
        /// <param name="IsActive">A flag indicating whether the user is active.</param>
        /// <returns>True if the user information is retrieved successfully, false otherwise.</returns>
        public static bool GetUserInfoByPersonID(int PersonID, ref int UserID, ref string UserName, ref string Password, ref bool IsActive)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT UserID, UserName, Password, IsActive FROM Users WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["UserID"]);
                    UserName = reader["UserName"].ToString();
                    Password = reader["Password"].ToString();
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    isSuccess = true;
                    reader.Close();
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
        /// Retrieves user information by username and password.
        /// </summary>
        /// <param name="UserName">The username to search for.</param>
        /// <param name="Password">The password to search for.</param>
        /// <param name="UserID">The ID of the user.</param>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <param name="IsActive">A flag indicating whether the user is active.</param>
        /// <returns>True if the user information is retrieved successfully, false otherwise.</returns>
        public static bool GetUserInfoByUsernameAndPassword(string UserName, string Password, ref int UserID, ref int PersonID, ref bool IsActive)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT UserID, PersonID, IsActive FROM Users WHERE UserName = @UserName AND Password = @Password";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    UserID = Convert.ToInt32(reader["UserID"]);
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    IsActive = Convert.ToBoolean(reader["IsActive"]);
                    isSuccess = true;
                    reader.Close();
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
        /// Adds a new user to the database.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <param name="UserName">The username of the new user.</param>
        /// <param name="Password">The password of the new user.</param>
        /// <param name="IsActive">A flag indicating whether the new user is active.</param>
        /// <returns>The ID of the newly created user.</returns>
        public static int AddNewUser(int PersonID, string UserName, string Password, bool IsActive)
        {
            int CreatedUserID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "INSERT INTO Users (PersonID, UserName, Password, IsActive) VALUES (@PersonID, @UserName, @Password, @IsActive); SELECT @@IDENTITY";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
            try
            {
                connection.Open();
                CreatedUserID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return CreatedUserID;
        }
        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <param name="UserID">The ID of the user to be updated.</param>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <param name="UserName">The username of the user.</param>
        /// <param name="Password">The password of the user.</param>
        /// <param name="IsActive">A flag indicating whether the user is active.</param>
        /// <returns>True if the user information is updated successfully, false otherwise.</returns>
        public static bool UpdateUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Users SET PersonID = @PersonID, UserName = @UserName, Password = @Password, IsActive = @IsActive WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@UserName", UserName);
            command.Parameters.AddWithValue("@Password", Password);
            command.Parameters.AddWithValue("@IsActive", IsActive);
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
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A DataTable containing all users.</returns>
        public static DataTable GetAllUsers()
        {
            DataTable dataTable = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                                SELECT  Users.UserID, Users.PersonID,
                                FullName = People.FirstName + ' ' + People.SecondName + ' ' + ISNULL( People.ThirdName,'') +' ' + People.LastName,
                                Users.UserName, Users.IsActive
                                FROM  Users INNER JOIN
                                People ON Users.PersonID = People.PersonID
                            ";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                dataTable.Load(reader);
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
            return dataTable;
        }
        /// <summary>
        /// Deletes a user from the database based on the provided UserID.
        /// </summary>
        /// <param name="UserID">The ID of the user to be deleted.</param>
        /// <returns>True if the user is deleted successfully, false otherwise.</returns>
        public static bool DeleteUser(int UserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                int numberOfRowsAffacted = command.ExecuteNonQuery();
                isSuccess = numberOfRowsAffacted > 0;

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
        /// Checks if a user exists in the database based on the provided UserID.
        /// </summary>
        /// <param name="UserID">The ID of the user to check for existence.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsUserExist(int UserID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(*) FROM Users WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isSuccess = count > 0;

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
        /// Checks if a user exists in the database by their username.
        /// </summary>
        /// <param name="UserName">The username to search for.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsUserExist(string UserName)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(*) FROM Users WHERE UserName = @UserName";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserName", UserName);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isExist = Convert.ToInt32(reader[0]) > 0;
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
            return isExist;
        }
        /// <summary>
        /// Checks if a user exists in the database for the given PersonID.
        /// </summary>
        /// <param name="PersonID">The ID of the person to check for user existence.</param>
        /// <returns>True if a user exists for the given PersonID, false otherwise.</returns>
        public static bool IsUserExistForPersonID(int PersonID)
        {
            bool isUserExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT COUNT(*) FROM Users WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                int count = (int)command.ExecuteScalar();
                isUserExist = count > 0;
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
            }
            finally
            {
                connection.Close();
            }
            return isUserExist;
        }
        /// <summary>
        /// Checks if a person has a user in the database.
        /// </summary>
        /// <param name="PersonID">The ID of the person.</param>
        /// <returns>True if the person has a user, false otherwise.</returns>
        public static bool DoesPersonHaveUser(int PersonID)
        {
            bool isFound = false;

            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);

            string query = "SELECT Found=1 FROM Users WHERE PersonID = @PersonID";

            SqlCommand command = new SqlCommand(query, connection);

            command.Parameters.AddWithValue("@PersonID", PersonID);

            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

                isFound = reader.HasRows;

                reader.Close();
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                isFound = false;
            }
            finally
            {
                connection.Close();
            }

            return isFound;
        }
        /// <summary>
        /// Changes the password of a user in the database.
        /// </summary>
        /// <param name="UserID">The ID of the user to change the password for.</param>
        /// <param name="NewPassword">The new password to set for the user.</param>
        /// <returns>True if the password is changed successfully, false otherwise.</returns>
        public static bool ChangePassword(int UserID, string NewPassword)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "UPDATE Users SET Password = @Password WHERE UserID = @UserID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@UserID", UserID);
            command.Parameters.AddWithValue("@Password", NewPassword);
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
    }
}
