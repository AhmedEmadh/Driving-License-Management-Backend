using Driving_License_Management_BusinessLogicLayer;
using System.ComponentModel;
using System.Text.Json.Serialization;

namespace Driving_License_Management_Backend.DTOs
{
    public class CountryDTO
    {
        public CountryDTO() { }
        public CountryDTO(clsCountry country)
        {
            this.ID = country.CountryID;
            this.CountryName = country.CountryName;
        }
        [JsonPropertyName("ID")]
        public int ID { get; private set; }
        [JsonPropertyName("CountryName")]
        public string CountryName { get; }

    }
}
