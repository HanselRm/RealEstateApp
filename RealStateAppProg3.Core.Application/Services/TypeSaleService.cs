
using AutoMapper;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Services
{
    public class TypeSaleService : BaseService<TypeSaleViewModel, SaveTypeSaleViewModel, TypeSale>, ITypeSaleService
    {
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly IMapper _mapper;
        public TypeSaleService(ITypeSaleRepository typeSaleRepository,IMapper mapper): base(typeSaleRepository,mapper)
        {
            _typeSaleRepository = typeSaleRepository;
            _mapper = mapper;
        }
    }
}
