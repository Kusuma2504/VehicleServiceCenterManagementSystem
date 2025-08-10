using VehicleServiceCenter.Application.DTOs.ServiceRequest;
using VehicleServiceCenter.Application.Interfaces;
using VehicleServiceCenter.Domain.Entities;
using VehicleServiceCenter.Domain.Enums;
using VehicleServiceCenter.Infrastructure.Repositories;

namespace VehicleServiceCenter.Application.Services
{
    public class ServiceRequestService : IServiceRequestService
    {
        private readonly ServiceRequestRepository _repository;

        public ServiceRequestService(ServiceRequestRepository repository)
        {
            _repository = repository;
        }

        public async Task<ServiceRequestDto> CreateServiceRequestAsync(CreateServiceRequestDto dto)
        {
            var request = new ServiceRequest
            {
                VehicleId = dto.VehicleId,
                CustomerId = dto.CustomerId,
                TechnicianId = dto.TechnicianId,
                DateRequested = DateTime.UtcNow,
                Status = ServiceStatus.Received,
                Notes = dto.Notes
            };

            await _repository.CreateAsync(request);

            return MapToDto(request);
        }

        public async Task<ServiceRequestDto> GetServiceRequestByIdAsync(int id)
        {
            var request = await _repository.GetByIdAsync(id);
            return request == null ? null : MapToDto(request);
        }

        public async Task<IEnumerable<ServiceRequestDto>> GetServiceRequestsByVehicleIdAsync(int vehicleId)
        {
            var requests = await _repository.GetByVehicleIdAsync(vehicleId);
            return requests.Select(MapToDto);
        }

        public async Task<ServiceRequestDto> UpdateServiceStatusAsync(int id, UpdateServiceStatusDto dto)
        {
            var request = await _repository.GetByIdAsync(id);
            if (request == null) return null;

            request.Status = Enum.Parse<ServiceStatus>(dto.Status, true);
            request.Cost = dto.Cost;
            request.Notes = dto.Notes;

            await _repository.UpdateAsync(request);
            return MapToDto(request);
        }

        public async Task<bool> DeleteServiceRequestAsync(int id)
        {
            var request = await _repository.GetByIdAsync(id);
            if (request == null) return false;

            await _repository.DeleteAsync(request);
            return true;
        }

        private ServiceRequestDto MapToDto(ServiceRequest r) =>
            new ServiceRequestDto
            {
                Id = r.Id,
                VehicleId = r.VehicleId,
                CustomerId = r.CustomerId,
                TechnicianId = r.TechnicianId,
                DateRequested = r.DateRequested,
                Status = r.Status.ToString(),
                Notes = r.Notes,
                Cost = r.Cost
            };
    }
}
