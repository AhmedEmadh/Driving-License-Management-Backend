using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    public class clsTestType
    {
        public enum enMode { AddNew, Update };
        public enMode Mode = enMode.AddNew;
        public enum enTestType { VisionTest = 1, WrittenTest = 2, StreetTest = 3 };
        public int ID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public float Fees { get; set; }
        /// <summary>
        /// Initializes a new instance of the clsTestType class, setting default values for its properties.
        /// </summary>
        public clsTestType()
        {
            this.ID = (int)enTestType.VisionTest;
            this.Title = string.Empty;
            this.Description = string.Empty;
            this.Fees = 0;
            Mode = enMode.AddNew;
        }
        /// <summary>
        /// Initializes a new instance of the clsTestType class with the provided parameters.
        /// </summary>
        /// <param name="ID">The ID of the test type.</param>
        /// <param name="TestTypeTitle">The title of the test type.</param>
        /// <param name="Description">The description of the test type.</param>
        /// <param name="TestTypeFees">The fees associated with the test type.</param>
        private clsTestType(int ID, string TestTypeTitle, string Description, float TestTypeFees)
        {
            this.ID = (int)ID;
            this.Title = TestTypeTitle;
            this.Description = Description;
            this.Fees = TestTypeFees;
            Mode = enMode.Update;
        }
        /// <summary>
        /// Adds a new test type to the data storage.
        /// </summary>
        /// <returns>True if the test type was added successfully, false otherwise.</returns>
        private bool _AddNewTestType()
        {
            int CreatedID = clsTestTypeData.AddNewTestType(this.Title, this.Description, this.Fees);
            return (CreatedID != -1);
        }
        /// <summary>
        /// Updates an existing test type in the database.
        /// </summary>
        /// <returns>True if the test type was updated successfully, false otherwise.</returns>
        private bool _UpdateTestType()
        {
            return clsTestTypeData.UpdateTestType(this.ID, this.Title, this.Description, this.Fees);
        }
        /// <summary>
        /// Finds a test type by its unique identifier.
        /// </summary>
        /// <param name="TestTypeID">The ID of the test type to find.</param>
        /// <returns>A clsTestType object if found, otherwise null.</returns>
        public static clsTestType Find(int TestTypeID)
        {
            string TestTypeTitle = string.Empty, TestTypeDescription = string.Empty;
            float TestTypeFees = 0;
            bool isFound = clsTestTypeData.GetTestTypeInfoByID(TestTypeID, ref TestTypeTitle, ref TestTypeDescription, ref TestTypeFees);
            if (isFound)
            {
                return new clsTestType(TestTypeID, TestTypeTitle, TestTypeDescription, TestTypeFees);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieves all test types from the database.
        /// </summary>
        /// <returns>A DataTable containing all test types.</returns>
        public static DataTable GetAllTestTypes()
        {
            DataTable dataTable = clsTestTypeData.GetAllTestTypes();
            return dataTable;
        }
        public static List<clsTestType> GetAllTestTypesList()
        {
            DataTable dt = GetAllTestTypes();
            List<clsTestType> testTypes = new List<clsTestType>();
            foreach (DataRow row in dt.Rows)
            {
                clsTestType testType = new clsTestType
                {
                    ID = Convert.ToInt32(row["TestTypeID"]),
                    Title = row["TestTypeTitle"].ToString(),
                    Description = row["TestTypeDescription"].ToString(),
                    Fees = float.Parse(row["TestTypeFees"].ToString()),
                    Mode = enMode.Update
                };
                testTypes.Add(testType);
            }
            return testTypes;
        }
        /// <summary>
        /// Saves the current test type to the database based on its current mode.
        /// </summary>
        /// <returns>True if the test type was saved successfully, false otherwise.</returns>
        public bool Save()
        {
            switch (Mode)
            {
                case enMode.AddNew:
                    if (_AddNewTestType())
                    {
                        Mode = enMode.Update;
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                case enMode.Update:
                    return _UpdateTestType();
            }
            return false;
        }
        /// <summary>
        /// Converts an enTestType enum value to its corresponding string representation.
        /// </summary>
        /// <param name="TestType">The enTestType enum value to convert.</param>
        /// <returns>A string representing the test type (e.g., "Vision Test", "Written Test", etc.).</returns>
        static public string TestTypeToString(enTestType TestType)
        {
            switch (TestType)
            {
                case enTestType.VisionTest:
                    return "Vision Test";
                case enTestType.WrittenTest:
                    return "Written Test";
                case enTestType.StreetTest:
                    return "Street Test";
                default:
                    return string.Empty;
            }
        }
    }
}
