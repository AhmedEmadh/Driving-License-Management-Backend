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
        [HttpGet("{ID}", Name = "GetLicenseClassByID"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetLicenseClassByID(int ID)
        {
            // 1. Find the LicenseClass by ID
            var licenseClass = clsLicenseClass.Find(ID);
            // 2. Check if LicenseClass exists
            if (licenseClass == null)
            {
                return NotFound("License Class is not found");
            }
            // 3. Return the LicenseClassDTO
            return Ok(new LicenseClassDTO(licenseClass));

        }

        [HttpPost, ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewLicenseClass([FromBody]LicenseClassDTO licenseClassDTO)
        {
            // 1. Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // 2. Create a new LicenseClass object from DTO
            clsLicenseClass newlicenseClass = new clsLicenseClass();
            licenseClassDTO.MapValuesToEntity(newlicenseClass);
            // 3. Call your business logic to save to DB
            newlicenseClass.Mode = clsLicenseClass.enMode.AddNew;
            if (!newlicenseClass.Save())
            {
                return BadRequest("Failed to add new License Class");
            }
            // 4. Return response with the new resource location
            return CreatedAtRoute("GetLicenseClassByID", new { ID = newlicenseClass.LicenseClassID }, new LicenseClassDTO(newlicenseClass));
        }

        [HttpPut("{ID}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdateLicenseClass(int ID, [FromBody] LicenseClassDTO licenseClassDTO)
        {
            // 1. Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // 2. Check if the LicenseClass exists
            clsLicenseClass licenseClass = clsLicenseClass.Find(ID);
            if (licenseClass == null)
            {
                return NotFound("License Class is not found");
            }
            // 3. Map updated fields from DTO to entity
            licenseClassDTO.MapValuesToEntity(licenseClass);
            // Set ID to ID paramenter
            licenseClassDTO.ID = ID;
            // 4. Set mode to Update
            licenseClass.Mode = clsLicenseClass.enMode.Update;
            // 5. Save the updated LicenseClass
            if (!licenseClass.Save())
            {
                return BadRequest("Failed to update License Class");
            }
            // 6. Return the updated LicenseClassDTO
            return Ok(new LicenseClassDTO(licenseClass));

        }

        [HttpDelete("{ID}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteLicenseClass(int ID)
        {
            // 1. Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            // 2. Check if the LicenseClass exists
            clsLicenseClass licenseClass = clsLicenseClass.Find(ID);
            if (licenseClass == null)
            {
                return NotFound("License Class is not found");
            }
            // 3. Here you would call a delete method in your business logic layer
            if (!licenseClass.Delete())
            {
                return BadRequest("Failed to delete License Class");
            }
            // 4. Return a success response
            return Ok("License Class deleted successfully");
        }
    }
}
