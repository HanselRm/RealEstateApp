using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Exceptions;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateAppProg3.Presentation.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    [SwaggerTag("Controlador de mejoras")]
    public class UpgradeController : Controller
    {
        private readonly IUpgradeService _upgradeService;
        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("List")]
        [SwaggerOperation(
           Summary = "Obtener mejoras",
           Description = "Trae un listado de mejoras"
           )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<UpgradeViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> List()
        {
            try
            {
                var types = await _upgradeService.GetAllAsync();
                if (types == null)
                {
                    return NoContent();
                }
                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("GetById")]
        [SwaggerOperation(
            Summary = "Obtener una mejora",
            Description = "Trae una mejora por Id"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveUpgradesViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var types = await _upgradeService.GetByIdAsync(Id);
                if (types == null)
                {
                    return NoContent();
                }
                return Ok(types);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }


        [Authorize(Roles = "Admin")]
        [HttpPost("Create")]
        [SwaggerOperation(
            Summary = "Crear una mejora",
            Description = "Crea una mejora"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SaveUpgradesViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiException("Campos invalidos."));
                }
                //guardado 
                await _upgradeService.SaveAsync(vm);
                return StatusCode(StatusCodes.Status201Created);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }



        [Authorize(Roles = "Admin")]
        [HttpPut("Update")]
        [SwaggerOperation(
            Summary = "Actualizar una mejora",
            Description = "Actualizar una mejora mediante el Id"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveUpgradesViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] SaveUpgradesViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiException("Campos invalidos."));
                }
                //guardado 
                var user = await _upgradeService.UpdateAsync(vm, vm.Id);
                return Ok(user);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("Delete")]
        [SwaggerOperation(
        Summary = "Remover una mejora",
        Description = "Remover una mejora"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {

                await _upgradeService.RemoveAsync(Id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
 