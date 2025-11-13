using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsUser
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;

        public int UserID { get; set; }
        public int PersonID { get; set; }
        public clsPerson? PersonInfo { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public bool IsActive { get; set; }
        /// <summary>
        /// Initializes a new instance of the clsUser class, setting default values for its properties.
        /// </summary>
        public clsUser()
        {
            this.UserID = -1;
            this.UserName = string.Empty;
            this.Password = string.Empty;
            this.IsActive = true;
            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsUser class with the specified user ID, person ID, user name, password, and active status.
        /// </summary>
        /// <param name="UserID">The unique identifier for the user.</param>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <param name="UserName">The user name of the user.</param>
        /// <param name="Password">The password of the user.</param>
        /// <param name="IsActive">A flag indicating whether the user is active.</param>
        private clsUser(int UserID, int PersonID, string UserName, string Password, bool IsActive)
        {
            this.UserID = UserID;
            this.PersonID = PersonID;
            this.PersonInfo = clsPerson.Find(PersonID);
            this.UserName = UserName;
            this.Password = Password;
            this.IsActive = IsActive;

            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new user to the system.
        /// </summary>
        /// <returns>True if the user was added successfully, false otherwise.</returns>
        private bool _AddNewUser()
        {
            //call DataAccess Layer

            this.UserID = clsUserData.AddNewUser(this.PersonID, this.UserName, this.Password, this.IsActive);
            return (this.UserID != -1);
        }
        /// <summary>
        /// Updates an existing user in the database.
        /// </summary>
        /// <returns>True if the user was updated successfully, false otherwise.</returns>
        private bool _UpdateUser()
        {
            //call DataAccess Layer
            return clsUserData.UpdateUser(this.UserID, this.PersonID, this.UserName, this.Password, this.IsActive);
        }
        /// <summary>
        /// Finds a user by their unique user ID.
        /// </summary>
        /// <param name="UserID">The ID of the user to find.</param>
        /// <returns>A clsUser object if found, otherwise null.</returns>
        public static clsUser FindByUserID(int UserID)
        {
            int PersonID = -1;
            string UserName = "", Password = "";
            bool IsActive = false;

            bool IsFound = clsUserData.GetUserInfoByUserID(UserID, ref PersonID, ref UserName, ref Password, ref IsActive);
            if (IsFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Finds a user by their associated person ID.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <returns>A clsUser object if found, otherwise null.</returns>
        public static clsUser FindByPersonID(int PersonID)
        {
            string UserName = "", Password = "";
            bool IsActive = false;
            int UserID = -1;
            bool isFound = clsUserData.GetUserInfoByPersonID(PersonID, ref UserID, ref UserName, ref Password, ref IsActive);
            if (isFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Finds a user by their username and password.
        /// </summary>
        /// <param name="UserName">The username of the user to find.</param>
        /// <param name="Password">The password of the user to find.</param>
        /// <returns>A clsUser object if found, otherwise null.</returns>
        public static clsUser FindByUserNameAndPassword(string UserName, string Password)
        {
            int UserID = -1;
            int PersonID = -1;
            bool IsActive = false;
            bool IsFound = clsUserData.GetUserInfoByUsernameAndPassword(UserName, Password, ref UserID, ref PersonID, ref IsActive);
            if (IsFound)
            {
                return new clsUser(UserID, PersonID, UserName, Password, IsActive);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Saves the user information to the database.
        /// </summary>
        /// <returns>True if the user information was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewUser())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case enMode.Update:
                    return _UpdateUser();
            }
            return false;
        }
        /// <summary>
        /// Retrieves all users from the database.
        /// </summary>
        /// <returns>A DataTable containing all users.</returns>
        public static DataTable GetAllUsers()
        {
            return clsUserData.GetAllUsers();
        }
        public static List<clsUser> GetAllUsersList()
        {
            DataTable dtUsers = clsUserData.GetAllUsers();
            List<clsUser> usersList = new List<clsUser>();
            foreach (DataRow row in dtUsers.Rows)
            {
                int userID = Convert.ToInt32(row["UserID"]);
                clsUser user = FindByUserID(userID);
                usersList.Add(user);
            }
            return usersList;
        }
        /// <summary>
        /// Deletes a user from the database.
        /// </summary>
        /// <param name="UserID">The ID of the user to delete.</param>
        /// <returns>True if the user was deleted successfully, false otherwise.</returns>
        public static bool DeleteUser(int UserID)
        {
            return clsUserData.DeleteUser(UserID);
        }
        /// <summary>
        /// Checks if a user exists in the database.
        /// </summary>
        /// <param name="UserID">The ID of the user to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsUserExist(int UserID)
        {
            return clsUserData.IsUserExist(UserID);
        }
        /// <summary>
        /// Checks if a user exists in the database.
        /// </summary>
        /// <param name="UserName">The username of the user to check.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsUserExist(string UserName)
        {
            return clsUserData.IsUserExist(UserName);
        }
        /// <summary>
        /// Checks if a user exists in the database for a specific person ID.
        /// </summary>
        /// <param name="PersonID">The ID of the person associated with the user.</param>
        /// <returns>True if the user exists, false otherwise.</returns>
        public static bool IsUserExistForPersonID(int PersonID)
        {
            return clsUserData.IsUserExistForPersonID(PersonID);
        }
    }
}
