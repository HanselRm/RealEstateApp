﻿using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Domain.Entities;
using RealStateAppProg3.Infrastructure.Persistence.Context;

namespace RealStateAppProg3.Infrastructure.Persistence.Repositories
{
    public class UpgradesPropertyRepository : BaseRepository<UpgradesProperty>, IUpgradesPropertyRepository
    {
        private readonly RealStateAppContext _ctx;
        public UpgradesPropertyRepository(RealStateAppContext ctx) : base(ctx)
        {
            _ctx = ctx;
        }
    }
}
