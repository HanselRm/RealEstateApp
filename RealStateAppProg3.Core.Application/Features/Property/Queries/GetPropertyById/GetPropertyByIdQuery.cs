using MediatR;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RealStateAppProg3.Core.Application.Features.Property.Queries.GetPropertyById
{
    public class GetPropertyByIdQuery : IRequest<SavePropertyViewModel>
    {
        [SwaggerParameter("Codigo de la propiedad")]
        public int Id { get; set; }
    }

    public class PropertyByIdQueryHandler : IRequestHandler<GetPropertyByIdQuery, SavePropertyViewModel>
    {
        private readonly IPropertyService _propertyService;
        public PropertyByIdQueryHandler(IPropertyService propertyService)
        {
            _propertyService = propertyService;
        }

        public async Task<SavePropertyViewModel> Handle(GetPropertyByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var user = await _propertyService.GetByIdAsync(request.Id);
                return user;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}
