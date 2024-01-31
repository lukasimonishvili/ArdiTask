using Domain.DTO;
using Domain.Interfaces;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        [HttpPost("Add")]
        public IActionResult AddUser([FromBody] UserDTO user)
        {
            var result = _userService.AddUser(user);
            return Ok(result);
        }

        [HttpPost("AddInsurance")]
        public IActionResult AddInsuranceToUser([FromQuery] string userId, string insuranceId)
        {
            try
            {
                var result = _userService.AddInsuranceToUser(userId, insuranceId);
                return Ok(result);
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (NotIntegerException ex)
            {
                return BadRequest(ex.Message);
            }
            catch (DataExistsException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpGet("GetById/{id}")]
        public IActionResult GetUserById(int id)
        {
            try
            {
                var user = _userService.GetUserById(id);
                return Ok(user);
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpPut("Update")]
        public IActionResult UpdateUser(UserUpdaetDTO user)
        {
            try
            {
                _userService.UpdateUser(user);
                return Ok("Updated");
            }
            catch (DataNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("DeleteInsurance")]
        public IActionResult DeleteInsuranceForUser([FromQuery] string UserId, string InsuranceId)
        {
            try
            {
                _userService.DeleteInsuranceForUser(UserId, InsuranceId);
                return Ok("deleted");
            }
            catch (DataExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpDelete("Delete/{id}")]
        public IActionResult DeleteUser(int id)
        {
            try
            {
                _userService.DeleteUser(id);
                return Ok("deleted");
            }
            catch (DataExistsException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
