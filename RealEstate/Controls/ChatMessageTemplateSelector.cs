using RealEstate.Models;
using Xamarin.Forms;

namespace RealEstate.Controls
{
    class ChatMessageTemplateSelector : DataTemplateSelector
    {
        public DataTemplate IncomingMessageTemplate { get; set; }
        public DataTemplate OutgoingMessageTemplate { get; set; }

        protected override DataTemplate OnSelectTemplate(object item, BindableObject container)
        {
            return (item as Message).IsFromMe ? OutgoingMessageTemplate : IncomingMessageTemplate;
        }
    }
}
