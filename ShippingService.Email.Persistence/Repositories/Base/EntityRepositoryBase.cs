using Microsoft.EntityFrameworkCore;
using ShippingService.Email.Core.Common;
using ShippingService.Email.Domain.Entities.Base;
using ShippingService.Email.Domain.Repositories.Base;
using ShippingService.Email.Persistence.Context;

namespace ShippingService.Email.Persistence.Repositories.Base;

public abstract class EntityRepositoryBase<TEntity, TId> : IEntityRepositoryBase<TEntity, TId>
    where TEntity : EntityBase<TId>
    where TId : IEquatable<TId>
{
    protected EntityRepositoryBase(DataContext context)
    {
        Context = context;
        Items = context.Set<TEntity>();
    }

    protected virtual DbSet<TEntity> Items { get; }
    protected virtual DataContext Context { get; }

    public virtual async Task<OperationResult<TEntity>> GetById(TId id)
    {
        var getEntity = await GetByIdOrNull(id);
        if (getEntity.IsFail && getEntity.Error != null)
                return getEntity.Error;

        var entity = getEntity.Result;
        if (entity is null)
            return Error.NotFound($"Entity with id: {id} was not found.");

        return entity;
    }

    public virtual async Task<OperationResult<TEntity?>> GetByIdOrNull(TId id)
    {
        var entity = await Items.FirstOrDefaultAsync(entity => entity.Id.Equals(id));

        return entity;
    }

    public virtual async Task<OperationResult<IList<TEntity>>> GetAll()
    {
        return await Items.ToListAsync();
    }

    public virtual async Task<OperationResult<TId>> CreateOrUpdate(TEntity entity)
    {
        if (entity.Id.Equals(default))
        {
            Items.Add(entity);
        }
        else
        {
            Items.Update(entity);
        }
        var saveResult = await SaveChanges();
        if (saveResult.IsFail)
        {
            return saveResult.Error;
        }

        return entity.Id;
    }

    public virtual async Task<OperationResult> Delete(TId id)
    {
        var getEntity = await GetById(id);
        if (getEntity.IsFail && getEntity.Error.StatusCode != StatusCode.NotFound)
            return getEntity.Error;

        var entity = getEntity.Result;
        if (entity is not null)
            Items.Remove(entity);

        return await SaveChanges();
    }

    protected virtual async Task<OperationResult> SaveChanges()
    {
        try
        {
            await Context.SaveChangesAsync();
        }
        catch (DbUpdateException e)
        {
            return Error.Internal($"Failed to update database: {e.Message}");
        }
        catch (Exception e)
        {
            return Error.Internal($"Unknown error while updating database: {e.Message}");
        }

        return OperationResult.Success();
    }
}