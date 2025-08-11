using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServiceCenter.Application.DTOs.Auth
{
    public class AuthResponseDto
    {
        public bool Success { get; set; }
        public string Token { get; set; }
    }
}

