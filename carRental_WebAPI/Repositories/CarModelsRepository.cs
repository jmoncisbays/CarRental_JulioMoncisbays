using System;
using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public class CarModelsRepository : ICarModelsRepository
    {
        private CarRentalContext _context;

        public CarModelsRepository(CarRentalContext context)
        {
            _context = context;
        }

        public async Task<int> AddAsync(CarModel carModel)
        {
            await _context.CarModels.AddAsync(carModel);
            await _context.SaveChangesAsync();

            return carModel.Id;
        }

        public IQueryable<CarModel> Get() => _context.CarModels;

        public async Task RemoveAsync(int id)
        {
            CarModel carModel = await _context.CarModels.FindAsync(id);

            if (carModel == null) throw new Exception($"The model with ID {id} does not exist");
            
            _context.CarModels.Remove(carModel);
            await _context.SaveChangesAsync();
        }
    }
}
