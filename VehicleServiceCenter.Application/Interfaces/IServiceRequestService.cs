using VehicleServiceCenter.Application.DTOs.ServiceRequest;


namespace VehicleServiceCenter.Application.Interfaces
{
    public interface IServiceRequestService
    {
        Task<ServiceRequestDto> CreateServiceRequestAsync(CreateServiceRequestDto dto);
        Task<ServiceRequestDto> GetServiceRequestByIdAsync(int id);
        Task<IEnumerable<ServiceRequestDto>> GetServiceRequestsByVehicleIdAsync(int vehicleId);
        Task<ServiceRequestDto> UpdateServiceStatusAsync(int id, UpdateServiceStatusDto dto);
        Task<bool> DeleteServiceRequestAsync(int id);
    }
}
