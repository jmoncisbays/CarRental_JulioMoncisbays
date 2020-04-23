using System;
using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public class CarsRepository : ICarsRepository
    {
        private CarRentalContext _context;

        public CarsRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            await _context.SaveChangesAsync();

            return car.Id;
        }

        public IQueryable<Car> Get() => _context.Cars;

        public async Task RemoveAsync(int id)
        {
            Car car = await _context.Cars.FindAsync(id);

            if (car == null) throw new Exception($"The car with ID {id} does not exist");

            _context.Cars.Remove(car);
            await _context.SaveChangesAsync();
        }
    }
}
