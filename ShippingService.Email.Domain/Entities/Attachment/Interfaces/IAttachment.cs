namespace ShippingService.Email.Domain.Entities.Attachment.Interfaces;

public interface IAttachment
{
    public string FileName { get; init; }

    public bool IsValidFileName { get; }
}