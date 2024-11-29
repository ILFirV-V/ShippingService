using ShippingService.Email.Domain.Entities.Attachment.Interfaces;
using ShippingService.Email.Domain.Entities.Recipient;

namespace ShippingService.Email.Domain.Entities.Message;

public abstract record SendMessageBase<TAttachment, TConnect>
    where TAttachment : IAttachment
{
    public string Title { get; init; }
    public string Content { get; init; }
    public IEnumerable<TAttachment> Attachments { get; init; }
    public IEnumerable<Recipient<TConnect>> Recipients { get; init; }

    protected SendMessageBase(Recipient<TConnect> recipient, string title, string content, IEnumerable<TAttachment> attachments)
        : this([recipient], title, content, attachments)
    {
    }
    
    protected SendMessageBase(Recipient<TConnect> recipient, string title, string content)
        : this([recipient], title, content)
    {
    }

    protected SendMessageBase(IEnumerable<Recipient<TConnect>> recipients, 
        string title, string content, IEnumerable<TAttachment> attachments)
    {
        Recipients = recipients;
        Title = title;
        Content = content;
        Attachments = attachments ?? [];
    }
    
    protected SendMessageBase(IEnumerable<Recipient<TConnect>> recipients, 
        string title, string content)
    {
        Recipients = recipients;
        Title = title;
        Content = content;
        Attachments = [];
    }
}