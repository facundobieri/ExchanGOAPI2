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
            Password = HashPassword(request.Password)
        };
        public static UserDto ToDto(this User user) => new UserDto
        {
            Id = user.Id,
            Username = user.Username,
            Email = user.Email,
            Subscription = user.Subscription.ToString(),
            Role = user.Role.ToString(),
            TotalConversions = user.TotalConversions,
            PasswordHash = user.Password
        };
        public static void UpdateEntity(this UpdateUserRequest req, User user)
        {
            if (!string.IsNullOrWhiteSpace(req.Email)) user.Email = req.Email;
            if (!string.IsNullOrWhiteSpace(req.Password)) user.Password = HashPassword(req.Password);
            if (req.Subscription.HasValue) user.Subscription = req.Subscription.Value;
            if (req.TotalConversions.HasValue) user.TotalConversions = req.TotalConversions.Value;
        }
        private static string HashPassword(string password)
        {
            using (var sha256 = SHA256.Create())
            {
                var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(bytes);
            }
        }
    }
}
