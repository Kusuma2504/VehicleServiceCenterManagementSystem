using System.ComponentModel.DataAnnotations;

namespace VehicleServiceCenter.Application.DTOs.ServiceRequest
{
    public class CreateServiceRequestDto
    {
        [Range(1, int.MaxValue, ErrorMessage = "VehicleId must be provided and greater than 0.")]
        public int VehicleId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "CustomerId must be provided and greater than 0.")]
        public int CustomerId { get; set; }

        public int? TechnicianId { get; set; }
      
        public string? Notes { get; set; }
    }
}