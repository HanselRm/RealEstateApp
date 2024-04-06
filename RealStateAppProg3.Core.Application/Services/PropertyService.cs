
using AutoMapper;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Property;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Services
{
    public class PropertyService : BaseService<PropertyViewModel, SavePropertyViewModel, Property>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        private readonly IUpgradesPropertyRepository _upgradePropertyRepository;
        public PropertyService(IPropertyRepository propertyRepository,
                               IMapper mapper,
                               IUpgradesPropertyRepository upgradesPropertyRepository) : base(propertyRepository,mapper)
        {
            _propertyRepository =  propertyRepository;
            _mapper = mapper;
        }

        public override async Task<SavePropertyViewModel> SaveAsync(SavePropertyViewModel vm)
        {
            var property = _mapper.Map<Property>(vm);
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
