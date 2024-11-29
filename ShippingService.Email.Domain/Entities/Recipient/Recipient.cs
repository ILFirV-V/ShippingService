namespace ShippingService.Email.Domain.Entities.Recipient;

public record Recipient<T>
{
    public T RecipientConnect { get; init; }
}