using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using carRental_WebAPI.Repositories;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CarsController : ControllerBase
    {
        private ICarsRepository _repo;

        public CarsController(ICarsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<JsonResult> GetAsync()
        {
            var data = await (from c in _repo.Get()
                        select new
                        {
                            c.Id,
                            c.CarTypeId,
                            Type = c.CarType.Name,
                            c.CarBrandId,
                            Brand = c.CarBrand.Name,
                            c.CarModelId,
                            Model = c.CarModel.Name
                        }).ToListAsync();

            return new JsonResult(data);
        }

        [HttpGet("Search")]
        public async Task<JsonResult> SearchAsync([FromQuery] string type, string brand, string model)
        {
            string carType = type.ToLower();
            string carBrand = brand.ToLower();
            string carModel = model.ToLower();

            var data = await (from c in _repo.Get()
                              where ((carType == "" || c.CarType.Name.ToLower() == carType)
                                  && (carBrand == "" || c.CarBrand.Name.ToLower() == carBrand)
                                  && (carModel == "" || c.CarModel.Name.ToLower() == carModel))
                              select new
                              {
                                  c.Id,
                                  c.CarTypeId,
                                  Type = c.CarType.Name,
                                  c.CarBrandId,
                                  Brand = c.CarBrand.Name,
                                  c.CarModelId,
                                  Model = c.CarModel.Name
                              }).ToListAsync();

            return new JsonResult(data);
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] Car car)
        {
            try
            {
                return Ok(await _repo.AddAsync(car));
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(int id)
        {
            try
            {
                await _repo.RemoveAsync(id);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}