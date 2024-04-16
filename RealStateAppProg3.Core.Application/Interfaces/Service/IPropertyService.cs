using InternetBanking.Core.Application.Interfaces.Service;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface IPropertyService : IBaseService<PropertyViewModel,SavePropertyViewModel, Property>
    {
        Task<SavePropertyViewModel> SaveAsync(SavePropertyViewModel vm, string Id);
        Task<List<PropertyViewModelDetail>> GetallWithIncludeDetail();
        Task<List<PropertyViewModel>> GetallWithInclude();
        Task RemoveAsync(string id);
        Task<List<PropertyViewModel>> GetAllPropertiesByAgentId(string code);
        Task<SavePropertyViewModel> GetByIdAsync(string id);
        Task<SavePropertyViewModel> UpdateAsync(SavePropertyViewModel vm, string id);
    }
}
