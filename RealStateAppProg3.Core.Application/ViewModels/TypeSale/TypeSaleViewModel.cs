

using Microsoft.EntityFrameworkCore.Metadata.Internal;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using Swashbuckle.AspNetCore.Annotations;


namespace RealStateAppProg3.Core.Application.ViewModels.TypeSale
{
    public class TypeSaleViewModel
    {
        [SwaggerParameter("Id del tipo de venta")]
        public int Id { get; set; }
        [SwaggerParameter("Descripcion")]
        public string Description { get; set; }

        [SwaggerParameter("Nomrbe")]
        public string Name { get; set; }
        [SwaggerParameter("Listado de propiedades")]
        public List<PropertyViewModel>? Properties { get; set; }
    }
}
