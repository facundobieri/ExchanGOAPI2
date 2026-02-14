using Application.DTOs;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Application.Mappings
{
    public static class UserMapping
    {
        public static User ToEntity(this CreateUserRequest request) => new User
        {
            Username = request.Username,
            Email = request.Email,
            Password = request.Password
        };
        public static UserDto ToDto(this User user) => new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Subscription = user.Subscription.ToString(),
            Role = user.Role.ToString(),
            TotalConversions = user.TotalConversions
        };
        public static void UpdateEntity(this UpdateUserRequest req, User user)
        {
            if (!string.IsNullOrWhiteSpace(req.Email)) user.Email = req.Email;
            if (!string.IsNullOrWhiteSpace(req.Password)) user.Password = req.Password;
            if (req.Subscription.HasValue) user.Subscription = req.Subscription.Value;
            if (req.TotalConversions.HasValue) user.TotalConversions = req.TotalConversions.Value;
        }
    }
}
