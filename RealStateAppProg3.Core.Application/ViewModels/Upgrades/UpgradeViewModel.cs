

using RealStateAppProg3.Core.Domain.Entities;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Upgrades
{
    public class UpgradeViewModel
    {

        [SwaggerParameter("Id de la mejora")]
        public int Id { get; set; }
        [SwaggerParameter("Descripcion")]
        public string Description { get; set; }

        [SwaggerParameter("Nomrbe")]
        public string Name { get; set; }
        [SwaggerParameter("Listado de propiedades")]
        public List<Property>? Properties {get; set;}
    }
}
