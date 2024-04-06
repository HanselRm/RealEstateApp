
using AutoMapper;
using MediatR;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Core.Application.Features.Property.Commands.UpdatePropertyByIdCommand
{
    public class UpdatePropertyByIdCommand : IRequest<UpdatePropertyResponse>
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
    }

    public class UpdatePropertyByIdCommandHandler : IRequestHandler<UpdatePropertyByIdCommand, UpdatePropertyResponse>
    {
        private readonly IPropertyRepository _propertyRepository;
        private readonly IMapper _mapper;
        public UpdatePropertyByIdCommandHandler(IPropertyRepository propertyRepository, IMapper mapper)
        {
            _propertyRepository = propertyRepository;
            _mapper = mapper;
        }

        public async Task<UpdatePropertyResponse> Handle(UpdatePropertyByIdCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(int.Parse(request.Code));
            if (property == null) throw new Exception("Propiedad no encontrada");
            property = _mapper.Map<Domain.Entities.Property>(request);
            await _propertyRepository.UpdateAsync(property, int.Parse(property.Code));

            return _mapper.Map<UpdatePropertyResponse>(property);
        }
    }
}
