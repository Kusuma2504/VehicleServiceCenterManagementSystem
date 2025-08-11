using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServiceCenter.Application.DTOs.Vehicle
{
    public class CreateVehicleDto
    {
        [Required(ErrorMessage = "Registration number is required.")]
        public string RegistrationNumber { get; set; }

        [Required(ErrorMessage = "Make is required.")]

        public string Make { get; set; }

        [Required(ErrorMessage = "Model is required.")]

        public string Model { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "OwnerId must be provided and greater than 0.")]

        public int OwnerId { get; set; }
    }
}
