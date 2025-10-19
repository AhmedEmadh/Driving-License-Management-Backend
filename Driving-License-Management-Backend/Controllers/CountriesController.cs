using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CountriesController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllCountries()
        {
            List<clsCountry> countries = clsCountry.GetAllCountriesList();
            List<CountryDTO> countryDTOs = new List<CountryDTO>();
            foreach (var country in countries)
            {
                countryDTOs.Add(new CountryDTO(country));
            }
            return Ok(countryDTOs);
        }
        [HttpGet("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindCountryByID(int ID)
        {
            var country = clsCountry.Find(ID);
            if(country != null)
            {
                return Ok(new CountryDTO(country));
            }
            else
            {
                return NotFound("Country is not found");
            }
        }
        [HttpGet("Name/{Name}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindCountryByName(string Name)
        {
            var country = clsCountry.Find(Name);
            if (country != null)
            {
                return Ok(new CountryDTO(country));
            }
            else
            {
                return NotFound("Country is not found");
            }
        }
    }
}
