using Domain.Enum;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Application.DTOs
{
    public class ChangeSubscriptionRequest
    {
        [Required]
        public UserSubscription NewSubscription { get; set; }
    }
}
