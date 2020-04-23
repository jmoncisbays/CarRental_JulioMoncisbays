using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace carRental_WebAPI.Models
{
    [Table("RentalTransactions")]
    public class RentalTransaction
    {
        public int Id { get; set; }
        public int CarId { get; set; }
        public DateTime DateRented { get; set; }
        public DateTime? DateReturned { get; set; }

        public Car Car { get; set; }
    }
}
