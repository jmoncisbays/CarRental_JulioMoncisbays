using System.Linq;
using System.Threading.Tasks;
using carRental_WebAPI.Models;

namespace carRental_WebAPI.Repositories
{
    public interface ICarsRepository
    {
        IQueryable<Car> Get();
        Task<int> AddAsync(Car car);
        Task RemoveAsync(int id);
    }
}
