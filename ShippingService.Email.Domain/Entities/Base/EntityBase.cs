namespace ShippingService.Email.Domain.Entities.Base;

public abstract class EntityBase<TId> : IEntity<TId>
    where TId : IEquatable<TId>
{
    public TId Id { get; set; }
}