
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Features.Property.Commands.CreateProperty
{
    public class CreatePropertyCommand : IRequest<string>
    {
        public string Code { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public int NumberRooms { get; set; }
        public int SizeInMeters { get; set; }
        public int Bathrooms { get; set; }
        public int IdTypeProperty { get; set; }
        public int IdTypeSale { get; set; }
        public string UrlImage1 { get; set; }
        public string? UrlImage2 { get; set; }
        public string? UrlImage3 { get; set; }
        public string? UrlImage4 { get; set; }
        public string IdUser { get; set; }
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
