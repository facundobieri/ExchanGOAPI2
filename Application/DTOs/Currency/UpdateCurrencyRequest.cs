using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class UpdateCurrencyRequest
    {
        public string? Legend { get; set; }
        public string? Symbol { get; set; }
        public decimal? ConvertibilityIndex { get; set; }
    }
}
