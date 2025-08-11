using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Domain.Entities;

namespace VehicleServiceCenter.Infrastructure.Repositories
{
    public interface IUserRepository
    {
        Task<IEnumerable<User>> GetAllAsync();
        Task<User> GetByIdAsync(int id);
        Task<User> GetByUsernameAsync(string username);
        Task<User> GetByEmailAsync(string email);
        Task AddAsync(User user);
        Task UpdateAsync(User user);
        Task DeleteAsync(User user);
        Task SaveChangesAsync();
        Task<Role> GetRoleByNameAsync(string roleName);
        Task AssignRoleAsync(User user, Role role);
        Task RemoveRoleAsync(User user, string roleName);

    }
}
