using AutoMapper;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Services
{
    public class TypePropertyService : BaseService<TypePropertyViewModel, SaveTypePropertyViewModel, TypeProperty>,
        ITypePropertyService 
    {
        private readonly ITypePropertyRepository _typePropertyRepository;
        private readonly IMapper _mapper;
        public TypePropertyService(ITypePropertyRepository typePropertyRepository, IMapper mapper) : 
            base(typePropertyRepository, mapper)
        {
            _typePropertyRepository = typePropertyRepository;
            _mapper = mapper;
        }
    }
}
