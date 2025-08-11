using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Domain.Entities;

namespace VehicleServiceCenter.Infrastructure.Repositories
{
    public interface IServiceRequestRepository
    {
        Task<IEnumerable<ServiceRequest>> GetByVehicleIdAsync(int vehicleId);
        Task CreateAsync(ServiceRequest request);

        Task<ServiceRequest> GetByIdAsync(int id);

        Task UpdateAsync(ServiceRequest request);
        Task DeleteAsync(ServiceRequest request);
    }
}
