using DataModel.Repositories.Contracts;

namespace DataModel.Repositories;

internal class GenericRepository<TEntity, TKey> : IGenericRepository<TEntity, TKey> where TEntity : class
{
    protected readonly DataModelContext _context;

    public GenericRepository(DataModelContext context) => _context = context;

    public virtual Task AddAsync(TEntity entity)
    {
        _context.Set<TEntity>().Add(entity);

        return _context.SaveChangesAsync();
    }

    public virtual Task DeleteAsync(TEntity entity)
    {
        _context.Set<TEntity>().Remove(entity);

        return _context.SaveChangesAsync();
    }

    public virtual Task<TEntity?> FindAsync(TKey key) => _context.Set<TEntity>().FindAsync(key).AsTask();

    public virtual IQueryable<TEntity> GetAll() => _context.Set<TEntity>();

    public virtual Task UpdateAsync(TEntity entity)
    {
        _context.Set<TEntity>().Update(entity);

        return _context.SaveChangesAsync();
    }
}
