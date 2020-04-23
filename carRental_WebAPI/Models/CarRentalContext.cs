using Microsoft.EntityFrameworkCore;

namespace carRental_WebAPI.Models
{
    public class CarRentalContext : DbContext
    {
        public CarRentalContext(DbContextOptions<CarRentalContext> options) : base(options) { }

        public DbSet<Car> Cars { get; set; }
        public DbSet<CarBrand> CarBrands { get; set; }
        public DbSet<CarModel> CarModels { get; set; }
        public DbSet<CarType> CarTypes { get; set; }
        public DbSet<RentalTransaction> RentalTransactions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) { }
    }
}
