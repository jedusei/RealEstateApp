using RealEstate.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface IRentalService
    {
        Task<Rental[]> GetRentalsAsync();
        Task<ObservableCollection<Rental>> GetFavoriteRentalsAsync();
        Task<ObservableCollection<Rental>> GetRentalHistoryAsync();
    }
}
