using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationTypesController : ControllerBase
    {
        [HttpGet(Name = "GetAllApplicationTypes")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllApplicationTypes()
        {
            List<clsApplicationType> applicationTypesList = clsApplicationType.GetAllApplicationTypesList();
            List<ApplicationTypeDTO> applicationTypesDTOList = new List<ApplicationTypeDTO>();
            foreach (var applicationType in applicationTypesList)
            {
                applicationTypesDTOList.Add(new ApplicationTypeDTO(applicationType));
            }
            return Ok(applicationTypesDTOList);
        }
        [HttpGet("{ID}", Name = "GetApplicationTypeByID")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult FindApplicationTypeByID(int ID)
        {
            var applicationType = clsApplicationType.Find(ID);
            if (applicationType == null)
            {
                return NotFound("Application Type is not found");

            }
            return Ok(new ApplicationTypeDTO(applicationType));
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewApplicationType(ApplicationTypeDTO applicationTypeDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Invalid data.");
            }
            var newApplicationType = new clsApplicationType();
            applicationTypeDTO.MapValuesToEntity(newApplicationType);
            newApplicationType.Mode = clsApplicationType.enMode.AddNew;
            if (newApplicationType.Save())
            {
                return CreatedAtRoute("GetApplicationTypeByID", new { ID = newApplicationType.ID }, new ApplicationTypeDTO(newApplicationType));
            }
            else
            {
                return BadRequest("Failed to add new Application Type");
            }
        }
        [HttpPut("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateApplicationType(int ID, [FromBody] ApplicationTypeDTO applicationTypeDTO)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Invalid data.");
            }
            var existingApplicationType = clsApplicationType.Find(ID);
            if (existingApplicationType == null)
            {
                return NotFound("Application Type is not found");
            }
            applicationTypeDTO.ID = ID;
            applicationTypeDTO.MapValuesToEntity(existingApplicationType);
            existingApplicationType.Mode = clsApplicationType.enMode.Update;
            if (existingApplicationType.Save())
            {
                return Ok(new ApplicationTypeDTO(existingApplicationType));
            }
            else
            {
                return BadRequest("Failed to update Application Type");
            }
        }
        [HttpDelete("{ID}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteApplicationType(int ID)
        {
            if (ModelState.IsValid == false)
            {
                return BadRequest("Invalid data.");
            }
            var existingApplicationType = clsApplicationType.Find(ID);
            if (existingApplicationType == null)
            {
                return NotFound("Application Type is not found");
            }
            if (existingApplicationType.Delete())
            {
                return Ok("Application Type deleted successfully");
            }
            else
            {
                return BadRequest("Failed to delete Application Type");
            }
        }
    }
}
