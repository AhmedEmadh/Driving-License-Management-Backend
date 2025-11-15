using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LicensesController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<LicenseReadDTO>> GetAllLicenses()
        {
            var licenses = Driving_License_Management_BusinessLogicLayer.clsLicense.GetAllLicensesList();
            var licenseDTOs = licenses.Select(license => new LicenseReadDTO(license)).ToList();
            return Ok(licenseDTOs);
        }
        [HttpGet("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LicenseReadDTO> GetLicenseById(int id)
        {
            var license = clsLicense.Find(id);
            if (license == null)
            {
                return NotFound();
            }
            var licenseDTO = new LicenseReadDTO(license);
            return Ok(licenseDTO);
        }
        [HttpPost, ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult)), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LicenseReadDTO> AddNewLicense([FromBody] LicenseUpdateDTO licenseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var license = new clsLicense();
            licenseDTO.MapValuesToEntity(license);
            bool isSaved = license.Save();
            if (!isSaved)
            {
                //Internal Server Error
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var createdLicenseDTO = new LicenseReadDTO(license);
            return CreatedAtAction(nameof(GetLicenseById), new { id = createdLicenseDTO.Id }, createdLicenseDTO);
        }
        [HttpPut("{id}")]
        public ActionResult<LicenseReadDTO> UpdateLicense(int id, [FromBody] LicenseUpdateDTO licenseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            licenseDTO.Id = id;
            var existingLicense = clsLicense.Find(id);
            if (existingLicense == null)
            {
                return NotFound();
            }
            licenseDTO.MapValuesToEntity(existingLicense);
            bool isUpdated = existingLicense.Save();
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the license.");
            }
            var updatedLicenseDTO = new LicenseReadDTO(existingLicense);
            return Ok(updatedLicenseDTO);
        }
        [HttpDelete("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status500InternalServerError), ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteLicense(int id)
        {
            var existingLicense = clsLicense.Find(id);
            if (existingLicense == null)
            {
                return NotFound();
            }
            bool isDeleted = existingLicense.Delete();
            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return Ok();
        }
    }
}
