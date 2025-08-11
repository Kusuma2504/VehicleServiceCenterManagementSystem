using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Application.DTOs.Vehicle;
using VehicleServiceCenter.Application.Interfaces;
using VehicleServiceCenter.Domain.Entities;
using VehicleServiceCenter.Infrastructure.Repositories;

namespace VehicleServiceCenter.Application.Services
{
    public class VehicleService : IVehicleService
    {
        private readonly IVehicleRepository _vehicleRepository;

        public VehicleService(IVehicleRepository vehicleRepository)
        {
            _vehicleRepository = vehicleRepository;
        }

        public async Task<VehicleDto> RegisterVehicleAsync(CreateVehicleDto dto)
        {
            var vehicle = new Vehicle
            {
                RegistrationNumber = dto.RegistrationNumber,
                Make = dto.Make,
                Model = dto.Model,
                OwnerId = dto.OwnerId
            };

            await _vehicleRepository.CreateAsync(vehicle);

            return new VehicleDto
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Make = vehicle.Make,
                Model = vehicle.Model,
                OwnerId = vehicle.OwnerId
            };
        }

        public async Task<IEnumerable<VehicleDto>> GetVehiclesByOwnerAsync(int ownerId)
        {
            var vehicles = await _vehicleRepository.GetByOwnerIdAsync(ownerId);
            return vehicles.Select(v => new VehicleDto
            {
                Id = v.Id,
                RegistrationNumber = v.RegistrationNumber,
                Make = v.Make,
                Model = v.Model,
                OwnerId = v.OwnerId
            });
        }

        public async Task<VehicleDto> UpdateVehicleAsync(int id, UpdateVehicleDto dto)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null) return null;

            vehicle.RegistrationNumber = dto.RegistrationNumber;
            vehicle.Make = dto.Make;
            vehicle.Model = dto.Model;

            await _vehicleRepository.UpdateAsync(vehicle);

            return new VehicleDto
            {
                Id = vehicle.Id,
                RegistrationNumber = vehicle.RegistrationNumber,
                Make = vehicle.Make,
                Model = vehicle.Model,
                OwnerId = vehicle.OwnerId
            };
        }

        public async Task<bool> DeleteVehicleAsync(int id)
        {
            var vehicle = await _vehicleRepository.GetByIdAsync(id);
            if (vehicle == null) return false;

            await _vehicleRepository.DeleteAsync(vehicle);
            return true;
        }
    }
}
