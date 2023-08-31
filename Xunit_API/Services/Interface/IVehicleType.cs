using Xunit_API.Models;

namespace Xunit_API.Services.Interface
{
    public interface IVehicleType
    {
        public Task<VehicleType> AddVehicleType(VehicleType vehicleType);
        public Task<IEnumerable<VehicleType>> GetAllVehicleType();
    }
}
