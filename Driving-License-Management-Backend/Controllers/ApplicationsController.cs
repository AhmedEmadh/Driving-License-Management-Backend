using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationsController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ApplicationReadDTO>> GetAllApplications()
        {
            List<clsApplication> applications = clsApplication.GetAllApplicationsList();
            List<ApplicationReadDTO> applicationDTOs = new List<ApplicationReadDTO>();
            foreach (var application in applications)
            {
                applicationDTOs.Add(new ApplicationReadDTO(application));
            }
            return Ok(applicationDTOs);
        }
        [HttpGet("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound,Type = typeof(string))]
        public ActionResult<ApplicationReadDTO> GetApplicationById(int id)
        {
            var application = clsApplication.FindBaseApplication(id);
            if (application == null)
            {
                return NotFound("Application Not Found");
            }
            ApplicationReadDTO dto = new ApplicationReadDTO(application);
            return Ok(dto);
        }
        [HttpPost]
        public ActionResult<ApplicationReadDTO> AddNewApplication([FromBody] ApplicationUpdateDTO applicationDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newApplication = new clsApplication();
            applicationDTO.MapValuesToEntity(newApplication);
            bool isAdded = newApplication.Save();
            if (!isAdded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add new application.");
            }
            applicationDTO.id = newApplication.ApplicationID;
            return CreatedAtAction(nameof(GetApplicationById), new { id = newApplication.ApplicationID }, new ApplicationReadDTO(newApplication));
        }
        [HttpPut("{id}"), ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(BadRequestObjectResult)), ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string)), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string)), ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<ApplicationReadDTO> UpdateApplication(int id, [FromBody] ApplicationUpdateDTO applicationDTO)
        {
            applicationDTO.id = id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingApplication = clsApplication.FindBaseApplication(id);
            if (existingApplication == null)
            {
                return NotFound("Application Not Found");
            }
            applicationDTO.MapValuesToEntity(existingApplication);
            bool isUpdated = existingApplication.Save();
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError,"Failed to update application.");
            }
            return Ok(new ApplicationReadDTO(existingApplication));
        }
        [HttpDelete("{id}"), ProducesResponseType(StatusCodes.Status200OK, Type = typeof(string)), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string)), ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(string))]
        public ActionResult<string> DeleteApplication(int id)
        {
            var existingApplication = clsApplication.FindBaseApplication(id);
            if (existingApplication == null)
            {
                return NotFound("Application Not Found");
            }
            bool isDeleted = existingApplication.Delete();
            if (!isDeleted)
            {
                return BadRequest("Failed to delete application.");
            }
            return Ok("Application deleted successfully.");
        }
    }
}
