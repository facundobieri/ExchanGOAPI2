using Application.DTOs;
using Application.Interfaces;
using Domain.Enum;
using System;
using System.Collections.Generic;
using System.Text;

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

        // Obtener divisas
        var sourceCurrency = await _currencyRepository.GetByIdAsync(request.SourceCurrencyId);
        var targetCurrency = await _currencyRepository.GetByIdAsync(request.TargetCurrencyId);

        if (sourceCurrency == null || targetCurrency == null)
            throw new ArgumentException("Invalid currency.");

        // Calcular conversión usando ConvertibilityIndex
        var exchangeRate = targetCurrency.ConvertibilityIndex / sourceCurrency.ConvertibilityIndex;
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
