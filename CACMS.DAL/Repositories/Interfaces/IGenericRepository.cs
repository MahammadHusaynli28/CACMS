namespace CACMS.DAL.Repositories.Interfaces;

public interface IGenericRepository<T> where T : class
{
    Task<IEnumerable<T>> GetAllAsync();
    Task<T?> GetByIdAsync(int id);
    Task<T> CreateAsync(T entity);
    void Update(T entity);
    void Delete(T entity);
    Task SaveAsync();
}
