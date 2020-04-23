using System;
using carRental_WebAPI.Models;
using System.Linq;
using System.Threading.Tasks;

namespace carRental_WebAPI.Repositories
{
    public interface IRentalTransactionsRepository
    {
        IQueryable<RentalTransaction> Get();
        Task RentCarAsync(int carId);
        Task ReturnCarAsync(int carId);
        DateTime GetLatestRentalDate();
    }
}
