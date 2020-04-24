using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using carRental_WebAPI.Repositories;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize]
    public class CarModelsController : ControllerBase
    {
        private ICarModelsRepository _repo;

        public CarModelsController(ICarModelsRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns all CarModel objects
        /// </summary>
        /// <returns>List of CarModels</returns>
        [HttpGet]
        public async Task<IEnumerable<CarModel>> GetAsync() => await _repo.Get().ToListAsync();

        /// <summary>
        /// Add a CarModel
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /CarModels
        /// {
        ///     "id": 0,
        ///     "name": "New Model"
        /// }
        /// 
        /// </remarks>
        /// <param name="carModel">The CarModel object to be added</param>
        /// <response code="200">Returns the ID of the new CarModel</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostAsync([FromBody] CarModel carModel)
        {
            try
            {
                return Ok(await _repo.AddAsync(carModel));
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a specific CarModel
        /// </summary>
        /// <param name="id">The ID of the CarModel to be deleted</param>
        /// <response code="200">Sucess</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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