using Microsoft.AspNetCore.Mvc;
using Xunit_API.Models;

namespace Xunit_API.Services.Interface
{
    public interface IBrand
    {
        public Task<IEnumerable<brand>> GetAllBrandsOfAVehicleType(int id);
        public Task<brand> DeleteBrand(int id);
    }
}
