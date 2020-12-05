using RealEstate.Models;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IRentalService
    {
        Task<Rental[]> GetRentalsAsync();
    }
}
