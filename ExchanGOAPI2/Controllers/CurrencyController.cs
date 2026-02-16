using Application.DTOs;
using Application.DTOs.Currency;
using Application.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ExchanGOAPI2.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class CurrencyController : ControllerBase
    {
        private readonly ICurrencyService _currencyService;

        public CurrencyController(ICurrencyService currencyService) =>
            _currencyService = currencyService;

        [HttpGet]
        [AllowAnonymous]
        public async Task<ActionResult<IEnumerable<CurrencyDto>>> GetAll()
        {
            var currencies = await _currencyService.GetAllCurrenciesAsync();
            return Ok(currencies);
        }

        [HttpGet("{id}")]
        [AllowAnonymous]
        public async Task<ActionResult<CurrencyDto>> GetById(int id)
        {
            var currency = await _currencyService.GetCurrencyByIdAsync(id);
            if (currency is null) return NotFound();
            return Ok(currency);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CurrencyDto>> Create(CreateCurrencyRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            try
            {
                var currency = await _currencyService.CreateCurrencyAsync(request, userId);
                return CreatedAtAction(nameof(GetById), new { id = currency.Id }, currency);
            }
            catch (UnauthorizedAccessException ex)
            {
                return Forbid();
            }
        }

        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult<CurrencyDto>> Update(int id, UpdateCurrencyRequest request)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            try
            {
                var currency = await _currencyService.UpdateCurrencyAsync(id, request, userId);
                if (currency is null) return NotFound();
                return Ok(currency);
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }

        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<ActionResult> Delete(int id)
        {
            var userId = int.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
            try
            {
                var result = await _currencyService.DeleteCurrencyAsync(id, userId);
                if (!result) return NotFound();
                return NoContent();
            }
            catch (UnauthorizedAccessException)
            {
                return Forbid();
            }
        }
    }
}