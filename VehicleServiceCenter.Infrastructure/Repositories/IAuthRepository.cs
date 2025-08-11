using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Domain.Entities;

namespace VehicleServiceCenter.Infrastructure.Repositories
{
    public interface IAuthRepository
    {
        Task<User> GetUserByUsernameAsync(string username);
        Task CreateUserAsync(User user);
    }
}
