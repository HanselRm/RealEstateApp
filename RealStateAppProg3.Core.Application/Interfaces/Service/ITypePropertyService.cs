using InternetBanking.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Domain.Entities;


namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface ITypePropertyService : IBaseService<TypePropertyViewModel,SaveTypePropertyViewModel,TypeProperty>
    {
    }
}
