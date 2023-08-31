using Xunit_API.Models;

namespace Xunit_API.Services.Interface
{
    public interface IModelInterface
    {
        public Task<Model> UpdateModel(int id, Model model);
     
        //public Task<IEnumerable<Model>> GetAllModelByBrand(int? brandId);
        public Task<dynamic> GetAllModelBrand();


    }
}
