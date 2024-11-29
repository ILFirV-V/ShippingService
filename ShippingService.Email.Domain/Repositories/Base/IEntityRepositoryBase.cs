using ShippingService.Email.Core.Common;
using ShippingService.Email.Domain.Entities.Base;

namespace ShippingService.Email.Domain.Repositories.Base;

public interface IEntityRepositoryBase<TEntity, TId>
    where TEntity : EntityBase<TId>
    where TId : IEquatable<TId>
{
    public Task<OperationResult<TEntity>> GetById(TId id);

    public Task<OperationResult<TEntity?>> GetByIdOrNull(TId id);

    public Task<OperationResult<IList<TEntity>>> GetAll();

    public Task<OperationResult<TId>>CreateOrUpdate(TEntity entity);

    public Task<OperationResult> Delete(TId id);
}