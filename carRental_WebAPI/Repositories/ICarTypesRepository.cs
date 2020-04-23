using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public interface ICarTypesRepository
    {
        IQueryable<CarType> Get();
        Task<int> AddAsync(CarType carType);
        Task RemoveAsync(int id);
    }
}
