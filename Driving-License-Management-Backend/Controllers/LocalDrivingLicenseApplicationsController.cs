using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LocalDrivingLicenseApplicationsController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<LocalDrivingLicenseApplicationReadDTO>> GetAllLocalDrivingLicenseApplications()
        {
            var applications = clsLocalDrivingLicenseApplication.GetAllLocalDrivingLicenseApplicationsList();
            var applicationDTOs = applications.Select(app => new LocalDrivingLicenseApplicationReadDTO(app)).ToList();
            return Ok(applicationDTOs);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public ActionResult<LocalDrivingLicenseApplicationReadDTO> GetLocalDrivingLicenseApplicationById(int id)
        {
            var application = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(id);
            if (application == null)
            {
                return NotFound();
            }
            var applicationDTO = new LocalDrivingLicenseApplicationReadDTO(application);
            return Ok(applicationDTO);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LocalDrivingLicenseApplicationReadDTO> AddNewLocalDrivingLicenseApplication([FromBody] LocalDrivingLicenseApplicationUpdateDTO applicationCreateDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            var newApplication = new clsLocalDrivingLicenseApplication();
            applicationCreateDTO.MapValuesToEntity(newApplication);
            newApplication.Mode = clsLocalDrivingLicenseApplication.enMode.AddNew;
            if (newApplication.Save() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var applicationReadDTO = new LocalDrivingLicenseApplicationReadDTO(newApplication);
            return CreatedAtAction(nameof(GetLocalDrivingLicenseApplicationById), new { id = applicationReadDTO.LocalDrivingLicenseApplicationId }, applicationReadDTO);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string)), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<LocalDrivingLicenseApplicationReadDTO> UpdateLocalDrivingLicenseApplication(int id, [FromBody] LocalDrivingLicenseApplicationUpdateDTO applicationUpdateDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest(ModelState);
            }
            applicationUpdateDTO.LocalDrivingLicenseApplicationId = id;
            var existingApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(id);
            if (existingApplication == null)
            {
                return NotFound();
            }
            applicationUpdateDTO.MapValuesToEntity(existingApplication);
            existingApplication.Mode = clsLocalDrivingLicenseApplication.enMode.Update;
            if (existingApplication.Save() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            var applicationReadDTO = new LocalDrivingLicenseApplicationReadDTO(existingApplication);
            return Ok(applicationReadDTO);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string)), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult DeleteLocalDrivingLicenseApplication(int id)
        {
            var existingApplication = clsLocalDrivingLicenseApplication.FindByLocalDrivingAppLicenseID(id);
            if (existingApplication == null)
            {
                return NotFound();
            }
            if (existingApplication.Delete() == false)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
            return NoContent();
        }

    }
}
