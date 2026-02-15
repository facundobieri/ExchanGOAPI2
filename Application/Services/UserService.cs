using Application.DTOs;
using Application.DTOs.User;
using Application.Interfaces;
using Application.Mappings;
using Domain.Entities;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IPasswordHasher _passwordHasher;
        private readonly IJwtTokenService _jwtTokenService;

        public UserService(
            IUserRepository userRepository,
            IPasswordHasher passwordHasher,
            IJwtTokenService jwtTokenService)
        {
            _userRepository = userRepository;
            _passwordHasher = passwordHasher;
            _jwtTokenService = jwtTokenService;
        }

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var existingUsername = await _userRepository.GetByUsernameAsync(request.Username);
            if (existingUsername != null)
                throw new InvalidOperationException("Username already exists.");

            var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
            if (existingEmail != null)
                throw new InvalidOperationException("Email already exists.");

            var user = request.ToEntity(_passwordHasher);
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user.ToDto();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user?.ToDto();
        }

        public async Task<UserDto?> GetUserByUsernameAsync(string username)
        {
            var user = await _userRepository.GetByUsernameAsync(username);
            return user?.ToDto();
        }

        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            if (!string.IsNullOrWhiteSpace(request.Email) && request.Email != user.Email)
            {
                var existingEmail = await _userRepository.GetByEmailAsync(request.Email);
                if (existingEmail != null)
                    throw new InvalidOperationException("Email already exists.");
            }

            request.UpdateEntity(user, _passwordHasher);
            _userRepository.Update(user);
            await _userRepository.SaveChangesAsync();
            return user.ToDto();
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return false;

            _userRepository.Delete(user);
            await _userRepository.SaveChangesAsync();
            return true;
        }

        public async Task<LoginResponse?> LoginAsync(LoginRequest request)
        {
            var user = await _userRepository.GetByUsernameAsync(request.Username);
            if (user == null)
                return null;

            if (!_passwordHasher.VerifyPassword(request.Password, user.Password))
                return null;

            var userDto = user.ToDto();
            var token = _jwtTokenService.GenerateToken(userDto);

            return new LoginResponse
            {
                User = userDto,
                Token = token
            };
        }
    }
}