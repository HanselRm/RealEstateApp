using AutoMapper;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Domain.Entities;
using System.Xml.Serialization;

namespace RealStateAppProg3.Core.Application.Services
{
    public class PropertyService : BaseService<PropertyViewModel, SavePropertyViewModel, Property>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;

        private readonly IMapper _mapper;
        private readonly IUpgradesPropertyRepository _upgradePropertyRepository;
        private readonly IUpgradeRepository _upgradeRepository;
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IUpgradeRepository upgradeRepository,
                                IUpgradesPropertyRepository upgradePropertyRepository) : base(propertyRepository,mapper)
        {
            _propertyRepository =  propertyRepository;
            _upgradePropertyRepository = upgradePropertyRepository;
            _mapper = mapper;
            _upgradeRepository = upgradeRepository;
        }

        public  async Task<SavePropertyViewModel> SaveAsync(SavePropertyViewModel vm, string Id)
        {

            //genera el codigo
            vm.Code = CodeGenerator.Unique9DigitsGenerator().ToString();

            var verifyCode = await _propertyRepository.GetByIdAsync(vm.Code);
            while (verifyCode != null)
            {
                vm.Code = CodeGenerator.Unique9DigitsGenerator().ToString();
                verifyCode = await _propertyRepository.GetByIdAsync(vm.Code);
            }
            //mapea de viewmodel a entidad
            var property = _mapper.Map<Property>(vm);
            property.Code = vm.Code;
            property.Created = DateTime.Now;

            vm.IdUser = Id;
            property = await _propertyRepository.SaveAsync(property);

            
            foreach(var item in vm.UpgradesId)
            {
                UpgradesProperty upgradesProperty = new();
                upgradesProperty.IdProperty = property.Code;
                upgradesProperty.IdUpgrade = item; 
                await _upgradePropertyRepository.SaveAsync(upgradesProperty);
            }
            return _mapper.Map<SavePropertyViewModel>(property);
        }
    }
}
