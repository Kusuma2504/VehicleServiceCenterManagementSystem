using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Domain.Entities;

namespace VehicleServiceCenter.Infrastructure.Repositories
{
    public interface IVehicleRepository
    {
        Task CreateAsync(Vehicle vehicle);
        Task<Vehicle> GetByIdAsync(int id);
        Task<IEnumerable<Vehicle>> GetByOwnerIdAsync(int ownerId);
        Task UpdateAsync(Vehicle vehicle);
        Task DeleteAsync(Vehicle vehicle);


    }
}
