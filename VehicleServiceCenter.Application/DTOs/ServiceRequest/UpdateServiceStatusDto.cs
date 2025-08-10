using VehicleServiceCenter.Domain.Enums;

namespace VehicleServiceCenter.Application.DTOs.ServiceRequest
{
    public class UpdateServiceStatusDto
    {
        public string Status { get; set; }
        public decimal? Cost { get; set; }
        public string? Notes { get; set; }
    }
}
