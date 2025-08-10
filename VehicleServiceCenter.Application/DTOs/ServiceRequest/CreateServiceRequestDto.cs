namespace VehicleServiceCenter.Application.DTOs.ServiceRequest
{
    public class CreateServiceRequestDto
    {
        public int VehicleId { get; set; }
        public int CustomerId { get; set; }
        public int? TechnicianId { get; set; }
        public string? Notes { get; set; }
    }
}