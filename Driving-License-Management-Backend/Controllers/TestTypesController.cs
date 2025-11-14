using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestTypesController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<TestTypeDTO>> GetAllTestTypes()
        {
            List<clsTestType> testTypes = clsTestType.GetAllTestTypesList();
            List<TestTypeDTO> testTypeDTOs = testTypes.ConvertAll(tt => new TestTypeDTO
            {
                id = tt.ID,
                Title = tt.Title,
                Description = tt.Description,
                Fees = tt.Fees
            });
            return Ok(testTypeDTOs);
        }
        [HttpGet("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<TestTypeDTO> GetTestTypeByID(int id)
        {
            clsTestType testType = clsTestType.Find(id);
            if (testType == null)
            {
                return NotFound();
            }
            TestTypeDTO testTypeDTO = new TestTypeDTO
            {
                id = testType.ID,
                Title = testType.Title,
                Description = testType.Description,
                Fees = testType.Fees
            };
            return Ok(testTypeDTO);
        }
        [HttpPost, ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TestTypeDTO> AddNewTestType([FromBody] TestTypeDTO testTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            var newTestType = new clsTestType();
            testTypeDTO.MapValuesToEntity(newTestType);
            newTestType.Mode = clsTestType.enMode.AddNew;
            bool isAdded = newTestType.Save();
            if (!isAdded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to add new test type.");
            }
            testTypeDTO.id = newTestType.ID;
            return CreatedAtAction(nameof(GetTestTypeByID), new { id = newTestType.ID }, testTypeDTO);
        }
        [HttpPut("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult<TestTypeDTO> UpdateTestType(int id, [FromBody] TestTypeDTO testTypeDTO)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            testTypeDTO.id = id;
            var existingTestType = clsTestType.Find(id);
            if (existingTestType == null)
            {
                return NotFound();
            }
            testTypeDTO.MapValuesToEntity(existingTestType);
            existingTestType.Mode = clsTestType.enMode.Update;
            bool isUpdated = existingTestType.Save();
            if (!isUpdated)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to update test type.");
            }
            return Ok(new TestTypeDTO
            {
                id = existingTestType.ID,
                Title = existingTestType.Title,
                Description = existingTestType.Description,
                Fees = existingTestType.Fees
            });
        }
        [HttpDelete("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound), ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public ActionResult DeleteTestType(int id)
        {
            var existingTestType = clsTestType.Find(id);
            if (existingTestType == null)
            {
                return NotFound();
            }
            bool isDeleted = existingTestType.Delete();
            if (!isDeleted)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Failed to delete test type.");
            }
            return Ok();
        }
    }
}
