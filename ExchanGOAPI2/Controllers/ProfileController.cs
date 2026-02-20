using Application.DTOs;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExchanGOAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ProfileController : ControllerBase
    {
        private readonly IUserService _userService;

        public ProfileController(IUserService userService) => _userService = userService;

        [HttpGet("me")]
        public async Task<ActionResult<UserDto>> GetCurrentUser()
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _userService.GetUserByIdAsync(userId);
            if (user is null) return NotFound();
            return Ok(user);
        }

        [HttpPatch("subscription")]
        public async Task<ActionResult<UserDto>> ChangeSubscription(ChangeSubscriptionRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            var user = await _userService.ChangeSubscriptionAsync(userId, request);
            if (user is null) return NotFound();
            return Ok(user);
        }
    }
}