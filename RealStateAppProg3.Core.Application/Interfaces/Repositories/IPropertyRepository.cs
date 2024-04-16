﻿
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Interfaces.Repositories
{
    public interface IPropertyRepository : IBaseRepository<Property>
    {
        Task<Property> GetByIdAsync(string id);
        Task<List<Property>> GetPropertiesByIdAgentAsync(string agentId);
    }
}
