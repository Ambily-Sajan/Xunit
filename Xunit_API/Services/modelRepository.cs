using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit_API.Data;
using Xunit_API.Models;
using Xunit_API.Services.Interface;

namespace Xunit_API.Services
{
    public class modelRepository: IModelInterface
    {
        private readonly VehicleDbContext vehicleDbContext;
        public modelRepository(VehicleDbContext _vehicleDbContext)
        {
            this.vehicleDbContext = _vehicleDbContext;
        }

        //Edit Model
        public async Task<Model> UpdateModel(int id, Model model)
        {
            var models = await vehicleDbContext.vehicle_model.SingleOrDefaultAsync(option => option.ModelId == id);
            if (models != null) {

                models.ModelId = model.ModelId;
                models.BrandId = model.BrandId;
                models.modelname = model.modelname;
                models.Description = model.Description;
                models.SortOrder= model.SortOrder;
                models.IsActive = model.IsActive;

                await vehicleDbContext.SaveChangesAsync();
                return models;
            }
            else
            {
                return null;
            }

        }

        /*public async Task<IEnumerable<Model>> GetAllModelByBrand(int? brandId)
        {
            var exist = vehicleDbContext.vehicle_brands.Any(b => b.BrandId == brandId);
            if (exist != null)
            {
                var model = await vehicleDbContext.vehicle_model.Where(m => m.BrandId == brandId).ToListAsync();
                return model;
            }
            else
            {
                return null;
            }
           
        }*/

        //Get All Model with Brand Name
        public async Task<dynamic> GetAllModelBrand()
        {
            var modelsWithBrands = await vehicleDbContext.vehicle_model
               .Join(
                   vehicleDbContext.vehicle_brands,
                   model => model.BrandId,
                   brand => brand.BrandId,
                   (model, brand) => new 
                   {
                       ModelId = model.ModelId,
                       BrandId = brand.BrandId,
                       ModelName = model.modelname,
                       Description = model.Description,
                       SortOrder = model.SortOrder,
                       IsActive = model.IsActive,
                       VehicleBrandName = brand.Brand,
                       
                   }
               )
               .ToListAsync();

            return modelsWithBrands;
        }

    }
}
