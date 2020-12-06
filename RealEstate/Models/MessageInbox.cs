using System.Collections.ObjectModel;
using Xamarin.Forms;

namespace RealEstate.Models
{
    public class MessageInbox : BindableObject
    {
        static int _nextId = 1;
        int _unreadMessageCount;

        public int Id { get; } = _nextId++;
        public User User { get; }
        public ObservableCollection<Message> Messages { get; }
        public Message LastMessage => Messages.Count > 0 ? Messages[^1] : null;
        public int UnreadMessageCount
        {
            get => _unreadMessageCount;
            set
            {
                _unreadMessageCount = value;
                OnPropertyChanged();
            }
        }

        public MessageInbox(User user, Message[] messages = null)
        {
            User = user;
            Messages = messages == null ? new ObservableCollection<Message>() : new ObservableCollection<Message>(messages);
            Messages.CollectionChanged += Messages_CollectionChanged;
        }

        ~MessageInbox()
        {
            Messages.CollectionChanged -= Messages_CollectionChanged;
        }

        void Messages_CollectionChanged(object sender, System.Collections.Specialized.NotifyCollectionChangedEventArgs e)
        {
            OnPropertyChanged(nameof(LastMessage));
        }
    }
}
