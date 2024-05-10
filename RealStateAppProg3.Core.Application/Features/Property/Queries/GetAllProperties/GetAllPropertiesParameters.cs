

using Swashbuckle.AspNetCore.Annotations;

namespace RealStateAppProg3.Core.Application.Features.Property.Queries.GetAllProperties
{
    public class GetAllPropertiesParameters
    {
        [SwaggerParameter("Id del tipo de propiedad")]
        public int? IdTypeProperty { get; set; }
        [SwaggerParameter("Precio minimo")]
        public decimal? MinPrice {  get; set; }
        [SwaggerParameter("Precio maximo")]
        public decimal? MaxPrice { get; set; }
        [SwaggerParameter("Numero de habitaciones")]
        public int? NumberRooms { get; set; }
        [SwaggerParameter("Precio de baños")]
        public int? Bathrooms {  get; set; }
    }
}
