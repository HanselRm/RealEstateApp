using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Features.Property.Queries.GetAllProperties;
using RealStateAppProg3.Core.Application.Features.Property.Queries.GetPropertyById;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using Swashbuckle.AspNetCore.Annotations;
using System.Diagnostics;
using System.Net.Mime;

namespace RealStateAppProg3.Presentation.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Developer")]
    public class PropertyController : BaseApiController
    {
        

        [HttpGet("List")]
        [SwaggerOperation(
            Summary = "Listado de propiedades",
            Description = "Aqui se obtienen un listado de todas las propiedades")
            ]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> List()
        {
            var properties = await Mediator.Send(new GetAllPropertiesQuery());
            if(properties == null)
            {
                return NoContent();
            }
            return Ok(properties);
        }

        [HttpDelete("GetById")]
        [SwaggerOperation(
            Summary = "Propiedad por id",
            Description = "Obtener una propiedad por ID")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(PropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(int id)
        {
            try
            {
                var property = await Mediator.Send(new GetPropertyByIdQuery { Id = id });
                if (property == null)
                {
                    return NoContent();
                }
                return Ok(property);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        
        }
    }
}
