
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateAppProg3.Core.Application.ViewModels.TypeProperty
{
    public class SaveTypePropertyViewModel
    {

        [SwaggerParameter("Id del tipo de propiedad")]
        public int Id { get; set; }
        [SwaggerParameter("Descripcion")]
        public string Description { get; set; }
        [SwaggerParameter("Nomrbe")]
        public string Name { get; set; }
    }
}
