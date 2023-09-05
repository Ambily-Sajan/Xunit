using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Xunit_API.Data;
using Xunit_API.Models;
using Xunit_API.Services.Interface;

namespace Xunit_API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class vehicleTypeController : ControllerBase
    {
        private readonly IVehicleType vehicleTypeInterface;
        public vehicleTypeController(IVehicleType vehicleTypeInterface)
        {
            this.vehicleTypeInterface = vehicleTypeInterface;
        }

        [HttpPost]
        public async Task<IActionResult> AddVehicleType([FromBody] VehicleType vehicleType)
        {
            try
            {
                if (vehicleType != null)
                {

                    var vehicle = await vehicleTypeInterface.AddVehicleType(vehicleType);
                    if (vehicle != null)
                    {
                        return Ok(vehicle);
                    }
                    else
                    {
                        //return NotFound();
                        return null;
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
        [HttpGet]
        public async Task<IActionResult> GetAllVehicleType()
        {
            try
            {
                var vehicles = await vehicleTypeInterface.GetAllVehicleType();
                if(vehicles !=null)
                {
                    return Ok(vehicles);
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
