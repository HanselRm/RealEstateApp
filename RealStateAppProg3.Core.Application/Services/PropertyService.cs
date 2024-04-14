using AutoMapper;
using InternetBanking.Core.Application.Services;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using RealStateAppProg3.Core.Domain.Entities;
using System.Xml.Serialization;

namespace RealStateAppProg3.Core.Application.Services
{
    public class PropertyService : BaseService<PropertyViewModel, SavePropertyViewModel, Property>, IPropertyService
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly ITypeSaleRepository _typeSaleRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        private readonly IUpgradesPropertyRepository _upgradePropertyRepository;
        private readonly IUpgradeRepository _upgradeRepository;
        public PropertyService(IPropertyRepository propertyRepository, IMapper mapper, IUpgradeRepository upgradeRepository,
                                IUpgradesPropertyRepository upgradePropertyRepository, ITypeSaleRepository typeSaleRepository,
                                IUserService userService) : base(propertyRepository,mapper)
        {
            _propertyRepository =  propertyRepository;
            _upgradePropertyRepository = upgradePropertyRepository;
            _mapper = mapper;
            _upgradeRepository = upgradeRepository;
            _upgradePropertyRepository = upgradePropertyRepository;
            _typeSaleRepository = typeSaleRepository;
            _typeSaleRepository = typeSaleRepository;
            _userService = userService;

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

        public async Task<List<PropertyViewModel>> GetallWithInclude()
        {
            var propiedad = await _propertyRepository.GetAllWithInclude(new List<string> { "TypeProperty", "TypeSale", "Upgrades" });

            return propiedad.Select(p => new PropertyViewModel
            {
                Code = p.Code,
                Description = p.Description,
                Value = p.Value,
                NumberRooms = p.NumberRooms,
                Bathrooms = p.Bathrooms,
                SizeInMeters = p.SizeInMeters,
                IdTypeProperty = p.IdTypeProperty,
                IdTypeSale = p.IdTypeSale,
                UrlImage1 = p.UrlImage1,
                UrlImage2 = p.UrlImage2,
                UrlImage3 = p.UrlImage3,
                UrlImage4 = p.UrlImage4,
                IdUser = p.IdUser,
                TypeProperty = _mapper.Map<TypePropertyViewModel>(p.TypeProperty),
                TypeSale = _mapper.Map<TypeSaleViewModel>(p.TypeSale),
                Upgrades = _mapper.Map<List<UpgradesProperty>>(p.Upgrades)

            }).ToList();
            
        }

        public async Task<List<PropertyViewModelDetail>> GetallWithIncludeDetail()
        {
            var propiedad = await _propertyRepository.GetAllWithInclude(new List<string> { "TypeProperty", "TypeSale", "Upgrades" });
            var upgradesP = await _upgradeRepository.GetAllAsync();
            var user = await _userService.GetAllAsync();

            return propiedad.Select(p => new PropertyViewModelDetail
            {

                Code = p.Code,
                Description = p.Description,
                Value = p.Value,
                NumberRooms = p.NumberRooms,
                Bathrooms = p.Bathrooms,
                SizeInMeters = p.SizeInMeters,
                TypeProperty = p.TypeProperty.Name,
                TypeSale = p.TypeSale.Name,
                UrlImage1 = p.UrlImage1,
                UrlImage2 = p.UrlImage2,
                UrlImage3 = p.UrlImage3,
                UrlImage4 = p.UrlImage4,
                IdUser = p.IdUser,
                NameAgent = user.FirstOrDefault(u => u.Id == p.IdUser).Name,
                PhoneNumber = user.FirstOrDefault(u => u.Id == p.IdUser).PhoneNumber,
                fotoAgente = user.FirstOrDefault(u => u.Id == p.IdUser).PhotoProfileUrl,
                EmailAgente = user.FirstOrDefault(u => u.Id == p.IdUser).Email,
                Upgrades = p.Upgrades.Select(up => upgradesP.FirstOrDefault(u => u.Id == up.IdUpgrade)?.Name)
                                .Where(name => !string.IsNullOrEmpty(name)).ToList()

            }).ToList();

        }

        public async Task RemoveAsync(string id)
        {
            var entity = await _propertyRepository.GetByIdAsync(id);
            await _propertyRepository.RemoveAsync(entity);
        }

        public async Task<SavePropertyViewModel> GetByIdAsync(string id)
        {
            var entity = await _propertyRepository.GetByIdAsync(id);
            return _mapper.Map<SavePropertyViewModel>(entity);
        }

        public virtual async Task<SavePropertyViewModel> UpdateAsync(SavePropertyViewModel vm, string id)
        {
            var entity = _mapper.Map<Property>(vm);
            var t = await _propertyRepository.UpdateAsync(entity, id);
            return _mapper.Map<SavePropertyViewModel>(t);
        }
    }
}
