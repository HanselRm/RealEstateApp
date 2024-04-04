using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Domain.Entities;
using RealStateAppProg3.Infrastructure.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateAppProg3.Infrastructure.Persistence.Repositories
{
    public class TypePropertyRepository : BaseRepository<TypeProperty>, ITypePropertyRepository
    {
        private readonly RealStateAppContext _ctx;
        public TypePropertyRepository(RealStateAppContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
