using RealEstate.Models;
using RealEstate.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace RealEstate.ViewModels
{
    public class MessagesViewModel : BaseViewModel
    {
        IMessageService _messageService;

        public ObservableCollection<MessageInbox> MessageInboxes { get; private set; }

        public MessagesViewModel()
        {
            _messageService = DependencyService.Get<IMessageService>();
            LoadStatus = LoadStatus.Loading;
        }

        public override async void OnStart()
        {
            base.OnStart();
            MessageInboxes = await _messageService.GetMessageInboxesAsync();
            OnPropertyChanged(nameof(MessageInboxes));
            LoadStatus = LoadStatus.Loaded;
        }
    }
}
