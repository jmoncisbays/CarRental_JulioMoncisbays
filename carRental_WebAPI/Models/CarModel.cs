using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace carRental_WebAPI.Models
{
    [Table("CarModels")]
    public class CarModel
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Car> Cars { get; set; }
    }
}
