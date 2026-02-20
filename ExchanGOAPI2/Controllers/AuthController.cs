using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace ExchanGOAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IUserService _userService;

        public AuthController(IUserService userService) => _userService = userService;

        [HttpPost("register")]
        public async Task<ActionResult<UserDto>> Register(CreateUserRequest request)
        {
            try
            {
                var user = await _userService.CreateUserAsync(request);
                return CreatedAtAction(nameof(Register), new { id = user.Id }, user);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        [HttpPost("login")]
        public async Task<ActionResult<LoginResponse>> Login(LoginRequest request)
        {
            var loginResponse = await _userService.LoginAsync(request);
            if (loginResponse == null)
                return Unauthorized(new { message = "Invalid credentials." });

            return Ok(loginResponse);
        }
    }
}
