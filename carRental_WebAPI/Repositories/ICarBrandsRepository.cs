using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public interface ICarBrandsRepository
    {
        IQueryable<CarBrand> Get();
        Task<int> AddAsync(CarBrand carBrand);
        Task RemoveAsync(int id);
    }
}
