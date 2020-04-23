using System;
using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public class CarBrandsRepository : ICarBrandsRepository
    {
        private CarRentalContext _context;

        public CarBrandsRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(CarBrand carBrand)
        {
            await _context.CarBrands.AddAsync(carBrand);
            await _context.SaveChangesAsync();

            return carBrand.Id;
        }

        public IQueryable<CarBrand> Get() => _context.CarBrands;

        public async Task RemoveAsync(int id)
        {
            CarBrand carBrand = await _context.CarBrands.FindAsync(id);

            if (carBrand == null) throw new Exception($"The brand with ID {id} does not exist");

            _context.CarBrands.Remove(carBrand);
            await _context.SaveChangesAsync();
        }
    }
}
