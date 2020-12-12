using RealEstate.Models;
using RealEstate.Services;
using RealEstate.Views;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class ChatViewModel : BaseViewModel
    {
        IMessageService _messageService;
        string _currentMessage;

        public MessageInbox MessageInbox { get; private set; }
        public string CurrentMessage
        {
            get => _currentMessage;
            set => SetProperty(ref _currentMessage, value, onChanged: SendMessageCommand.ChangeCanExecute);
        }
        public Command SendMessageCommand { get; private set; }

        public ChatViewModel()
        {
            _messageService = DependencyService.Get<IMessageService>();
            SendMessageCommand = new Command(() =>
            {
                _messageService.SendMessageAsync(MessageInbox, CurrentMessage.Trim(' ', '\n'));
                CurrentMessage = string.Empty;
            }, () => !string.IsNullOrEmpty(CurrentMessage?.Trim(' ', '\n')));
        }

        public override void Initialize(object navigationData)
        {
            var args = navigationData as ChatPage.Args;
            MessageInbox = _messageService.GetMessageInbox(args.MessageInboxId);
            OnPropertyChanged(nameof(MessageInbox));
        }
    }
}
