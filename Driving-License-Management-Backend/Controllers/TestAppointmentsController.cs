using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestAppointmentsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<TestAppointmentReadDTO>> GetAllTestAppointments()
        {
            var TestAppointmentsList = clsTestAppointment.GetAllTestAppointmentsList();
            var TestAppointmentsDTOList = TestAppointmentsList.Select(ta => new TestAppointmentReadDTO(ta)).ToList();
            return Ok(TestAppointmentsDTOList);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TestAppointmentReadDTO> GetTestAppointmentById(int id)
        {
            var testAppointment = clsTestAppointment.Find(id);
            if (testAppointment == null)
            {
                return NotFound();
            }
            var testAppointmentDTO = new TestAppointmentReadDTO(testAppointment);
            return Ok(testAppointmentDTO);

        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public ActionResult<TestAppointmentReadDTO> CreateTestAppointment([FromBody] TestAppointmentsUpdateDTO testAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var testAppointment = new clsTestAppointment();
            testAppointmentDTO.MapValuesToEntity(testAppointment);
            if (!testAppointment.Save())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while saving the test appointment.");
            }
            var createdTestAppointmentDTO = new TestAppointmentReadDTO(testAppointment);
            return CreatedAtAction(nameof(GetTestAppointmentById), new { id = createdTestAppointmentDTO.ID }, createdTestAppointmentDTO);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<TestAppointmentReadDTO> UpdateTestAppointment(int id, [FromBody] TestAppointmentsUpdateDTO testAppointmentDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            testAppointmentDTO.ID = id;
            var existingTestAppointment = clsTestAppointment.Find(id);
            if (existingTestAppointment == null)
            {
                return NotFound();
            }
            testAppointmentDTO.MapValuesToEntity(existingTestAppointment);
            if (!existingTestAppointment.Save())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while updating the test appointment.");
            }
            var updatedTestAppointmentDTO = new TestAppointmentReadDTO(existingTestAppointment);
            return Ok(updatedTestAppointmentDTO);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public IActionResult DeleteTestAppointment(int id)
        {
            var existingTestAppointment = clsTestAppointment.Find(id);
            if (existingTestAppointment == null)
            {
                return NotFound();
            }
            if (!existingTestAppointment.Delete())
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while deleting the test appointment.");
            }
            return NoContent();
        }
    }
}
