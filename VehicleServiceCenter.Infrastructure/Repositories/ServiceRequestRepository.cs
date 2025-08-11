using Microsoft.EntityFrameworkCore;
using VehicleServiceCenter.Domain.Entities;
using VehicleServiceCenter.Infrastructure.Data;

namespace VehicleServiceCenter.Infrastructure.Repositories
{
    public class ServiceRequestRepository : IServiceRequestRepository
    {
        private readonly AppDbContext _context;

        public ServiceRequestRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task CreateAsync(ServiceRequest request)
        {
            _context.ServiceRequests.Add(request);
            await _context.SaveChangesAsync();
        }

        public async Task<ServiceRequest> GetByIdAsync(int id)
        {
            return await _context.ServiceRequests
                .Include(sr => sr.Vehicle)
                .Include(sr => sr.Customer)
                .FirstOrDefaultAsync(sr => sr.Id == id);
        }

        public async Task<IEnumerable<ServiceRequest>> GetByVehicleIdAsync(int vehicleId)
        {
            return await _context.ServiceRequests
                .Where(sr => sr.VehicleId == vehicleId)
                .ToListAsync();
        }

        public async Task UpdateAsync(ServiceRequest request)
        {
            _context.ServiceRequests.Update(request);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(ServiceRequest request)
        {
            _context.ServiceRequests.Remove(request);
            await _context.SaveChangesAsync();
        }
    }
}
