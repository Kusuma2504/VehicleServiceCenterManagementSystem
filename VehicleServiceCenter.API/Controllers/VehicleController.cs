using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using VehicleServiceCenter.Application.DTOs.Vehicle;
using VehicleServiceCenter.Application.Interfaces;

namespace VehicleServiceCenter.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class VehicleController : ControllerBase
    {
        private readonly IVehicleService _vehicleService;

        public VehicleController(IVehicleService vehicleService)
        {
            _vehicleService = vehicleService;
        }

        [HttpPost]
        public async Task<IActionResult> RegisterVehicle([FromBody] CreateVehicleDto dto)
        {
            var vehicle = await _vehicleService.RegisterVehicleAsync(dto);
            return Ok(vehicle);
        }

        [HttpGet("owner/{ownerId}")]
        public async Task<IActionResult> GetVehiclesByOwner(int ownerId)
        {
            var vehicles = await _vehicleService.GetVehiclesByOwnerAsync(ownerId);
            return Ok(vehicles);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateVehicle(int id, [FromBody] UpdateVehicleDto dto)
        {
            var updated = await _vehicleService.UpdateVehicleAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteVehicle(int id)
        {
            var deleted = await _vehicleService.DeleteVehicleAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }

}
