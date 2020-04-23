using System;
using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public class CarTypesRepository : ICarTypesRepository
    {
        private CarRentalContext _context;

        public CarTypesRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(CarType carType)
        {
            await _context.CarTypes.AddAsync(carType);
            await _context.SaveChangesAsync();

            return carType.Id;
        }

        public IQueryable<CarType> Get() => _context.CarTypes;

        public async Task RemoveAsync(int id)
        {
            CarType carType = await _context.CarTypes.FindAsync(id);

            if (carType == null) throw new Exception($"The type with ID {id} does not exist");

            _context.CarTypes.Remove(carType);
            await _context.SaveChangesAsync();
        }
    }
}
