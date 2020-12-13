using RealEstate.Views;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    public interface INavigationService
    {
        Task InitializeAsync();
        Task GoToPageAsync<TPage>(object navigationData = null, bool clearHistory = false, bool removeCurrentPage = false) where TPage : BasePage;
        Task GoBackAsync(bool animated = true);
        Task PopToRootAsync(bool animated = true);
    }
}
