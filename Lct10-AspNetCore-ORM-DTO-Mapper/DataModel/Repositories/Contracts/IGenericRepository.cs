namespace DataModel.Repositories.Contracts;

public interface IGenericRepository<TEntity, TKey> where TEntity : class
{
    IQueryable<TEntity> GetAll();
    Task<TEntity?> FindAsync(TKey key);
    Task AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task DeleteAsync(TEntity entity);
}
