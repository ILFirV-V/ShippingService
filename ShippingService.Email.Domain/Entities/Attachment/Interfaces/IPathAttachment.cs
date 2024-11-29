namespace ShippingService.Email.Domain.Entities.Attachment.Interfaces;

public interface IPathAttachment : IAttachment
{
    public string PublicUrl { get; init; }
    public bool IsValidPublicUrl { get; }
}