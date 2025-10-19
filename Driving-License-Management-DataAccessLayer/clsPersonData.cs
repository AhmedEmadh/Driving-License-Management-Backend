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
    /// Provides data access functionality for person-related operations.
    /// </summary>
    public static class clsPersonData
    {
        /// <summary>
        /// Retrieves a person's information by their PersonID.
        /// </summary>
        /// <param name="PersonID">The ID of the person to retrieve information for.</param>
        /// <param name="FirstName">The person's first name.</param>
        /// <param name="SecondName">The person's second name.</param>
        /// <param name="ThirdName">The person's third name.</param>
        /// <param name="LastName">The person's last name.</param>
        /// <param name="NationalNo">The person's national number.</param>
        /// <param name="DateOfBirth">The person's date of birth.</param>
        /// <param name="
        /// 
        /// ">The person's gender.</param>
        /// <param name="Address">The person's address.</param>
        /// <param name="Phone">The person's phone number.</param>
        /// <param name="Email">The person's email address.</param>
        /// <param name="NationalityCountryID">The ID of the person's nationality country.</param>
        /// <param name="ImagePath">The path to the person's image.</param>
        /// <returns>True if the person's information was successfully retrieved; otherwise, false.</returns>
        public static bool GetPersonInfoByID(int PersonID, ref string FirstName, ref string SecondName, ref string ThirdName, ref string LastName, ref string NationalNo, ref DateTime DateOfBirth, ref short Gender, ref string Address, ref string Phone, ref string Email, ref int NationalityCountryID, ref string ImagePath)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"] != DBNull.Value ? reader["ThirdName"].ToString() : "";
                    LastName = reader["LastName"].ToString();
                    NationalNo = reader["NationalNo"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gender = Convert.ToInt16(reader["Gender"]);
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";
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
        /// Retrieves a person's information by their national number.
        /// </summary>
        /// <param name="NationalNo">The national number of the person to retrieve information for.</param>
        /// <param name="PersonID">The ID of the person.</param>
        /// <param name="FirstName">The first name of the person.</param>
        /// <param name="SecondName">The second name of the person.</param>
        /// <param name="ThirdName">The third name of the person.</param>
        /// <param name="LastName">The last name of the person.</param>
        /// <param name="DateOfBirth">The date of birth of the person.</param>
        /// <param name="Gender">The gender of the person.</param>
        /// <param name="Address">The address of the person.</param>
        /// <param name="Phone">The phone number of the person.</param>
        /// <param name="Email">The email address of the person.</param>
        /// <param name="NationalityCountryID">The ID of the person's nationality country.</param>
        /// <param name="ImagePath">The path to the person's image.</param>
        /// <returns>True if the person's information is retrieved successfully, false otherwise.</returns>
        public static bool GetPersonInfoByNationalNo(string NationalNo, ref int PersonID, ref string FirstName, ref string SecondName,
                                                     ref string ThirdName, ref string LastName, ref DateTime DateOfBirth,
                                                     ref short Gender, ref string Address, ref string Phone, ref string Email,
                                                     ref int NationalityCountryID, ref string ImagePath)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    PersonID = Convert.ToInt32(reader["PersonID"]);
                    FirstName = reader["FirstName"].ToString();
                    SecondName = reader["SecondName"].ToString();
                    ThirdName = reader["ThirdName"] != DBNull.Value ? reader["ThirdName"].ToString() : "";
                    LastName = reader["LastName"].ToString();
                    DateOfBirth = Convert.ToDateTime(reader["DateOfBirth"]);
                    Gender = Convert.ToInt16(reader["Gender"]);
                    Address = reader["Address"].ToString();
                    Phone = reader["Phone"].ToString();
                    Email = reader["Email"] != DBNull.Value ? reader["Email"].ToString() : "";
                    NationalityCountryID = Convert.ToInt32(reader["NationalityCountryID"]);
                    ImagePath = reader["ImagePath"] != DBNull.Value ? reader["ImagePath"].ToString() : "";
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
        /// Adds a new person to the database and returns the ID of the added person.
        /// </summary>
        /// <param name="FirstName">The first name of the person.</param>
        /// <param name="SecondName">The second name of the person.</param>
        /// <param name="ThirdName">The third name of the person.</param>
        /// <param name="LastName">The last name of the person.</param>
        /// <param name="NationalNo">The national number of the person.</param>
        /// <param name="DateOfBirth">The date of birth of the person.</param>
        /// <param name="Gender">The gender of the person.</param>
        /// <param name="Address">The address of the person.</param>
        /// <param name="Phone">The phone number of the person.</param>
        /// <param name="Email">The email address of the person.</param>
        /// <param name="NationalityCountryID">The ID of the person's nationality country.</param>
        /// <param name="ImagePath">The path to the person's image.</param>
        /// <returns>The ID of the added person, or -1 if the operation fails.</returns>
        public static int AddNewPerson(string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth, short Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            int PersonID = -1;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                               INSERT INTO People
                               (NationalNo
                               ,FirstName
                               ,SecondName
                               ,ThirdName
                               ,LastName
                               ,DateOfBirth
                               ,Gender
                               ,Address
                               ,Phone
                               ,Email
                               ,NationalityCountryID
                               ,ImagePath)
                         VALUES
                               (@NationalNo, @FirstName, @SecondName, @ThirdName, @LastName, @DateOfBirth, @Gender, @Address, @Phone, @Email, @NationalityCountryID, @ImagePath);
                         SELECT SCOPE_IDENTITY();
             ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != string.Empty && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != string.Empty && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != string.Empty && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
            try
            {
                connection.Open();
                PersonID = Convert.ToInt32(command.ExecuteScalar());
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                PersonID = -1;
            }
            finally
            {
                connection.Close();
            }
            return PersonID;
        }
        /// <summary>
        /// Updates an existing person record in the database.
        /// </summary>
        /// <param name="PersonID">The ID of the person to be updated.</param>
        /// <param name="FirstName">The first name of the person.</param>
        /// <param name="SecondName">The second name of the person.</param>
        /// <param name="ThirdName">The third name of the person.</param>
        /// <param name="LastName">The last name of the person.</param>
        /// <param name="NationalNo">The national number of the person.</param>
        /// <param name="DateOfBirth">The date of birth of the person.</param>
        /// <param name="Gender">The gender of the person.</param>
        /// <param name="Address">The address of the person.</param>
        /// <param name="Phone">The phone number of the person.</param>
        /// <param name="Email">The email address of the person.</param>
        /// <param name="NationalityCountryID">The ID of the nationality country of the person.</param>
        /// <param name="ImagePath">The path to the image of the person.</param>
        /// <returns>True if the person record is updated successfully, false otherwise.</returns>
        public static bool UpdatePerson(int PersonID, string FirstName, string SecondName, string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth, short Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"
                               UPDATE People
                               SET
                               FirstName = @FirstName
                               ,SecondName = @SecondName
                               ,ThirdName = @ThirdName
                               ,LastName = @LastName
                               ,NationalNo = @NationalNo
                               ,DateOfBirth = @DateOfBirth
                               ,Gender = @Gender
                               ,Address = @Address
                               ,Phone = @Phone
                               ,Email = @Email
                               ,NationalityCountryID = @NationalityCountryID
                               ,ImagePath = @ImagePath
                               WHERE PersonID = @PersonID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            command.Parameters.AddWithValue("@FirstName", FirstName);
            command.Parameters.AddWithValue("@SecondName", SecondName);
            if (ThirdName != string.Empty && ThirdName != null)
                command.Parameters.AddWithValue("@ThirdName", ThirdName);
            else
                command.Parameters.AddWithValue("@ThirdName", DBNull.Value);
            command.Parameters.AddWithValue("@LastName", LastName);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            command.Parameters.AddWithValue("@DateOfBirth", DateOfBirth);
            command.Parameters.AddWithValue("@Gender", Gender);
            command.Parameters.AddWithValue("@Address", Address);
            command.Parameters.AddWithValue("@Phone", Phone);
            if (Email != string.Empty && Email != null)
                command.Parameters.AddWithValue("@Email", Email);
            else
                command.Parameters.AddWithValue("@Email", DBNull.Value);
            command.Parameters.AddWithValue("@NationalityCountryID", NationalityCountryID);
            if (ImagePath != string.Empty && ImagePath != null)
                command.Parameters.AddWithValue("@ImagePath", ImagePath);
            else
                command.Parameters.AddWithValue("@ImagePath", DBNull.Value);
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
        /// Retrieves all people from the database.
        /// </summary>
        /// <returns>A DataTable containing all people.</returns>
        public static DataTable GetAllPeople()
        {
            DataTable Result = new DataTable();
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = @"SELECT PersonID, NationalNo, FirstName, SecondName, ThirdName, LastName,DateOfBirth, 
                                CASE
								WHEN People.Gender = '0' THEN 'Male'
								ELSE 'Female'
								END
								AS GenderCaption, Address, Phone, Email, Countries.CountryName, ImagePath FROM People
                                INNER JOIN Countries ON People.NationalityCountryID = Countries.CountryID
            ";
            SqlCommand command = new SqlCommand(query, connection);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                Result.Load(reader);

                Result.Columns[0].DataType = typeof(int);
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                Result = null;
            }
            return Result;
        }
        /// <summary>
        /// Deletes a person from the People table in the database based on the provided PersonID.
        /// </summary>
        /// <param name="PersonID">The ID of the person to be deleted.</param>
        /// <returns>True if the deletion is successful, false otherwise.</returns>
        public static bool DeletePerson(int PersonID)
        {
            bool isSuccess = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "DELETE FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
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
        /// Checks if a person exists in the database by their PersonID.
        /// </summary>
        /// <param name="PersonID">The ID of the person to check.</param>
        /// <returns>True if the person exists, false otherwise.</returns>
        public static bool IsPersonExist(int PersonID)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE PersonID = @PersonID";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@PersonID", PersonID);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;

        }
        /// <summary>
        /// Checks if a person exists in the database by their NationalNo.
        /// </summary>
        /// <param name="NationalNo">The NationalNo of the person to check.</param>
        /// <returns>True if the person exists, false otherwise.</returns>
        public static bool IsPersonExist(string NationalNo)
        {
            bool isExist = false;
            SqlConnection connection = new SqlConnection(clsDataAccessSettings.ConnectionString);
            string query = "SELECT * FROM People WHERE NationalNo = @NationalNo";
            SqlCommand command = new SqlCommand(query, connection);
            command.Parameters.AddWithValue("@NationalNo", NationalNo);
            try
            {
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                if (reader.Read())
                {
                    isExist = true;
                }
                else
                {
                    isExist = false;
                }
            }
            catch (Exception ex)
            {
                clsLogger.Log(ex);
                isExist = false;
            }
            finally
            {
                connection.Close();
            }
            return isExist;

        }
    }
}
