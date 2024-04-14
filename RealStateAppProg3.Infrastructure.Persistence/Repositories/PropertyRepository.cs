

using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Domain.Entities;
using RealStateAppProg3.Infrastructure.Persistence.Context;

namespace RealStateAppProg3.Infrastructure.Persistence.Repositories
{
    public class PropertyRepository : BaseRepository<Property>, IPropertyRepository
    {
        private readonly RealStateAppContext _ctx;
        public PropertyRepository(RealStateAppContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }

        public async Task<Property> GetByIdAsync(string id)
        {
            return await _ctx.Set<Property>().FindAsync(id);
        }

        public async Task<Property> UpdateAsync(Property vm, string id)
        {
            Property entry = await _ctx.Set<Property>().FindAsync(id);
            _ctx.Entry(entry).CurrentValues.SetValues(vm);
            await _ctx.SaveChangesAsync();
            return entry;
        }
    }
}
