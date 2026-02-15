using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs.Conversion
{
    public class ConversionRequestDto
    {
        public int SourceCurrencyId { get; set; }
        public int TargetCurrencyId { get; set; }
        public decimal Amount { get; set; }
    }
}
