using RealEstate.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    interface IMessageService
    {
        Task<ObservableCollection<MessageInbox>> GetMessageInboxesAsync();
        Task SendMessageAsync(string message);
    }
}
