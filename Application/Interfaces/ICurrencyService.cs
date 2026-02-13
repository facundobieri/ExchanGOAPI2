using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface ICurrencyService
    {
        Task<CurrencyDto> CreateCurrencyAsync(CreateCurrencyRequest request, int userId);
        Task<IEnumerable<CurrencyDto>> GetAllCurrenciesAsync();
        Task<CurrencyDto?> GetCurrencyByIdAsync(int id);
        Task<CurrencyDto?> UpdateCurrencyAsync(int id, UpdateCurrencyRequest request, int userId);
        Task<bool> DeleteCurrencyAsync(int id, int userId);
    }
}
