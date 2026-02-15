using Application.Interfaces;
using BCrypt.Net;
namespace Infrastructure.Security
{
    public class BcryptPasswordHasher : IPasswordHasher
    {
        public string HashPassword(string password) =>
            BCrypt.Net.BCrypt.HashPassword(password);

        public bool VerifyPassword(string inputPassword, string hashedPassword) =>
            BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
    }
}
