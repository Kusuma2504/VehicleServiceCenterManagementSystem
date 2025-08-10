using System.Collections.Generic;

namespace VehicleServiceCenter.Application.DTOs.Users
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string UserType { get; set; }

        public List<string> Roles { get; set; } = new List<string>();
    }
}
