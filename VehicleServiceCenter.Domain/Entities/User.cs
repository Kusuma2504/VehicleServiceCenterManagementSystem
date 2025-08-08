using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using VehicleServiceCenter.Domain.Enums;

namespace VehicleServiceCenter.Domain.Entities
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string Username { get; set; }

        public string Email { get; set; }

        public string PasswordHash { get; set; }

        public UserType UserType { get; set; }

        public ICollection<UserRole> UserRoles { get; set; }

        public ICollection<Vehicle> Vehicles { get; set; }

        public ICollection<ServiceRequest> ServiceRequestsAsCustomer { get; set; }

    }
}
