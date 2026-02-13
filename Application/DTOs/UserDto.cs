

namespace Application.DTOs
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Subscription { get; set; }
        public string Role { get; set; }
        public int TotalConversions { get; set; }
        public string PasswordHash { get; set; }
    }
}
