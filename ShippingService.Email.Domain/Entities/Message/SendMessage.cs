using ShippingService.Email.Domain.Entities.Attachment.Interfaces;
using ShippingService.Email.Domain.Entities.Recipient;

namespace ShippingService.Email.Domain.Entities.Message;

public record SendMessage : SendMessageBase<IPathAttachment, string>
{
    public SendMessage(Recipient<string> recipient, string title, string content, IEnumerable<IPathAttachment> attachments)
        : this([recipient], title, content, attachments)
    {
    }
    
    public SendMessage(IEnumerable<Recipient<string>> recipients, string title, string content, IEnumerable<IPathAttachment> attachments) 
        : base(recipients, title, content, attachments)
    {
    }
}