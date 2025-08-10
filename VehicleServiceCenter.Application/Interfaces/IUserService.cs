using System.Collections.Generic;
using System.Threading.Tasks;
using VehicleServiceCenter.Application.DTOs.Users;

namespace VehicleServiceCenter.Application.Interfaces
{
    public interface IUserService
    {
        Task<IEnumerable<UserDto>> GetAllUsersAsync();
        Task<UserDto> GetUserByIdAsync(int id);
        Task<UserDto> CreateUserAsync(CreateUserDto dto);
        Task<UserDto> UpdateUserAsync(int id, UpdateUserDto dto);
        Task<bool> DeleteUserAsync(int id);

        Task AssignRoleAsync(int userId, string roleName);
        Task RemoveRoleAsync(int userId, string roleName);
    }
}
