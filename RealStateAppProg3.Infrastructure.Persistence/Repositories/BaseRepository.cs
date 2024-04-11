
using Microsoft.EntityFrameworkCore;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Infrastructure.Persistence.Context;

namespace RealStateAppProg3.Infrastructure.Persistence.Repositories
{
    public class BaseRepository<T> : IBaseRepository<T>
        where T : class
    {
        private readonly RealStateAppContext _ctx;
        public BaseRepository(RealStateAppContext ctx)
        {
            _ctx = ctx;
        }

        public async Task<int> CountAsync()
        {
            return await _ctx.Set<T>().CountAsync();
        }

        public virtual async Task<List<T>> GetAllAsync()
        {
            return await _ctx.Set<T>().ToListAsync();
        }

        public async Task<List<T>> GetAllWithInclude(List<string> properties)
        {
            var query = _ctx.Set<T>().AsQueryable();
            foreach (string property in properties)
            {
                query = query.Include(property);
            }
            return await query.ToListAsync();
        }

        public virtual async Task<T> GetByIdAsync(int id)
        {
            return await _ctx.Set<T>().FindAsync(id);
        }

        public async Task RemoveAsync(T entity)
        {
            _ctx.Set<T>().Remove(entity);
            await _ctx.SaveChangesAsync();
        }

        public virtual async Task<T> SaveAsync(T entity)
        {
            _ctx.Set<T>().Add(entity);
            await _ctx.SaveChangesAsync();
            return entity;
        }

        public virtual async Task<T> UpdateAsync(T entity, int id)
        {
            T entry = await _ctx.Set<T>().FindAsync(id);
            _ctx.Entry(entry).CurrentValues.SetValues(entity);
            await _ctx.SaveChangesAsync();
            return entry;
        }
    }
}
