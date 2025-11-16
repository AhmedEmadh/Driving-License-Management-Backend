using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class InternationalLicensesController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<InternationalLicenseReadDTO>> GetAllInternationalLicenses()
        {
            var internationalLicenses = clsInternationalLicense.GetAllInternationalLicensesList();
            var internationalLicenseDTOs = internationalLicenses.Select(il => new InternationalLicenseReadDTO(il)).ToList();
            return Ok(internationalLicenseDTOs);
        }
        [HttpGet("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public ActionResult<InternationalLicenseReadDTO> GetInternationalLicenseById(int id)
        {
            var internationalLicense = clsInternationalLicense.Find(id);
            if (internationalLicense == null)
            {
                return NotFound();
            }
            var internationalLicenseDTO = new InternationalLicenseReadDTO(internationalLicense);
            return Ok(internationalLicenseDTO);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InternationalLicenseReadDTO> AddNewInternationalLicense([FromBody] InternationalLicenseUpdateDTO internationalLicenseCreateDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var newInternationalLicense = new clsInternationalLicense();
            internationalLicenseCreateDTO.MapValuesToEntity(newInternationalLicense);
            newInternationalLicense.Mode = clsInternationalLicense.enMode.AddNew;
            if (newInternationalLicense.Save() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var internationalLicenseReadDTO = new InternationalLicenseReadDTO(newInternationalLicense);
            return CreatedAtAction(nameof(GetInternationalLicenseById), new { id = internationalLicenseReadDTO.Id }, internationalLicenseReadDTO);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string)), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<InternationalLicenseReadDTO> UpdateInternationalLicense(int id, [FromBody] InternationalLicenseUpdateDTO internationalLicenseUpdateDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            internationalLicenseUpdateDTO.Id = id;
            var existingInternationalLicense = clsInternationalLicense.Find(id);
            if (existingInternationalLicense == null)
            {
                return NotFound();
            }
            internationalLicenseUpdateDTO.MapValuesToEntity(existingInternationalLicense);
            existingInternationalLicense.Mode = clsInternationalLicense.enMode.Update;
            if (existingInternationalLicense.Save() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var internationalLicenseReadDTO = new InternationalLicenseReadDTO(existingInternationalLicense);
            return Ok(internationalLicenseReadDTO);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string)), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteInternationalLicense(int id)
        {
            clsInternationalLicense existingInternationalLicense = clsInternationalLicense.Find(id);
            if (existingInternationalLicense == null)
            {
                return NotFound();
            }
            if (existingInternationalLicense.Delete() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }
    }
}
