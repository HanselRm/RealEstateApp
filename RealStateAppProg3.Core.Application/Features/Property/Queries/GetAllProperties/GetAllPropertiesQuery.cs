
using AutoMapper;
using MediatR;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.ViewModels.Property;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Features.Property.Queries.GetAllProperties
{
    public class GetAllPropertiesQuery : IRequest<List<PropertyViewModel>>
    {
        public int? IdTypeProperty { get; set; }
        public decimal? MinPrice { get; set; }
        public decimal? MaxPrice { get; set; }
        public int? NumberRooms { get; set; }
        public int? Bathrooms { get; set; }
    }

    public class GetAllPropertyesQueryHandler : IRequestHandler<GetAllPropertiesQuery, List<PropertyViewModel>>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public GetAllPropertyesQueryHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<List<PropertyViewModel>> Handle(GetAllPropertiesQuery request, CancellationToken cancellationToken)
        {
            var parameters = _mapper.Map<GetAllPropertiesParameters>(request);
            var properties = await GetAllPropertiesWithFilterAsync(parameters);
            if (properties == null || properties.Count == 0) throw new Exception("No existen propiedades");
            return properties;
        }

        private async Task<List<PropertyViewModel>> GetAllPropertiesWithFilterAsync(GetAllPropertiesParameters filters)
        {
            var propertiesList = await _propertyRepository.GetAllWithInclude(new List<string> 
            { "TypeProperty", "TypeSale","propertyFavs" });
            //realizando un mapping a propertyviewmodel
            var propertiesViewModel = _mapper.Map<List<PropertyViewModel>>(propertiesList);
            //filtrando 
            //filtor para el tipo de propiedad
            if(filters.IdTypeProperty != null)
            {
                propertiesViewModel = propertiesViewModel.Where(x => x.IdTypeProperty == filters.IdTypeProperty).ToList();
            }
            //filtro para el numero de habitaciones 
            if(filters.NumberRooms != null)
            {
                propertiesViewModel = propertiesViewModel.Where(x => x.NumberRooms== filters.NumberRooms).ToList();
            }
            //filtro para los ba;os 
            if(filters.Bathrooms != null)
            {
                propertiesViewModel = propertiesViewModel.Where(x => x.Bathrooms == filters.Bathrooms).ToList();
            }
            //filtro para los precios
            if(filters.MinPrice != null)
            {
                propertiesViewModel = propertiesViewModel.Where(x => x.Value > filters.MinPrice).ToList();
            }
            if(filters.MaxPrice != null) 
            {
                propertiesViewModel = propertiesViewModel.Where(x => x.Value < filters.MaxPrice).ToList();
            }
            //retornando la lista
            return propertiesViewModel;
        }
    }
}
