using Application.DTOs;
using Application.Interfaces;
using Application.Mappings;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        public UserService(IUserRepository userRepository) => _userRepository = userRepository;

        public async Task<UserDto> CreateUserAsync(CreateUserRequest request)
        {
            var user = request.ToEntity();
            await _userRepository.AddAsync(user);
            await _userRepository.SaveChangesAsync();
            return user.ToDto();

        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return user?.ToDto();
        }
        public async Task<UserDto?> UpdateUserAsync(int id, UpdateUserRequest request)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null) return null;

            request.UpdateEntity(user);
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
    }
}
