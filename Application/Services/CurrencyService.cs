using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Domain.Entities;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class CurrencyService : ICurrencyService
    {
        private readonly ICurrencyRepository _currencyRepository;
        private readonly IUserRepository _userRepository;

        public CurrencyService(ICurrencyRepository currencyRepository, IUserRepository userRepository)
        {
            _currencyRepository = currencyRepository;
            _userRepository = userRepository;
        }

        public async Task<CurrencyDto> CreateCurrencyAsync(CreateCurrencyRequest request, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Only admins can create currencies.");

            var currency = new Currency
            {
                Code = request.Code,
                Legend = request.Legend,
                Symbol = request.Symbol,
                ConvertibilityIndex = request.ConvertibilityIndex
            };

            await _currencyRepository.AddAsync(currency);
            await _currencyRepository.SaveChangesAsync();
            return currency.ToDto();
        }

        public async Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync()
        {
            var currencies = await _currencyRepository.GetAllAsync();
            return currencies.Select(c => c.ToDto());
        }

        public async Task<CurrencyDto?> GetCurrencyByIdAsync(int id)
        {
            var currency = await _currencyRepository.GetByIdAsync(id);
            return currency?.ToDto();
        }

        public async Task<CurrencyDto?> UpdateCurrencyAsync(int id, UpdateCurrencyRequest request, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Only admins can update currencies.");

            var currency = await _currencyRepository.GetByIdAsync(id);
            if (currency == null) return null;

            request.UpdateEntity(currency);
            _currencyRepository.Update(currency);
            await _currencyRepository.SaveChangesAsync();
            return currency.ToDto();
        }

        public async Task<bool> DeleteCurrencyAsync(int id, int userId)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null || user.Role != UserRole.Admin)
                throw new UnauthorizedAccessException("Only admins can delete currencies.");

            var currency = await _currencyRepository.GetByIdAsync(id);
            if (currency == null) return false;

            _currencyRepository.Delete(currency);
            await _currencyRepository.SaveChangesAsync();
            return true;
        }
    }
}
