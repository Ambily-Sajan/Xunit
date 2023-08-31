using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit_API.Data;
using Xunit_API.Models;
using Xunit_API.Services.Interface;

namespace Xunit_API.Services
{
    public class brandRepository:IBrand
    {
        private readonly VehicleDbContext vehicleDbContext;
        public brandRepository(VehicleDbContext vehicleDbContext)
        {
            this.vehicleDbContext = vehicleDbContext;
        }

        /*public bool DeleteBrand(int id)
        {
            var item=vehicleDbContext.vehicle_brands.Find(id);
            vehicleDbContext.vehicle_brands.Remove(item);
            return vehicleDbContext.SaveChanges() > 0 ? true : false;
        }*/

        //Get All Brands of VehicleType
        public async Task<IEnumerable<brand>> GetAllBrandsOfAVehicleType(int id)
        {
            var brand=await vehicleDbContext.vehicle_brands.Where(brand => brand.VehicleTypeId == id).ToListAsync();
            if(brand != null)
            {
                return brand;
            }
            else
            {
                return null;
            }
        }
        
        //Delete Brand
        public async Task<brand> DeleteBrand(int id)
        {
            //var brand = await vehicleDbContext.vehicle_brands.FirstOrDefaultAsync(options => options.BrandId == id);
            var brandid =vehicleDbContext.vehicle_brands.Find(id);
            if (brandid!=null)
            {
               
                vehicleDbContext.vehicle_brands.Remove(brandid);
                vehicleDbContext.SaveChanges();
                return (brandid);
               // return brand;

            }
            else
            {
                return null;
            }
        }

    }
}
