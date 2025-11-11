using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PeopleController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetAllPeople()
        {
            var people = clsPerson.GetAllPeopleList();
            List<PersonDTO> PersonDTOList = new List<PersonDTO>();
            foreach (var person in people)
            {
                PersonDTOList.Add(new PersonDTO(person));
            }
            return Ok(PersonDTOList);
        }
        [HttpGet("{id}", Name = "GetPersonById")]
        public IActionResult SearchPersonById(int id)
        {
            var person = clsPerson.Find(id);
            if (person != null)
            {
                return Ok(new PersonDTO(person));
            }
            else
            {
                return NotFound(person);
            }
        }
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewPerson([FromBody] PersonDTO person)
        {
            // 1. Validate the model
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            if (person.Gender != "Male" && person.Gender != "Female")
            {
                return BadRequest("Gender is Invalid");
            }
            // 2. Create a new Person object from DTO
            var newPerson = new clsPerson
            {
                NationalNo = person.NationalNumber,
                FirstName = person.FirstName,
                SecondName = person.SecoundName,
                ThirdName = person.ThirdName,
                LastName = person.LastName,
                DateOfBirth = person.DateOfBirth,
                Gender = (person.Gender == "Male") ? (short)clsPerson.enGender.Male : (short)clsPerson.enGender.Male,
                Address = person.Address,
                PhoneNumber = person.PhoneNumber,
                Email = person.Email,
                NationalityCountryID = Convert.ToInt32(person.CountryId),
                ImagePath = person.ImageURL
            };

            // 3. Call your business logic to save to DB
            bool isSaved = newPerson.Save(); // assuming Save() inserts into the database

            if (!isSaved)
                return BadRequest("Failed to add new person.");

            person.Id = newPerson.PersonID;
            // 4. Return response with the new resource location
            return CreatedAtRoute(
                "GetPersonById",                 // you should have a GET method with this name
                new { id = newPerson.PersonID },       // route values
                person                           // returned object
            );
        }
        clsPerson _MapDTOToPersonData(PersonDTO source)
        {
            clsPerson target = new clsPerson
            {
                NationalNo = source.NationalNumber,
                FirstName = source.FirstName,
                SecondName = source.SecoundName,
                ThirdName = source.ThirdName,
                LastName = source.LastName,
                DateOfBirth = source.DateOfBirth,
                Gender = (source.Gender == "Male") ? (short)clsPerson.enGender.Male : (short)clsPerson.enGender.Female,
                Address = source.Address,
                PhoneNumber = source.PhoneNumber,
                Phone = source.PhoneNumber,
                Email = source.Email,
                NationalityCountryID = source.CountryId,
                ImagePath = source.ImageURL
            };
            return target;
        }
        

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult UpdatePerson(int id, [FromBody] PersonDTO person)
        {
            person.Id = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var UpdatedPerson = clsPerson.Find(person.Id);
            if (UpdatedPerson == null)
            {
                return NotFound("Person is not found");
            }
            person.MapValuesToEntity(UpdatedPerson);
            bool isSaved = UpdatedPerson.Save();
            if (!isSaved)
                return BadRequest("Failed to update person.");
            return Ok(new PersonDTO(UpdatedPerson));

        }
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult DeletePerson(int id)
        {
            var person = clsPerson.Find(id);
            if (person == null)
            {
                return NotFound("Person is not found");
            }
            bool isDeleted = person.DeletePerson();
            if (!isDeleted)
                return BadRequest("Failed to delete person.");
            return Ok("Person is Deleted Successfully");
        }
    }
}
