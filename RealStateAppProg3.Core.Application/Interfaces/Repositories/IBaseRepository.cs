
namespace RealStateAppProg3.Core.Application.Interfaces.Repositories
{
    public interface IBaseRepository<T>
    {
        Task<List<T>> GetAllAsync();
        Task<List<T>> GetAllWithInclude(List<string> properties);
        Task<T> GetByIdAsync(int id);
        Task<T> SaveAsync(T entity);
        Task<T> UpdateAsync(T entity, int id);
        Task RemoveAsync(T entity);
        Task<int> CountAsync();
    }
}
