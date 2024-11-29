namespace ShippingService.Email.Domain.Entities.Base;

public interface IEntity<TId>
{
    public TId Id { get; set; }
}