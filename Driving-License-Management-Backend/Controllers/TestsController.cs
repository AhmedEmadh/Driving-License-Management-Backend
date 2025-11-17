using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestsController : ControllerBase
    {
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<TestReadDTO>> GetAllTests()
        {
            var tests = clsTest.GetAllTestsList();
            var testDTOs = tests.Select(test => new TestReadDTO(test)).ToList();
            return Ok(testDTOs);
        }
        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string))]
        public ActionResult<TestReadDTO> GetTestById(int id)
        {
            var test = clsTest.Find(id);
            if (test == null)
            {
                return NotFound();
            }
            var testDTO = new TestReadDTO(test);
            return Ok(testDTO);
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public ActionResult<TestReadDTO> AddNewTest([FromBody] TestUpdateDTO testCreateDTO)
        {
            var test = new clsTest();
            testCreateDTO.MapValuesToEntity(test);
            bool isAdded = test.Save();
            if (!isAdded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add new test.");
            }
            var testDTO = new TestReadDTO(test);
            return CreatedAtAction(nameof(GetTestById), new { id = test.TestID }, testDTO);
        }
        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public ActionResult<TestReadDTO> UpdateTest(int id, [FromBody] TestUpdateDTO testUpdateDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            testUpdateDTO.Id = id;
            var test = clsTest.Find(id);
            if (test == null)
            {
                return NotFound();
            }
            testUpdateDTO.MapValuesToEntity(test);
            bool isUpdated = test.Save();
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update test.");
            }
            var testDTO = new TestReadDTO(test);
            return Ok(testDTO);
        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent), ProducesResponseType(StatusCodes.Status404NotFound, Type = typeof(string)), ProducesResponseType(StatusCodes.Status500InternalServerError, Type = typeof(string))]
        public ActionResult DeleteTest(int id)
        {
            var test = clsTest.Find(id);
            if (test == null)
            {
                return NotFound();
            }
            bool isDeleted = test.Delete();
            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete test.");
            }
            return NoContent();
        }
    }
}
