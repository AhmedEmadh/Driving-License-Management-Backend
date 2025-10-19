using Driving_License_Management_DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Driving_License_Management_BusinessLogicLayer
{
    /// <summary>
    /// Represents a country with an ID and name.
    /// </summary>
    public class clsCountry
    {
        public int CountryID { get; set; }
        public string CountryName { get; set; }
        /// <summary>
        /// Initializes a new instance of the <see cref="clsCountry"/> class.
        /// </summary>
        public clsCountry()
        {
            CountryID = -1;
            CountryName = string.Empty;
        }
        /// <summary>
        /// Initializes a new instance of the <see cref="clsCountry"/> class with the specified ID and name.
        /// </summary>
        /// <param name="CountryID">The unique identifier for the country.</param>
        /// <param name="CountryName">The name of the country.</param>
        private clsCountry(int CountryID, string CountryName)
        {
            this.CountryID = CountryID;
            this.CountryName = CountryName;
        }
        /// <summary>
        /// Finds a country by its ID.
        /// </summary>
        /// <param name="CountryID">The ID of the country to find.</param>
        /// <returns>The country with the specified ID, or null if not found.</returns>
        public static clsCountry Find(int CountryID)
        {
            string CountryName = string.Empty;
            if (clsCountryData.GetCountryInfoByID(CountryID, ref CountryName))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Finds a country by its name.
        /// </summary>
        /// <param name="CountryName">The name of the country to find.</param>
        /// <returns>The country with the specified name, or null if not found.</returns>
        public static clsCountry Find(string CountryName)
        {
            int CountryID = -1;
            if (clsCountryData.GetCountryInfoByName(CountryName, ref CountryID))
            {
                return new clsCountry(CountryID, CountryName);
            }
            else
            {
                return null;
            }
        }
        /// <summary>
        /// Retrieves a list of all countries.
        /// </summary>
        /// <returns>A DataTable containing all countries.</returns>
        public static DataTable GetAllCountries()
        {
            return clsCountryData.GetAllCountries();
        }
        public static List<clsCountry> GetAllCountriesList()
        {
            DataTable dtCountries = clsCountryData.GetAllCountries();
            List<clsCountry> countries = new List<clsCountry>();
            foreach (DataRow row in dtCountries.Rows)
            {
                countries.Add(new clsCountry(Convert.ToInt32(row["CountryID"]), row["CountryName"].ToString()));
            }
            return countries;
        }
    }
}
