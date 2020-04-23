using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace carRental_WebAPI.Models
{
    [Table("Cars")]
    public class Car
    {
        public int Id { get; set; }
        public int CarTypeId { get; set; }
        public int CarBrandId { get; set; }
        public int CarModelId { get; set; }
        public bool IsAvailable { get; set; }
        public Nullable<int> CurrentRentalTransactionId { get; set; }

        public CarType CarType { get; set; }
        public CarBrand CarBrand { get; set; }
        public CarModel CarModel { get; set; }
        public List<RentalTransaction> RentalTransactions { get; set; }
    }
}
