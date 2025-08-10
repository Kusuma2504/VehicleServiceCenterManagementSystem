using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServiceCenter.Application.DTOs.Users
{ 
    public class AssignRoleDto
    {
        public int UserId { get; set; }
        public string RoleName { get; set; }
    }
}
