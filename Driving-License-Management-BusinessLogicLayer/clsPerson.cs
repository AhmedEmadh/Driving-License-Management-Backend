using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsPerson
    {
        public enum enMode { AddNew = 0, Update = 1 };
        public enMode Mode = enMode.AddNew;
        public enum enGender
        {
            Male = 0, Female = 1
        }
        public int PersonID { get; set; }
        public string FirstName { get; set; }
        public string SecondName { get; set; }
        public string? ThirdName { get; set; }
        public string LastName { get; set; }
        public string FullName
        {
            get
            {
                if (!string.IsNullOrEmpty(ThirdName))
                    return FirstName + " " + SecondName + " " + ThirdName + " " + LastName;
                else
                    return FirstName + " " + SecondName + " " + LastName;
            }
        }
        public string NationalNo { get; set; }
        public DateTime DateOfBirth { get; set; }
        public short Gender { get; set; }
        public string Address { get; set; }
        public string PhoneNumber { get; set; }
        public string Phone { set; get; }
        public string Email { set; get; }
        public int NationalityCountryID { get; set; }
        public clsCountry CountryInfo { get; set; }
        private string _ImagePath;
        public string ImagePath { get { return _ImagePath; } set { _ImagePath = value; } }
        /// <summary>
        /// Initializes a new instance of the clsPerson class, setting all properties to their default values.
        /// </summary>
        public clsPerson()
        {
            this.PersonID = -1;
            this.FirstName = string.Empty;
            this.SecondName = string.Empty;
            this.ThirdName = string.Empty;
            this.LastName = string.Empty;
            this.NationalNo = string.Empty;
            this.DateOfBirth = DateTime.Now;
            this.Gender = (short)enGender.Male;
            this.Address = string.Empty;
            this.PhoneNumber = string.Empty;
            this.Phone = string.Empty;
            this.Email = string.Empty;
            this.NationalityCountryID = -1;
            this.CountryInfo = new clsCountry();
            this.ImagePath = string.Empty;

        }
        /// <summary>
        /// Initializes a new instance of the clsPerson class with the specified ID, names, nationality, date of birth, gender, address, phone, email, and image path.
        /// </summary>
        /// <param name="PersonID">The unique identifier for the person.</param>
        /// <param name="FirstName">The first name of the person.</param>
        /// <param name="SecoundName">The second name of the person.</param>
        /// <param name="ThirdName">The third name of the person.</param>
        /// <param name="LastName">The last name of the person.</param>
        /// <param name="NationalNo">The national number of the person.</param>
        /// <param name="DateOfBirth">The date of birth of the person.</param>
        /// <param name="Gender">The gender of the person.</param>
        /// <param name="Address">The address of the person.</param>
        /// <param name="Phone">The phone number of the person.</param>
        /// <param name="Email">The email address of the person.</param>
        /// <param name="NationalityCountryID">The ID of the country of nationality.</param>
        /// <param name="ImagePath">The path to the person's image.</param>
        private clsPerson(int PersonID, string FirstName, string SecoundName, string ThirdName, string LastName, string NationalNo, DateTime DateOfBirth, short Gender, string Address, string Phone, string Email, int NationalityCountryID, string ImagePath)
        {
            this.PersonID = PersonID;
            this.FirstName = FirstName;
            this.SecondName = SecoundName;
            this.ThirdName = ThirdName;
            this.LastName = LastName;
            this.NationalNo = NationalNo;
            this.DateOfBirth = DateOfBirth;
            this.Gender = Gender;
            this.Address = Address;
            this.Phone = Phone;
            this.Email = Email;
            this.NationalityCountryID = NationalityCountryID;
            this.ImagePath = ImagePath;
            CountryInfo = clsCountry.Find(this.NationalityCountryID);
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new person to the database.
        /// </summary>
        /// <returns>True if the person was added successfully, false otherwise.</returns>
        private bool _AddNewPerson()
        {
            //Call DataAccessLayer
            this.PersonID = clsPersonData.AddNewPerson(this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
            return (this.PersonID != -1);
        }
        /// <summary>
        /// Updates an existing person in the database.
        /// </summary>
        /// <returns>True if the person was updated successfully, false otherwise.</returns>
        private bool _UpdatePerson()
        {
            //Call DataAccessLayer
            return clsPersonData.UpdatePerson(this.PersonID, this.FirstName, this.SecondName, this.ThirdName, this.LastName, this.NationalNo, this.DateOfBirth, this.Gender, this.Address, this.Phone, this.Email, this.NationalityCountryID, this.ImagePath);
        }
        /// <summary>
        /// Finds a person in the database by their ID.
        /// </summary>
        /// <param name="PersonID">The ID of the person to find.</param>
        /// <returns>A clsPerson object if found, otherwise null.</returns>
        public static clsPerson Find(int PersonID)
        {
            string FirstName = "", SecoundName = "", ThirdName = "", LastName = "", NationalNo = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gender = 0;

            bool IsFound = clsPersonData.GetPersonInfoByID(PersonID, ref FirstName, ref SecoundName, ref ThirdName, ref LastName, ref NationalNo, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);
            if (IsFound)
            {
                return new clsPerson(PersonID, FirstName, SecoundName, ThirdName, LastName, NationalNo, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Finds a person in the database by their national number.
        /// </summary>
        /// <param name="NationalNo">The national number of the person to find.</param>
        /// <returns>A clsPerson object if found, otherwise null.</returns>
        public static clsPerson Find(string NationalNo)
        {
            int PersonID = -1;
            string FirstName = "", SecoundName = "", ThirdName = "", LastName = "", Address = "", Phone = "", Email = "", ImagePath = "";
            DateTime DateOfBirth = DateTime.Now;
            int NationalityCountryID = -1;
            short Gender = 0;

            bool IsFound = clsPersonData.GetPersonInfoByNationalNo(NationalNo, ref PersonID, ref FirstName, ref SecoundName, ref ThirdName, ref LastName, ref DateOfBirth, ref Gender, ref Address, ref Phone, ref Email, ref NationalityCountryID, ref ImagePath);
            if (IsFound)
            {
                return new clsPerson(PersonID, FirstName, SecoundName, ThirdName, LastName, NationalNo, DateOfBirth, Gender, Address, Phone, Email, NationalityCountryID, ImagePath);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Saves the person to the database.
        /// </summary>
        /// <returns>True if the person was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    {
                        if (_AddNewPerson())
                        {
                            this.Mode = enMode.Update;
                            return true;
                        }
                        else
                        {
                            return false;
                        }

                    }
                case enMode.Update:
                    {
                        return _UpdatePerson();
                    }
            }
            return false;
        }
        /// <summary>
        /// Retrieves all people from the database.
        /// </summary>
        /// <returns>A DataTable containing all people.</returns>
        public static DataTable GetAllPeople()
        {
            return clsPersonData.GetAllPeople();
        }
        public static List<clsPerson> GetAllPeopleList()
        {
            DataTable dt = clsPersonData.GetAllPeople();
            List<clsPerson> people = new List<clsPerson>();
            foreach (DataRow row in dt.Rows)
            {
                clsPerson person = Find(Convert.ToInt32(row["PersonID"]));
                if (person != null)
                {
                    people.Add(person);
                }
            }
            return people;
        }
        /// <summary>
        /// Deletes a person from the database.
        /// </summary>
        /// <returns>True if the person was deleted successfully, false otherwise.</returns>
        public bool DeletePerson()
        {
            return clsPersonData.DeletePerson(this.PersonID);
        }
        /// <summary>
        /// Checks if a person exists in the database.
        /// </summary>
        /// <param name="PersonID">The ID of the person to check.</param>
        /// <returns>True if the person exists, false otherwise.</returns>
        public static bool IsPersonExist(int PersonID)
        {
            return clsPersonData.IsPersonExist(PersonID);
        }
        /// <summary>
        /// Checks if a person exists in the database.
        /// </summary>
        /// <param name="NationalNo">The national number of the person to check.</param>
        /// <returns>True if the person exists, false otherwise.</returns>
        public static bool IsPersonExist(string NationalNo)
        {
            return clsPersonData.IsPersonExist(NationalNo);
        }
        /// <summary>
        /// Converts a Gender value to a string.
        /// </summary>
        /// <param name="Gender">The Gender value to convert.</param>
        /// <returns>A string representation of the Gender value.</returns>
        public static string GenderToString(short Gender)
        {
            if (Gender == (short)enGender.Male)
            {
                return "Male";
            }
            else
            {
                return "Female";
            }
        }
    }
}
