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
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CarTypesController : ControllerBase
    {
        private ICarTypesRepository _repo;

        public CarTypesController(ICarTypesRepository repo)
        {
            _repo = repo;
        }

        [HttpGet]
        public async Task<IEnumerable<CarType>> GetAsync() => await _repo.Get().ToListAsync();

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromBody] CarType carType)
        {
            try
            {
                return Ok(await _repo.AddAsync(carType));
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