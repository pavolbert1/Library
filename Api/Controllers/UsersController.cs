using LibraryAPI.Core.IServices;
using LibraryAPI.Core.Models;
using LibraryAPI.Core.Exceptions;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Api.Controllers
{
    [ApiController]
    [Route("users")]
    public class UsersController(IUserService userService) : ControllerBase
    {
        private readonly IUserService _userService = userService;

        [HttpGet]
        public IActionResult GetAllUsers()
        {
            var users = _userService.GetAllUsers() ?? throw new NotFoundException("User records not found."); ;

            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUser(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameter is not valid.");
            }

            var user = await _userService.GetUserDetails(id) ?? throw new NotFoundException("User record not found.");

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameters are not valid.");
            }

            await _userService.CreateUser(user);
            return CreatedAtAction(nameof(GetUser), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(int id, [FromBody] User user)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameters are not valid.");
            }

            if (id != user.Id)
            {
                throw new BadRequestException("ID of user is different in body and parameter.");
            }

            await _userService.UpdateUser(user);
            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            if (!ModelState.IsValid)
            {
                throw new BadRequestException("Input parameter is not valid.");
            }

            await _userService.DeleteUser(id);
            return Ok();
        }
    }
}