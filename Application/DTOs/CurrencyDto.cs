using System;
using System.Collections.Generic;
using System.Text;

namespace Application.DTOs
{
    public class CurrencyDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string Legend { get; set; }
        public string Symbol { get; set; }
        public decimal ConvertibilityIndex { get; set; }
    }
}
