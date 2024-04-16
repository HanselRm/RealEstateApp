

using RealStateAppProg3.Core.Application.ViewModels.UpgradeProperty;
using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel;

namespace RealStateAppProg3.Core.Application.Features.Property.Commands.UpdatePropertyByIdCommand
{
    public class UpdatePropertyResponse
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
        public List<SaveUpgradePropertyViewModel> Upgrades { get; set; }
    }
}
