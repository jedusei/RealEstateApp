using RealEstate.Models;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using Xamarin.Forms;

[assembly: Dependency(typeof(RealEstate.Services.MessageService))]
namespace RealEstate.Services
{
    class MessageService : IMessageService
    {
        ObservableCollection<MessageInbox> _messageInboxes = new ObservableCollection<MessageInbox>(new MessageInbox[] {
            new MessageInbox (new User{ Name="Jane Doe", ProfileImageUrl="https://dieteticallyspeaking.com/wp-content/uploads/2017/01/profile-square.jpg" }, new Message[] {
                new Message
                {
                    Content = "Hello! I’m looking forward to having you stay in my apartment.\nPlease let me know when you come to your new appartment.",
                    DateCreated = new DateTime(2020,12,6,9,30,0)
                },
                new Message
                {
                    IsFromMe = true,
                    Content = "Hey! On Friday the 10th between 1:55 and 2:00 pm.",
                    DateCreated = new DateTime(2020,12,6,9,32,0)
                },
                new Message
                {
                    Content = "Hello! I’m looking forward to having you stay in my apartment.\nPlease let me know when you come to your new appartment.",
                    DateCreated = new DateTime(2020,12,6,9,33,0)
                },
                new Message
                {
                    IsFromMe = true,
                    Content = "Hey! On Friday the 10th between 1:55 and 2:00 pm.",
                    DateCreated = new DateTime(2020,12,6,9,34,0)
                },
            }){ UnreadMessageCount = 1 },
            new MessageInbox (new User{ Name="Billy Green", ProfileImageUrl="https://darylh.com/wp-content/uploads/2017/05/Profile-Picture-Square.jpg" }),
            new MessageInbox (new User{ Name="Anna Smith", ProfileImageUrl="https://th.bing.com/th/id/OIP.dFWozadKXRMaXcczQyDIwgHaHa?pid=Api&rs=1" }),
            new MessageInbox (new User{ Name="John Doe", ProfileImageUrl="https://th.bing.com/th/id/OIP.jcRbSaarQNTnJveKfWVQaAHaHa?pid=Api&w=1024&h=1024&rs=1" }),
            new MessageInbox (new User{ Name="Kristen Summers", ProfileImageUrl="https://dieteticallyspeaking.com/wp-content/uploads/2017/01/profile-square.jpg" }),
            new MessageInbox (new User{ Name="Amy Woolsworth", ProfileImageUrl="https://th.bing.com/th/id/OIP.m-e-s5X-7gYySbTaRrcKkAHaHa?pid=Api&w=540&h=540&rs=1" }),
            new MessageInbox (new User{ Name="Billy Green", ProfileImageUrl="https://darylh.com/wp-content/uploads/2017/05/Profile-Picture-Square.jpg" }),
            new MessageInbox (new User{ Name="Anna Smith", ProfileImageUrl="https://th.bing.com/th/id/OIP.dFWozadKXRMaXcczQyDIwgHaHa?pid=Api&rs=1" }),
            new MessageInbox (new User{ Name="John Doe", ProfileImageUrl="https://th.bing.com/th/id/OIP.jcRbSaarQNTnJveKfWVQaAHaHa?pid=Api&w=1024&h=1024&rs=1" })
        });

        public async Task<ObservableCollection<MessageInbox>> GetMessageInboxesAsync()
        {
            await Task.Delay(1500);
            return _messageInboxes;
        }

        public MessageInbox GetMessageInbox(int id)
        {
            return _messageInboxes.FirstOrDefault(inbox => inbox.Id == id);
        }

        public Task SendMessageAsync(MessageInbox messageInbox, string message)
        {
            messageInbox.Messages.Add(new Message
            {
                IsFromMe = true,
                Content = message,
                DateCreated = DateTime.Now
            });

            return Task.CompletedTask;
        }
    }
}
