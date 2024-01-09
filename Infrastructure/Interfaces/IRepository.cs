namespace Infrastructure.Interfaces;

public interface IRepository<TEntity> where TEntity : class
{
    Task<IQueryable<TEntity>> GetAllAsync();
    Task<TEntity> GetByIdAsync(int id);
    Task<TEntity> AddAsync(TEntity entity);
    Task UpdateAsync(TEntity entity);
    Task<TEntity> DeleteAsync(TEntity entity);
}
