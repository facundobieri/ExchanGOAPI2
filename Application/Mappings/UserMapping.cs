using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Mappings
{
    public static class UserMapping
    {
        public static User ToEntity(this CreateUserRequest request, IPasswordHasher hasher) =>
            new User
            {
                Username = request.Username,
                Email = request.Email,
                Password = hasher.HashPassword(request.Password)
            };

        public static UserDto ToDto(this User user) =>
            new UserDto
            {
                Id = user.Id,
                Username = user.Username,
                Email = user.Email,
                Subscription = user.Subscription.ToString(),
                Role = user.Role.ToString(),
                TotalConversions = user.TotalConversions
            };

        public static void UpdateEntity(this UpdateUserRequest req, User user, IPasswordHasher hasher)
        {
            if (!string.IsNullOrWhiteSpace(req.Email)) user.Email = req.Email;
            if (!string.IsNullOrWhiteSpace(req.Password)) user.Password = hasher.HashPassword(req.Password);
            if (req.Subscription.HasValue) user.Subscription = req.Subscription.Value;
            if (req.TotalConversions.HasValue) user.TotalConversions = req.TotalConversions.Value;
        }
    }
}
