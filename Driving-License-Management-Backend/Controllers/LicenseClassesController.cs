using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicenseClassesController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllLicenseClasses()
        {
            var licenseClasses = clsLicenseClass.GetAllLicenseClassesList();
            List<LicenseClassDTO> licenseClassDTOList = new List<LicenseClassDTO>();
            foreach (var licenseClass in licenseClasses)
            {
                var licenseClassDTO = new LicenseClassDTO(licenseClass);
                licenseClassDTOList.Add(licenseClassDTO);
            }
            return Ok(licenseClassDTOList);
        }
        [HttpGet("{ID}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLicenseClassByID(int ID)
        {
            var licenseClass = clsLicenseClass.Find(ID);
            if (licenseClass == null)
            {
                return NotFound("License Class is not found");
            }
            return Ok(new LicenseClassDTO(licenseClass));

        }

        [HttpPost, ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewLicenseClass()
        {
            throw new NotImplementedException();
        }

        [HttpPut("{ID}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateLicenseClass(int ID)
        {
            throw new NotImplementedException();
        }

        [HttpDelete("{ID}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteLicenseClass(int ID)
        {
            throw new NotImplementedException();
        }
    }
}
