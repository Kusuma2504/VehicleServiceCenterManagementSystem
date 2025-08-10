using VehicleServiceCenter.Domain.Enums;

namespace VehicleServiceCenter.Application.DTOs.ServiceRequest
{
    public class ServiceRequestDto
    {
        public int Id { get; set; }
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public int? TechnicianId { get; set; }
        public DateTime DateRequested { get; set; }
        public string Status { get; set; }
        public string? Notes { get; set; }
        public decimal? Cost { get; set; }
    }
}
