using Application.DTOs;
using Application.DTOs.Conversion;
using System;
using System.Collections.Generic;
using System.Text;

namespace Application.Interfaces
{
    public interface IConversionService
    {
        Task<ConversionResultDto> ConvertAsync(int userId, ConversionRequestDto request);
    }
}
