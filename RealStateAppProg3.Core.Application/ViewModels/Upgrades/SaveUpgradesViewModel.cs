

using Swashbuckle.AspNetCore.Annotations;
using System.ComponentModel.DataAnnotations;

namespace RealStateAppProg3.Core.Application.ViewModels.Upgrades
{
    public class SaveUpgradesViewModel
    {
        [SwaggerParameter("Id de la mejora")]
        public int Id { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [SwaggerParameter("Descripcion")]

        public string Description { get; set; }
        [Required]
        [DataType(DataType.Text)]
        [SwaggerParameter("Nombre")]
        public string Name { get; set; }
    }
}
