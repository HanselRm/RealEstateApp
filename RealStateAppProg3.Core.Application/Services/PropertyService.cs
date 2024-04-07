
using AutoMapper;
using InternetBanking.Core.Application.Services;
using Microsoft.AspNetCore.Http;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Domain.Entities;
using System.Xml.Serialization;

namespace RealStateAppProg3.Core.Application.Services
{
    public class PropertyService : BaseService<PropertyViewModel, SavePropertyViewModel, Property>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        private readonly IMapper _mapper;
        private readonly IUpgradesPropertyRepository _upgradePropertyRepository;
        private readonly IHttpContextAccessor contextAccessor;
        public PropertyService(IPropertyRepository propertyRepository,
                               IMapper mapper,
                               IUpgradesPropertyRepository upgradePropertyRepository) : base(propertyRepository,mapper)
        {
            _propertyRepository =  propertyRepository;
            _upgradePropertyRepository = upgradePropertyRepository;
            _mapper = mapper;
        }

        public override async Task<SavePropertyViewModel> SaveAsync(SavePropertyViewModel vm)
        {
            var property = _mapper.Map<Property>(vm);
            vm.Code = CodeGenerator.Unique9DigitsGenerator().ToString();
            vm.IdUser = contextAccessor.HttpContext.Session.Get<AuthenticationResponse>("user").Id; 
            property = await _propertyRepository.SaveAsync(property);
            foreach(var item in vm.Upgrades)
            {
                item.IdProperty = int.Parse(property.Code);
                await _upgradePropertyRepository.SaveAsync(_mapper.Map<UpgradesProperty>(item));
            }
            return _mapper.Map<SavePropertyViewModel>(property);
        }
    }
}
