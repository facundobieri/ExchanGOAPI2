using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs.Conversion
{
    public class ConversionRequestDto
    {
        [Required]
        public string SourceCurrencyCode { get; set; }
        [Required]
        public string TargetCurrencyCode { get; set; }
        [Required]
        [Range(0.01, double.MaxValue, ErrorMessage = "Amount must be greater than 0.")]
        public decimal Amount { get; set; }
    }
}
