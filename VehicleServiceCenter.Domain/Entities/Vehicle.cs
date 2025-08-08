using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;


namespace VehicleServiceCenter.Domain.Entities
{
    public class Vehicle
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public string RegistrationNumber { get; set; }

        public string Make { get; set; }

        public string Model { get; set; }

        public int OwnerId { get; set; }

        public User Owner { get; set; }

        public virtual ICollection<ServiceRequest> ServiceRequests { get; set; }
    }
}
