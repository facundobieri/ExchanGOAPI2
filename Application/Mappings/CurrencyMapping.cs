using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Mappings
{
    public static class CurrencyMapping
    {
        public static CurrencyDto ToDto(this Currency currency) => new CurrencyDto
        {
            Id = currency.Id,
            Code = currency.Code,
            Legend = currency.Legend,
            Symbol = currency.Symbol,
            ConvertibilityIndex = currency.ConvertibilityIndex
        };

        public static void UpdateEntity(this UpdateCurrencyRequest req, Currency currency)
        {
            if (!string.IsNullOrWhiteSpace(req.Legend)) currency.Legend = req.Legend;
            if (!string.IsNullOrWhiteSpace(req.Symbol)) currency.Symbol = req.Symbol;
            if (req.ConvertibilityIndex.HasValue) currency.ConvertibilityIndex = req.ConvertibilityIndex.Value;
        }
    }
}
