using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Application.ViewModels.Users;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateAppProg3.Presentation.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [Authorize(Roles = "Admin,Developer")]
    [SwaggerTag("Controlador de agente")]
    public class AgentController : BaseApiController
    {
        private readonly IUserService _userService;
        private readonly IPropertyService _propertyService;
        public AgentController(IUserService userService, IPropertyService propertyService)
        {
            _userService = userService;
            _propertyService = propertyService;
        }

        [HttpGet("List")]
        [SwaggerOperation(
            Summary = "Obtener los usuarios agente",
            Description = "Un listado con todos los usuarios con el rol de agente")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaveUserViewModel>))]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> List()
        {
            try
            {
                var users = await _userService.GetUsersByRole("Agent");
                if (users == null)
                {
                    return NoContent();
                }
                return Ok(users);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [HttpGet("GetById")]
        [SwaggerOperation(
            Summary = "Obtener un agente por ID",
            Description = "Un agente mediante un ID")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveUserViewModel))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetById(string Id)
        {
            try
            {
                var user = await _userService.GetByIdWithoutRol(Id);
                if (user.TypeUser != "Agent")
                {
                    return NoContent();
                }
                return Ok(user);
            }
            catch (Exception)
            {
                return NoContent();
            }

        }



        [Authorize(Roles = "Admin")]
        [HttpGet("ChangeStatus")]
        [SwaggerOperation(
         Summary = "Cambio de estado",
         Description = "Cambio de estado para un usuario por Id"
         )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> ChangeStatus(string Code)
        {
            try
            {
                var user = await _userService.GetByIdWithoutRol(Code);
                if (user.TypeUser != "Agent")
                {
                    throw new Exception("usuario invalido");
                }
                user.IsActive = !user.IsActive;
                await _userService.UpdateAsync(user);
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
                throw;
            }
        }
        [HttpGet("GetAgentProperties")]
        [SwaggerOperation(
            Summary = "Obtener las propiedades de un agente",
            Description = "Obtener las propiedades")]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<SaveUserViewModel>))]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAgentProperties(string Id)
        {
            try
            {
                var properties = await _propertyService.GetAllPropertiesByAgentId(Id);
                if (properties == null)
                {
                    return NoContent();
                }
                return Ok(properties);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

    }
}
