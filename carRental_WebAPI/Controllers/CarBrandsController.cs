using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using carRental_WebAPI.Repositories;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class CarBrandsController : ControllerBase
    {
        private ICarBrandsRepository _repo;

        public CarBrandsController(ICarBrandsRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<CarBrand>> GetAsync() => await _repo.Get().ToListAsync();

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CarBrand carBrand)
        {
            try
            {
                return Ok(await _repo.AddAsync(carBrand));
            }
            catch (Exception ex)
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
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}