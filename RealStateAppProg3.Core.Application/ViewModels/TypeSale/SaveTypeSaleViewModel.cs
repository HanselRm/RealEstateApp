
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateAppProg3.Core.Application.ViewModels.TypeSale
{
    public class SaveTypeSaleViewModel
    {
        [SwaggerParameter("Id")]
        public int Id {  get; set; }
        [SwaggerParameter("Descripcion")]
        public string Description {  get; set; }
        [SwaggerParameter("Nombre")]
        public string Name { get; set; }
    }
}
