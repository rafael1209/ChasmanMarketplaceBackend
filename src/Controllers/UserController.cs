using MarketplaceBackend.Models;
using MarketplaceBackend.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using MongoDB.Bson;

namespace MarketplaceBackend.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController(UserService userService) : ControllerBase
    {
        [HttpGet("me")]
        public async Task<IActionResult> GetCurrentUser()
        {
            Request.Headers.TryGetValue("Authorization", out var token);

            var user = await userService.GetByAuthTokenAsync(token.ToString());

            if (user == null)
                return Unauthorized(new { message = "Invalid or missing auth token" });

            return Ok(user);
        }

        [HttpGet]
        public async Task<IActionResult> GetAllUsers()
        {
            var users = await userService.GetAllAsync();
            return Ok(users);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetUserById(string id)
        {
            var user = await userService.GetByIdAsync(ObjectId.Parse(id));

            if (user == null) return NotFound();

            return Ok(user);
        }

        [HttpPost]
        public async Task<IActionResult> CreateUser([FromBody] User user)
        {
            await userService.CreateAsync(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.Id }, user);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateUser(string id, [FromBody] User user)
        {
            await userService.UpdateAsync(id, user);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(string id)
        {
            await userService.DeleteAsync(id);
            return NoContent();
        }
    }
}
