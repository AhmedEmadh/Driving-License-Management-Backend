using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DriversController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllDrivers()
        {
            List<clsDriver> drivers = clsDriver.GetAllDriversList();
            List<DriverReadDTO> driverDTOs = new List<DriverReadDTO>();
            foreach (var driver in drivers)
            {
                driverDTOs.Add(new DriverReadDTO(driver));
            }
            return Ok(driverDTOs);
        }
        [HttpGet("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetDriverById(int id)
        {
            var driver = clsDriver.FindByDriverID(id);
            if (driver == null)
            {
                return NotFound("Driver Not Found");
            }
            DriverReadDTO dto = new DriverReadDTO(driver);
            return Ok(dto);
        }
        [HttpPost, ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewDriver([FromBody] DriverUpdateDTO driverDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newDriver = new clsDriver();
            driverDTO.MapValuesToEntity(newDriver);
            bool isAdded = newDriver.Save();
            if (!isAdded)
            {
                return BadRequest("Failed to add new driver.");
            }
            driverDTO.id = newDriver.DriverID;
            return CreatedAtAction(nameof(GetDriverById), new { id = newDriver.DriverID }, new DriverReadDTO(newDriver));
        }
        [HttpPut("{id}"), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult UpdateDriver(int id, [FromBody] DriverUpdateDTO driverDTO)
        {
            driverDTO.id = id;
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var existingDriver = clsDriver.FindByDriverID(id);
            if (existingDriver == null)
            {
                return NotFound(existingDriver);
            }
            driverDTO.MapValuesToEntity(existingDriver);
            bool isUpdated = existingDriver.Save();
            if (!isUpdated)
            {
                return BadRequest("Failed to update driver.");
            }
            return Ok(new DriverReadDTO(existingDriver));
        }
        [HttpDelete("{id}"),ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeleteDriver(int id)
        {
            var existingDriver = clsDriver.FindByDriverID(id);
            if (existingDriver == null)
            {
                return NotFound("Driver Not Found");
            }
            bool isDeleted = existingDriver.Delete();
            if (!isDeleted)
            {
                return BadRequest("Failed to delete driver.");
            }
            return Ok("Driver deleted successfully.");
        }

    }
}
