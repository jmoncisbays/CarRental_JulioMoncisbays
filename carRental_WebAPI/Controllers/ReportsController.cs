using System;
using System.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using carRental_WebAPI.Repositories;

namespace carRental_WebAPI.Controllers
{
    [Route("[controller]")]
    [ApiController]
    [Authorize]
    public class ReportsController : ControllerBase
    {
        private IRentalTransactionsRepository _repo;

        public ReportsController(IRentalTransactionsRepository repo, ICarsRepository repoCar)
        {
            _repo = repo;
        }

        /// <summary>
        /// Returns a report of rented cars grouping the data by 'groupby1' and then, optionally, by 'groupby2'
        /// </summary>
        /// <remarks>
        /// Sample Requests
        /// 
        /// GET /reports?groupby1=type
        /// 
        /// GET /reports?groupby1=brand&groupby2=model
        /// 
        /// </remarks>
        /// <returns>List of Cars</returns>
        /// <param name="groupby1">The name of the data to group the report by</param>
        /// <param name="groupby2">The name of the data to sub-group the report by</param>
        /// <response code="200">Success</response>
        /// <response code="400">Returns the error message if any</response>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetAsync(string groupby1, string groupby2)
        {
            // parametrers validation
            if (groupby1 == null) return BadRequest("Parameter 'groupby1' cannot be null.");
            if (groupby1 != "brand" && groupby1 != "model" && groupby1 != "type") return BadRequest("Parameter 'groupby1' is invalid. Valid values: 'brand', 'model', 'type'");
            if (groupby2 != null) {
                if (groupby2 != "brand" && groupby2 != "model" && groupby2 != "type") return BadRequest("Parameter 'groupby2' is invalid. Valid values: 'brand', 'model', 'type'");
                if (groupby1.ToLower() == groupby2.ToLower()) return BadRequest("The values of both parameters cannot be the same.");
            }

            List<KeyCountParent> data = await (from rt in _repo.Get()
                                               group rt by new
                                               {
                                                   Id = (groupby1 == "brand" ? rt.Car.CarBrand.Id : groupby1 == "model" ? rt.Car.CarModel.Id : rt.Car.CarType.Id),
                                                   Name = (groupby1 == "brand" ? rt.Car.CarBrand.Name : groupby1 == "model" ? rt.Car.CarModel.Name : rt.Car.CarType.Name)
                                               }
                                               into g
                                               select new KeyCountParent
                                               {
                                                   Id = g.Key.Id,
                                                   Name = g.Key.Name,
                                                   Count = g.Count(),
                                               }).ToListAsync();

            if (groupby2 != null)
            {
                data.ForEach(item => {
                    item.Children = (from rt in _repo.Get()
                                     where (groupby1 == "brand" && rt.Car.CarBrandId == item.Id) || (groupby1 == "model" && rt.Car.CarModelId == item.Id) || (groupby1 == "type" && rt.Car.CarTypeId == item.Id)
                                     group rt by new {
                                         Id = (groupby2 == "brand" ? rt.Car.CarBrand.Id : groupby2 == "model" ? rt.Car.CarModel.Id : rt.Car.CarType.Id),
                                         Name = (groupby2 == "brand" ? rt.Car.CarBrand.Name : groupby2 == "model" ? rt.Car.CarModel.Name : rt.Car.CarType.Name)
                                     } into g
                                     select new KeyCount
                                     {
                                         Id = g.Key.Id,
                                         Name = g.Key.Name,
                                         Count = g.Count()
                                     }).ToList();
                });
            }

            return new JsonResult(data);

        }

    }

    public class KeyCount
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
    }

    public class KeyCountParent : KeyCount
    {
        public List<KeyCount> Children { get; set; }
    }

}