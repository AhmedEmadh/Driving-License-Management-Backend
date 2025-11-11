using Driving_License_Management_Backend.DTOs.Abstraction;
using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class CountryDTO : IDTO<clsCountry>
    {
        #region Properties
        [JsonPropertyName("ID")]
        public int ID { get; private set; }
        [JsonPropertyName("CountryName")]
        public string CountryName { get; private set; }
        #endregion
        #region Constructors
        public CountryDTO() { }
        public CountryDTO(clsCountry country)
        {
            SetValuesFromEntity(country);
        }
        #endregion
        #region Methods
        public void SetValuesFromEntity(clsCountry country)
        {
            this.ID = country.CountryID;
            this.CountryName = country.CountryName;
        }

        public void MapValuesToEntity(clsCountry country)
        {
            country.CountryID = this.ID;
            country.CountryName = this.CountryName;
        }
        #endregion

    }
}
