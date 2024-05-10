
using AutoMapper;
using InternetBanking.Core.Application.Services;
using Microsoft.AspNetCore.Http;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.PropertyFav;
using RealStateAppProg3.Core.Domain.Entities;
using System.Security.Cryptography.X509Certificates;

namespace RealStateAppProg3.Core.Application.Services
{
    public class PropertyFavService : BaseService<PropertyFavViewModel, SavePropertyFavViewModel, PropertyFav>, IPropertyFavService
    {
        private readonly IPropertyFavRepository _propertyFavRepository;
        private readonly IMapper _mapper;
        private readonly IHttpContextAccessor _httpContextAccessor;
        public PropertyFavService(IPropertyFavRepository propertyFavRepository, IMapper mapper) : base
                (propertyFavRepository, mapper) 
        {
            _propertyFavRepository = propertyFavRepository;
            _mapper = mapper;
        }

        public override Task<SavePropertyFavViewModel> SaveAsync(SavePropertyFavViewModel vm)
        {
            //vm.IdUser = _httpContextAccessor.HttpContext.Session.Get("User");
            return base.SaveAsync(vm);
        }

        public async Task<List<PropertyFavViewModel>> GetAllByUserAsync(string user)
        {
            var entities = await _propertyFavRepository.GetAllAsync();
            var list = entities.Where(x => x.IdUser == user).ToList();
            return _mapper.Map<List<PropertyFavViewModel>>(list);
        }
    }
}
