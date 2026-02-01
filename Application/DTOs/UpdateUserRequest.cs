using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.DTOs
{
    public class UpdateUserRequest
    {
        [EmailAddress]
        public string? Email { get; set; }
        [MinLength(8)]
        public string? Password { get; set; }
        
        public UserSubscription? Subscription { get; set; }
        public int? TotalConversions { get; set; }
    }
}
