using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.ViewModels.Users;
using Swashbuckle.AspNetCore.Annotations;

namespace RealStateAppProg3.Presentation.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TestController : BaseApiController
    {

        [HttpGet("List")]
        [SwaggerOperation(
          Summary = "Obtener los usuarios agente",
          Description = "Un listado con todos los usuarios con el rol de agente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaveUserViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Index()
        {
            return NoContent();
        }
    }
}
