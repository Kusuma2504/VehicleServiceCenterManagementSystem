using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Application.DTOs.Vehicle;

namespace VehicleServiceCenter.Application.Interfaces
{
    public interface IVehicleService
    {
        Task<VehicleDto> RegisterVehicleAsync(CreateVehicleDto dto);
        Task<IEnumerable<VehicleDto>> GetVehiclesByOwnerAsync(int ownerId);
        Task<VehicleDto> UpdateVehicleAsync(int id, UpdateVehicleDto dto);
        Task<bool> DeleteVehicleAsync(int id);

    }
}
