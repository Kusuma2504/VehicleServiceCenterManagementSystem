using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VehicleServiceCenter.Domain.Entities;

namespace VehicleServiceCenter.Infrastructure.Security
{
    public interface IJwtTokenGenerator 
    {
        string GenerateToken(User user);
    }
}
