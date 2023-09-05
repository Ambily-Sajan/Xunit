using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Xunit_API.Models;
using Xunit_API.Services.Interface;

namespace Xunit_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class modelController : ControllerBase
    {
        private readonly IModelInterface modelInterface;
        public modelController(IModelInterface model)
        {
            this.modelInterface = model;
        }
        [HttpPut]
        public async Task<IActionResult> UpdateModel(int id, Model model)
        {
            try
            {
                if (model != null)
                {


                    var models = await modelInterface.UpdateModel(id, model);
                    if (models != null)
                    {
                        return Ok(models);
                    }
                    else
                    {
                        return NotFound();
                    }
                }
                else
                {
                    return BadRequest();
                }
            }
            catch
            {
                return BadRequest("Exception Caught");
            }
        }
        /*[HttpGet]
        [Route("{brandId:int}")]
        public async Task<IActionResult> GetAllModelByBrand(int? brandId)
        {
            try
            {
                var model = await modelInterface.GetAllModelByBrand(brandId);
                if (model != null)
                {
                    return Ok(model);
                }
                else
                {
                    return null;
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }*/
        [HttpGet]
        public async Task<IActionResult> GetModelsAndBrands()
        {
            try
            {
                var modelsAndBrands = await modelInterface.GetAllModelBrand();
                if (modelsAndBrands != null)
                {
                    return Ok(modelsAndBrands);
                }
                else
                {
                    return null;
                }
            }
            catch
            {
                return BadRequest("Exception Caught");
            }
            
        }
    }
}
