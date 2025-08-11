using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using VehicleServiceCenter.Domain.Entities;
using VehicleServiceCenter.Infrastructure.Data;

namespace VehicleServiceCenter.Infrastructure.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly AppDbContext _db;
        public UserRepository(AppDbContext db) => _db = db;

        public async Task<IEnumerable<User>> GetAllAsync()
        {
            return await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .ToListAsync();
        }

        public async Task<User> GetByIdAsync(int id)
        {
            return await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<User> GetByUsernameAsync(string username)
        {
            return await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Username == username);
        }

        public async Task<User> GetByEmailAsync(string email)
        {
            return await _db.Users
                .Include(u => u.UserRoles).ThenInclude(ur => ur.Role)
                .FirstOrDefaultAsync(u => u.Email == email);
        }

        public async Task AddAsync(User user)
        {
            await _db.Users.AddAsync(user);
        }

        public async Task UpdateAsync(User user)
        {
            _db.Users.Update(user);
        }

        public async Task DeleteAsync(User user)
        {
            _db.Users.Remove(user);
        }

        public async Task SaveChangesAsync()
        {
            await _db.SaveChangesAsync();
        }

      
        public async Task<Role> GetRoleByNameAsync(string roleName)
        {
            return await _db.Roles.FirstOrDefaultAsync(r => r.Name == roleName);
        }

        public async Task AssignRoleAsync(User user, Role role)
        {
            if (user.UserRoles.Any(ur => ur.RoleId == role.Id)) return;

            _db.UserRoles.Add(new UserRole { UserId = user.Id, RoleId = role.Id });
            await _db.SaveChangesAsync();
        }

        public async Task RemoveRoleAsync(User user, string roleName)
        {
            var ur = user.UserRoles.FirstOrDefault(x => x.Role != null && x.Role.Name == roleName);
            if (ur == null) return;
            _db.UserRoles.Remove(ur);
            await _db.SaveChangesAsync();
        }
    }
}
