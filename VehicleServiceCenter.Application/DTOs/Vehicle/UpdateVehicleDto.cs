using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServiceCenter.Application.DTOs.Vehicle
{
    public class UpdateVehicleDto
    {
        public string RegistrationNumber { get; set; }
        public string Make { get; set; }
        public string Model { get; set; }
    }
}
