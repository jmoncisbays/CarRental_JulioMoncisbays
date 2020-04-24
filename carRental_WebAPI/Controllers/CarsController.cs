using System;
using System.Linq;
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
    public class CarsController : ControllerBase
    {
        private ICarsRepository _repo;
        private IRentalTransactionsRepository _repoRentalTransactions;

        public CarsController(ICarsRepository repo, IRentalTransactionsRepository repoRentalTransactions)
        {
            _repo = repo;
            _repoRentalTransactions = repoRentalTransactions;
        }

        /// <summary>
        /// Returns all Car objects
        /// </summary>
        /// <returns>List of Cars</returns>
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
                            Model = c.CarModel.Name,
                            c.IsAvailable,
                            c.CurrentRentalTransactionId
                        }).ToListAsync();

            return new JsonResult(data);
        }

        /// <summary>
        /// Search for Car objects by type and brand and model
        /// </summary>
        /// <remarks>
        /// Sample Requests
        /// 
        /// GET /cars/search?brand=kia&amp;model=2019
        /// 
        /// GET /cars/search?brand=vw&amp;type=medium&amp;model=2019
        /// 
        /// </remarks>
        /// <returns>List of Cars</returns>
        [HttpGet("Search")]
        public async Task<JsonResult> SearchAsync([FromQuery] string type, string brand, string model)
        {
            string carType = type?.ToLower() ?? "";
            string carBrand = brand?.ToLower() ?? "";
            string carModel = model?.ToLower() ?? "";

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
                                  Model = c.CarModel.Name,
                                  c.IsAvailable,
                                  c.CurrentRentalTransactionId
                              }).ToListAsync();

            return new JsonResult(data);
        }

        /// <summary>
        /// Add a Car
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /Cars
        /// {
        ///     "id": 0,
        ///     "carTypeId": 2,
        ///     "carBrandId": 2,
        ///     "carModelId": 1,
        ///     "isAvailable": true
        /// }
        /// 
        /// </remarks>
        /// <param name="car">The Car object to be added</param>
        /// <response code="200">Returns the ID of the new Car</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
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

        /// <summary>
        /// Updates a Car as rented and adds a record to its rental history
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /Cars/Rent/84
        /// 
        /// </remarks>
        /// <param name="carId">The ID of the Car to be rented</param>
        /// <response code="200">Success</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpPost]
        [Route("Rent/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostRentAsync(int carId)
        {
            try
            {
                await _repoRentalTransactions.RentCarAsync(carId);
                return Ok();
            }
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Updates a Car as returned and updates the related rental history record
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// 
        /// POST /Cars/Return/84
        /// 
        /// </remarks>
        /// <param name="carId">The ID of the Car to be returned</param>
        /// <response code="200">Success</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpPost]
        [Route("Return/{carId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> PostReturnAsync(int carId)
        {
            try
            {
                await _repoRentalTransactions.ReturnCarAsync(carId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Deletes a specific Car
        /// </summary>
        /// <param name="id">The ID of the Car to be deleted</param>
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
            catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

    }
}