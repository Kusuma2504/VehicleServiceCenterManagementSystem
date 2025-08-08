using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleServiceCenter.Domain.Enums;

namespace VehicleServiceCenter.Domain.Entities
{
    public class ServiceRequest
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public int VehicleId { get; set; }

        public int CustomerId { get; set; }

        public int? TechnicianId { get; set; }

        public DateTime DateRequested { get; set; }

        public ServiceStatus Status { get; set; }

        public string? Notes { get; set; }

        public decimal? Cost { get; set; }

        public Vehicle Vehicle { get; set; }

        public User Customer { get; set; }

        public User? Technician { get; set; }
    }
}