using ShippingService.Email.Domain.Entities.Attachment.Interfaces;

namespace ShippingService.Email.Domain.Entities.Attachment;

public record SendAttachment(string FileName, string PublicUrl) 
    : IAttachment, IPathAttachment
{
    public bool IsValidFileName => !string.IsNullOrEmpty(FileName);
    public bool IsValidPublicUrl => !string.IsNullOrEmpty(PublicUrl);
}