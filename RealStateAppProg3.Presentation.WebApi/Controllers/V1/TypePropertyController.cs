using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Exceptions;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateAppProg3.Presentation.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TypePropertyController : BaseApiController
    {
        private readonly ITypePropertyService _typePropertyService;
        public TypePropertyController(ITypePropertyService typePropertyService)
        {
            _typePropertyService = typePropertyService;
        }


        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("List")]
        [SwaggerOperation(
            Summary = "Obtener tipo de propiedades",
            Description = "Trae un listado de propiedades"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(List<TypePropertyViewModel>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> List()
        {
            try
            {
                var types = await _typePropertyService.GetAllAsync();
                if(types == null)
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
            Summary = "Obtener una propiedad",
            Description = "Trae una propiedad por Id"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypePropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var types = await _typePropertyService.GetByIdAsync(Id);
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
            Summary = "Obtener una propiedad",
            Description = "Trae una propiedad por Id"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SaveTypePropertyViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiException("Campos invalidos."));
                }
                //guardado 
                await _typePropertyService.SaveAsync(vm);
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
            Summary = "Actualizar una propiedad",
            Description = "Actualizar la propiedad mediante el Id"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypePropertyViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] SaveTypePropertyViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiException("Campos invalidos."));
                }
                //guardado 
                var user = await _typePropertyService.UpdateAsync(vm,vm.Id);
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
        Summary = "Remover un tipo de propiedad",
        Description = "Remover un tipo de propiedad"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {
               
                await _typePropertyService.RemoveAsync(Id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
