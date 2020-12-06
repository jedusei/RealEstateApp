using RealEstate.ViewModels;

namespace RealEstate.Views
{
    public partial class ChatPage : BasePage
    {
        ChatViewModel _viewModel;
        public class Args
        {
            public int MessageInboxId { get; set; }
        }

        public ChatPage()
        {
            InitializeComponent();
            BindingContext = _viewModel = new ChatViewModel();
        }

        void SendButton_Clicked(object sender, System.EventArgs e)
        {
            listView.ScrollTo(_viewModel.MessageInbox.Messages[^1], Syncfusion.ListView.XForms.ScrollToPosition.End);
        }
    }
}
