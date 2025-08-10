using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceCenter.Application.DTOs.Users;
using VehicleServiceCenter.Application.Interfaces;
using VehicleServiceCenter.Domain.Entities;
using VehicleServiceCenter.Infrastructure.Repositories;
using VehicleServiceCenter.Infrastructure.Security;

namespace VehicleServiceCenter.Application.Services
{
    public class UserService : IUserService
    {
        private readonly UserRepository _userRepo;
        private readonly PasswordHasher _hasher;

        public UserService(UserRepository userRepo, PasswordHasher hasher)
        {
            _userRepo = userRepo;
            _hasher = hasher;
        }

        public async Task<IEnumerable<UserDto>> GetAllUsersAsync()
        {
            var users = await _userRepo.GetAllAsync();
            return users.Select(Map);
        }

        public async Task<UserDto> GetUserByIdAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            return user == null ? null : Map(user);
        }

        public async Task<UserDto> CreateUserAsync(CreateUserDto dto)
        {
            if (await _userRepo.GetByUsernameAsync(dto.Username) != null)
                throw new InvalidOperationException("Username already exists");
            if (await _userRepo.GetByEmailAsync(dto.Email) != null)
                throw new InvalidOperationException("Email already exists");

            var user = new User
            {
                Username = dto.Username,
                Email = dto.Email,
                PasswordHash = _hasher.HashPassword(dto.Password),
                UserType = Enum.Parse<Domain.Enums.UserType>(dto.UserType, true)
            };

            await _userRepo.AddAsync(user);
            await _userRepo.SaveChangesAsync();

            var role = await _userRepo.GetRoleByNameAsync(dto.UserType);
            if (role != null)
                await _userRepo.AssignRoleAsync(user, role);

            var created = await _userRepo.GetByIdAsync(user.Id);
            return Map(created);
        }

        public async Task<UserDto> UpdateUserAsync(int id, UpdateUserDto dto)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return null;

            user.Username = dto.Username ?? user.Username;
            user.Email = dto.Email ?? user.Email;
            if (!string.IsNullOrWhiteSpace(dto.UserType))
                user.UserType = Enum.Parse<Domain.Enums.UserType>(dto.UserType, true);

            await _userRepo.UpdateAsync(user);
            await _userRepo.SaveChangesAsync();

            var updated = await _userRepo.GetByIdAsync(id);
            return Map(updated);
        }

        public async Task<bool> DeleteUserAsync(int id)
        {
            var user = await _userRepo.GetByIdAsync(id);
            if (user == null) return false;

            await _userRepo.DeleteAsync(user);
            await _userRepo.SaveChangesAsync();
            return true;
        }

        public async Task AssignRoleAsync(int userId, string roleName)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found");

            var role = await _userRepo.GetRoleByNameAsync(roleName);
            if (role == null) throw new InvalidOperationException("Role not found");

            await _userRepo.AssignRoleAsync(user, role);
        }

        public async Task RemoveRoleAsync(int userId, string roleName)
        {
            var user = await _userRepo.GetByIdAsync(userId);
            if (user == null) throw new InvalidOperationException("User not found");

            await _userRepo.RemoveRoleAsync(user, roleName);
        }

        private UserDto Map(User u)
        {
            return new UserDto
            {
                Id = u.Id,
                Username = u.Username,
                Email = u.Email,
                UserType = u.UserType.ToString(),
                Roles = u.UserRoles?.Select(ur => ur.Role?.Name).Where(rn => rn != null).ToList()
                        ?? new List<string>()
            };
        }
    }
}
