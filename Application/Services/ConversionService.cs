using Application.DTOs;
using Application.DTOs.Conversion;
using Application.Interfaces;
using Domain.Enum;
using System;
using System.Threading.Tasks;

namespace Application.Services
{
    public class ConversionService : IConversionService
    {
        private readonly IUserRepository _userRepository;
        private readonly ICurrencyRepository _currencyRepository;

        public ConversionService(IUserRepository userRepository, ICurrencyRepository currencyRepository)
        {
            _userRepository = userRepository;
            _currencyRepository = currencyRepository;
        }

        public async Task<ConversionResultDto> ConvertAsync(int userId, ConversionRequestDto request)
        {
            var user = await _userRepository.GetByIdAsync(userId);
            if (user == null)
                throw new ArgumentException("User not found.");

            // Validar suscripción
            if (user.Subscription == UserSubscription.Free && user.TotalConversions <= 0)
                throw new InvalidOperationException("Free user has reached conversion limit (10).");

            if (user.Subscription == UserSubscription.Estandar && user.TotalConversions <= 0)
                throw new InvalidOperationException("Trial user has reached conversion limit (100).");

            // Obtener divisas por code
            var sourceCurrency = await _currencyRepository.GetByCodeAsync(request.SourceCurrencyCode);
            var targetCurrency = await _currencyRepository.GetByCodeAsync(request.TargetCurrencyCode);

            if (sourceCurrency == null)
                throw new ArgumentException($"Currency '{request.SourceCurrencyCode}' not found.");

            if (targetCurrency == null)
                throw new ArgumentException($"Currency '{request.TargetCurrencyCode}' not found.");

            // Calcular conversión usando ConvertibilityIndex
            var exchangeRate = sourceCurrency.ConvertibilityIndex / targetCurrency.ConvertibilityIndex;
            var convertedAmount = request.Amount * exchangeRate;

            // Decrementar conversiones disponibles (excepto Pro)
            if (user.Subscription != UserSubscription.Pro)
            {
                user.TotalConversions--;
                _userRepository.Update(user);
                await _userRepository.SaveChangesAsync();
            }

            return new ConversionResultDto
            {
                SourceAmount = request.Amount,
                SourceCurrency = sourceCurrency.Code,
                TargetAmount = convertedAmount,
                TargetCurrency = targetCurrency.Code,
                ExchangeRate = exchangeRate
            };
        }
    }
}