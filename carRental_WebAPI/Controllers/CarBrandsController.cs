﻿using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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

        /// <summary>
        /// Returns all CarBrand objects
        /// </summary>
        /// <returns>List of CarBrands</returns>
        [HttpGet]
        public async Task<IEnumerable<CarBrand>> GetAsync() => await _repo.Get().ToListAsync();

        /// <summary>
        /// Add a CarBrand
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /CarBrands
        /// {
        ///     "id": 0,
        ///     "name": "New Brand"
        /// }
        /// 
        /// </remarks>
        /// <param name="carBrand">The CarBrand object to be added</param>
        /// <response code="200">Returns the ID of the new CarBrand</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Deletes a specific CarBrand
        /// </summary>
        /// <param name="id">The ID of the CarBrand to be deleted</param>
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