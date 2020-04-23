using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace carRental_WebAPI.Models
{
    [Table("CarBrands")]
    public class CarBrand
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
