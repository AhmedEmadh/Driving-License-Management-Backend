using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DetainedLicensesController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<DetainedLicenseReadDTO>> GetAllDetainedLicenses()
        {
            var detainedLicenses = clsDetainedLicense.GetAllDetainedLicensesList();
            var detainedLicenseDTOs = detainedLicenses.Select(dl => new DetainedLicenseReadDTO(dl)).ToList();
            return Ok(detainedLicenseDTOs);
        }
        [HttpGet("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<DetainedLicenseReadDTO> GetDetainedLicenseById(int id)
        {
            var detainedLicense = clsDetainedLicense.Find(id);
            if (detainedLicense == null)
            {
                return NotFound();
            }
            var detainedLicenseDTO = new DetainedLicenseReadDTO(detainedLicense);
            return Ok(detainedLicenseDTO);
        }
        [HttpPost, ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DetainedLicenseReadDTO> AddNewDetainedLicense([FromBody] DetainedLicenseUpdateDTO detainedLicenseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var detainedLicense = new clsDetainedLicense();
            detainedLicenseDTO.MapValuesToEntity(detainedLicense);
            bool isSaved = detainedLicense.Save();
            if (!isSaved)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var createdDetainedLicenseDTO = new DetainedLicenseReadDTO(detainedLicense);
            return CreatedAtAction(nameof(GetDetainedLicenseById), new { id = createdDetainedLicenseDTO.Id }, createdDetainedLicenseDTO);
        }
        [HttpPut("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<DetainedLicenseReadDTO> UpdateDetainedLicense(int id, [FromBody] DetainedLicenseUpdateDTO detainedLicenseDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            detainedLicenseDTO.Id = id;
            var existingDetainedLicense = clsDetainedLicense.Find(id);
            if (existingDetainedLicense == null)
            {
                return NotFound();
            }
            detainedLicenseDTO.MapValuesToEntity(existingDetainedLicense);
            bool isSaved = existingDetainedLicense.Save();
            if (!isSaved)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var updatedDetainedLicenseDTO = new DetainedLicenseReadDTO(existingDetainedLicense);
            return Ok(updatedDetainedLicenseDTO);
        }
        [HttpDelete("{id}"), ProducesResponseType(StatusCodes.Status204NoContent), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteDetainedLicense(int id)
        {
            var existingDetainedLicense = clsDetainedLicense.Find(id);
            if (existingDetainedLicense == null)
            {
                return NotFound();
            }
            bool isDeleted = existingDetainedLicense.Delete();
            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
