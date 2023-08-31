using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit_API.Data;
using Xunit_API.Models;
using Xunit_API.Services.Interface;

namespace Xunit_API.Services
{
    public class vehicleTypeRepository : IVehicleType
    {
    private readonly VehicleDbContext vehicleDbContext;
        public vehicleTypeRepository(VehicleDbContext _dbContext)
        {
            this.vehicleDbContext = _dbContext;
        }
        //Add VehicleType
        public async Task<VehicleType> AddVehicleType([FromBody] VehicleType vehicleType)
        {
            await vehicleDbContext.vehicle_type.AddAsync(vehicleType);
            await vehicleDbContext.SaveChangesAsync();
            return vehicleType;
        }
        //GetAllVehicle
        public async Task<IEnumerable<VehicleType>> GetAllVehicleType()
        {
            var vehicle=await vehicleDbContext.vehicle_type.ToListAsync();
            if(vehicle != null)
            {
                return vehicle;
            }
            else
            {
                return null;
            }
        }
    }
}
