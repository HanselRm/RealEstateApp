
using AutoMapper;
using MediatR;
using Swashbuckle.AspNetCore.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Domain.Entities;
using System.ComponentModel;

namespace RealStateAppProg3.Core.Application.Features.Property.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<string>
    {
        public string Code { get; set; }

        [SwaggerParameter(Description = "Una descripcion para la propiedad")]
        public string Description { get; set; }

        [SwaggerParameter(Description = "Valor de la propiedad")]
        public decimal Value { get; set; }
        [SwaggerParameter(Description = "Numero de habitaciones")]
        public int NumberRooms { get; set; }
        [SwaggerParameter(Description = "Tamaño de la propiedad en metros")]
        public int SizeInMeters { get; set; }
        [SwaggerParameter(Description = "Numero de baños")]
        public int Bathrooms { get; set; }
        [SwaggerParameter(Description = "Id tipo de propiedad")]
        [DefaultValue("0")]
        public int IdTypeProperty { get; set; }
        [SwaggerParameter(Description = "Id tipo de venta")]
        [DefaultValue("0")]
        public int IdTypeSale { get; set; }
        [SwaggerParameter(Description = "Url de la imagen 1")]
        public string UrlImage1 { get; set; }
        [SwaggerParameter(Description = "Url de la imagen 2")]
        public string? UrlImage2 { get; set; }
        [SwaggerParameter(Description = "Url de la imagen 3")]
        public string? UrlImage3 { get; set; }
        [SwaggerParameter(Description = "Url de la imagen 4")]
        public string? UrlImage4 { get; set; }
        [SwaggerParameter(Description = "Id del usuario")]
        [DefaultValue("0")]
        public string IdUser { get; set; }

        [SwaggerParameter("Lista de mejoras")]
        public List<SaveUpgradePropertyViewModel> Upgrades {  get; set; }
    }
    public class CreatePropertyCommandHandler : IRequestHandler<CreatePropertyCommand, string>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IUpgradePropertyService _upradePropertyService;
        private readonly IMapper _mapper;
        public CreatePropertyCommandHandler(IPropertyRepository propertyRepository, 
            IMapper mapper,
            IUpgradePropertyService upgradePropertyService)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
            _upradePropertyService = upgradePropertyService;
        }

        public async Task<string> Handle(CreatePropertyCommand request, CancellationToken cancellationToken)
        {
            var property = _mapper.Map<Domain.Entities.Property>(request);
            property = await _propertyRepository.SaveAsync(property);
            foreach(var item in request.Upgrades)
            {
                item.IdProperty = int.Parse(property.Code);
                await _upradePropertyService.SaveAsync(item);
            }
            return property.Code;
        }
    }
}
