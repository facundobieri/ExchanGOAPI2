using Application.DTOs;
using Application.DTOs.Conversion;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExchanGOAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ConversionController : ControllerBase
    {
        private readonly IConversionService _conversionService;

        public ConversionController(IConversionService conversionService) =>
            _conversionService = conversionService;

        [HttpPost]
        public async Task<ActionResult<ConversionResultDto>> Convert(ConversionRequestDto request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);

            try
            {
                var result = await _conversionService.ConvertAsync(userId, request);
                return Ok(result);
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (ArgumentException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
