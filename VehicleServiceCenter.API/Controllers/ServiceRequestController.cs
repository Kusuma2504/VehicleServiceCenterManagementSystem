using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using VehicleServiceCenter.Application.DTOs.ServiceRequest;
using VehicleServiceCenter.Application.Interfaces;

namespace VehicleServiceCenter.Presentation.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize]
    public class ServiceRequestController : ControllerBase
    {
        private readonly IServiceRequestService _serviceRequestService;

        public ServiceRequestController(IServiceRequestService serviceRequestService)
        {
            _serviceRequestService = serviceRequestService;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateServiceRequestDto dto)
        {
            var result = await _serviceRequestService.CreateServiceRequestAsync(dto);
            return Ok(result);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var result = await _serviceRequestService.GetServiceRequestByIdAsync(id);
            if (result == null) return NotFound();
            return Ok(result);
        }

        [HttpGet("vehicle/{vehicleId}")]
        public async Task<IActionResult> GetByVehicle(int vehicleId)
        {
            var result = await _serviceRequestService.GetServiceRequestsByVehicleIdAsync(vehicleId);
            return Ok(result);
        }

        [HttpPut("{id}/status")]
        public async Task<IActionResult> UpdateStatus(int id, [FromBody] UpdateServiceStatusDto dto)
        {
            var updated = await _serviceRequestService.UpdateServiceStatusAsync(id, dto);
            if (updated == null) return NotFound();
            return Ok(updated);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var deleted = await _serviceRequestService.DeleteServiceRequestAsync(id);
            if (!deleted) return NotFound();
            return NoContent();
        }
    }
}
