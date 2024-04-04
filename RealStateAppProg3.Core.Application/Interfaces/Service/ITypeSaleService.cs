

using InternetBanking.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Interfaces.Service
{
    public interface ITypeSaleService : IBaseService<TypeSaleViewModel,SaveTypeSaleViewModel,TypeSale>
    {
    }
}
