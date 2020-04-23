using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public interface ICarModelsRepository
    {
        IQueryable<CarModel> Get();
        Task<int> AddAsync(CarModel carModel);
        Task RemoveAsync(int id);
    }
}
