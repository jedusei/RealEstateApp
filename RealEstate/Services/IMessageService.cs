using RealEstate.Models;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace RealEstate.Services
{
    interface IMessageService
    {
        Task<ObservableCollection<MessageInbox>> GetMessageInboxesAsync();
        MessageInbox GetMessageInbox(int id);
        Task SendMessageAsync(MessageInbox messageInbox, string message);
    }
}
