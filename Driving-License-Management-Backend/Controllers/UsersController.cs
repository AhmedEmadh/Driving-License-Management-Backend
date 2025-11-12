using Driving_License_Management_Backend.DTOs;
using Driving_License_Management_BusinessLogicLayer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Numerics;

namespace Driving_License_Management_Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        [HttpGet, ProducesResponseType(StatusCodes.Status200OK)]
        public IActionResult GetAllUsers()
        {
            List<clsUser> Users = clsUser.GetAllUsersList();
            List<UserReadDTO> UsersDTOList = new List<UserReadDTO>();
            foreach (clsUser user in Users)
            {
                UserReadDTO userDTO = new UserReadDTO(user);
                UsersDTOList.Add(userDTO);
            }
            return Ok(UsersDTOList);
        }
        [HttpGet("{id}"),ProducesResponseType(StatusCodes.Status200OK),ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult GetUserById(int id)
        {
            var user = clsUser.FindByUserID(id);
            if (user == null)
            {
                return NotFound(user);
            }
            return Ok(new UserReadDTO(user));
        }
        [HttpPost, ProducesResponseType(StatusCodes.Status201Created), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult AddNewUser([FromBody] UserUpdateDTO userDTO)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var newUser = new clsUser();
            userDTO.MapValuesToEntity(newUser);
            bool isAdded = newUser.Save();
            if (isAdded)
            {
                return CreatedAtAction(nameof(GetUserById), new { id = newUser.UserID }, new UserReadDTO(newUser));
            }
            else
            {
                return BadRequest("Failed to add new user.");
            }
        }
        [HttpPut("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status400BadRequest)]
        public IActionResult UpdateUser(int id, [FromBody] UserUpdateDTO userDTO)
        {
            userDTO.id = id;
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var existingUser = clsUser.FindByUserID(id);
            if (existingUser == null)
            {
                return NotFound("User Not Found");
            }
            userDTO.MapValuesToEntity(existingUser);
            bool isUpdated = existingUser.Save();
            if (isUpdated)
            {
                return Ok(new UserReadDTO(existingUser));
            }
            else
            {
                return BadRequest("Failed to update user.");
            }
        }
        [HttpDelete("{id}"), ProducesResponseType(StatusCodes.Status200OK), ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult DeleteUser(int id)
        {
            var user = clsUser.FindByUserID(id);
            if (user == null)
            {
                return NotFound("User Not Found");
            }
            bool isDeleted = clsUser.DeleteUser(user.UserID);
            if (isDeleted)
            {
                return Ok("User Deleted Successfully");
            }
            else
            {
                return BadRequest("Failed to delete user.");
            }
        }
    }
}
