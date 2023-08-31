using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Xunit_API.Services.Interface;

namespace Xunit_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class brandController : ControllerBase
    {
        private readonly IBrand brandInterface;
        public brandController(IBrand brand)
        {
            this.brandInterface = brand;
        }

        [HttpGet]
        public async Task<IActionResult> GetAllBrandsOfAVehicleType(int id)
        {
            try
            {
                var brand = await brandInterface.GetAllBrandsOfAVehicleType(id);
                if (brand != null)
                {
                    return Ok(brand);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
        /*[HttpDelete]
        public bool DeleteBrand(int id) { 
            return brandInterface.DeleteBrand(id);
        }*/

       [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteBrand(int id)
        {
            try
            {
                var brandid = await brandInterface.DeleteBrand(id);
                if(brandid != null)
                {
                    return Ok(brandid);
                }
                else
                {
                    return null;
                }
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
