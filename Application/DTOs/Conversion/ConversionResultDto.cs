using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class ConversionResultDto
    {
        public decimal SourceAmount { get; set; }
        public string SourceCurrency { get; set; }
        public decimal TargetAmount { get; set; }
        public string TargetCurrency { get; set; }
        public decimal ExchangeRate { get; set; }
    }
}
