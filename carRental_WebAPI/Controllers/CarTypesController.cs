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
    public class CarTypesController : ControllerBase
    {
        private ICarTypesRepository _repo;

        public CarTypesController(ICarTypesRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns all CarType objects
        /// </summary>
        /// <returns>List of CarTypes</returns>
        [HttpGet]
        public async Task<IEnumerable<CarType>> GetAsync() => await _repo.Get().ToListAsync();

        /// <summary>
        /// Add a CarType
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /CarTypes
        /// {
        ///     "id": 0,
        ///     "name": "New Type"
        /// }
        /// 
        /// </remarks>
        /// <param name="carType">The CarType object to be added</param>
        /// <response code="200">Returns the ID of the new CarType</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Deletes a specific CarType
        /// </summary>
        /// <param name="id">The ID of the CarType to be deleted</param>
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