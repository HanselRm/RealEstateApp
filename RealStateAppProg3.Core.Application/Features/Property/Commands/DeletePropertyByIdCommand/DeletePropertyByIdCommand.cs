
using AutoMapper;
using MediatR;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;

namespace RealStateAppProg3.Core.Application.Features.Property.Commands.DeletePropertyByIdCommand
{
    public class DeletePropertyByIdCommand : IRequest<string>
    {
        public string Code { get; set; }
    }

    public class DeletePropertyByIdCommandHandler : IRequestHandler<DeletePropertyByIdCommand, string>
    {
        private readonly IMapper _mapper;
        private readonly IPropertyRepository _propertyRepository;
        public DeletePropertyByIdCommandHandler(IMapper mapper, IPropertyRepository propertyRepository)
        {
            _mapper = mapper;
            _propertyRepository = propertyRepository;
        }

        public async Task<string> Handle(DeletePropertyByIdCommand request, CancellationToken cancellationToken)
        {
            var property = await _propertyRepository.GetByIdAsync(int.Parse(request.Code));
            if (property == null) throw new Exception("Propiedad no encontrada.");
            await _propertyRepository.RemoveAsync(property);
            return request.Code;
        }
    }
}
