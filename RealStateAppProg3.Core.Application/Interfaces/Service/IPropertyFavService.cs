
using InternetBanking.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.PropertyFav;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface IPropertyFavService : IBaseService<PropertyFavViewModel,SavePropertyFavViewModel,PropertyFav>
    {
    }
}
