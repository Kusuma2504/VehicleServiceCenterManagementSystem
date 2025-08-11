using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VehicleServiceCenter.Infrastructure.Security
{
    public interface IPasswordHasher
    {
        bool VerifyPassword(string password, string hashedPassword);
        string HashPassword(string password);

    }
}
