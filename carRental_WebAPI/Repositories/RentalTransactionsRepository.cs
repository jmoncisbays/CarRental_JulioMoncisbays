using System;
using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public class RentalTransactionsRepository : IRentalTransactionsRepository
    {
        private CarRentalContext _context;

        public IQueryable<RentalTransaction> Get() => _context.RentalTransactions;

        public RentalTransactionsRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task RentCarAsync(int carId)
        {
            Car car = await _context.Cars.FindAsync(carId);

            if (car == null) throw new Exception("Car not found");

            RentalTransaction rt = new RentalTransaction()
            {
                Id = 0,
                CarId = carId,
                DateRented = DateTime.Now,
                DateReturned = null
            };
            await _context.RentalTransactions.AddAsync(rt);
            await _context.SaveChangesAsync();

            car.IsAvailable = false;
            car.CurrentRentalTransactionId = rt.Id;
            await _context.SaveChangesAsync();
        }

        public async Task ReturnCarAsync(int carId)
        {
            Car car = await _context.Cars.FindAsync(carId);

            if (car == null) throw new Exception("Car not found");

            // ? car is not rented
            if (car.IsAvailable == true) throw new Exception("Car cannot be returned because it has not neen rented");

            RentalTransaction rt = await _context.RentalTransactions.FindAsync(car.CurrentRentalTransactionId);

            if (rt == null) throw new Exception("Related rental transaction not found");

            rt.DateReturned = DateTime.Now;
            car.IsAvailable = true;
            car.CurrentRentalTransactionId = null;

            await _context.SaveChangesAsync();
        }

        public DateTime GetLatestRentalDate() {
            return _context.RentalTransactions.Max(rt => rt.DateRented);
        }
    }
}
