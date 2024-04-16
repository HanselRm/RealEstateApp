using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Exceptions;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateAppProg3.Presentation.WebApi.Controllers.V1
{
    [ApiVersion("1.0")]
    public class TypeSaleController : BaseApiController
    {
        private readonly ITypeSaleService _typeSaleService;
        public TypeSaleController(ITypeSaleService typeSaleService)
        {
            _typeSaleService = typeSaleService;
        }

        [Authorize(Roles = "Admin,Developer")]
        [HttpGet("List")]
        [SwaggerOperation(
            Summary = "Obtener tipos de ventas",
            Description = "Trae un listado de tipos de ventas"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(List<TypeSaleViewModel>))]
        public async Task<IActionResult> ListTypeSale()
        {
            try
            {
                var types = await _typeSaleService.GetAllAsync();
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
            Summary = "Obtener un tipo de venta",
            Description = "Trae un tipo de venta por Id"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeSaleViewModel))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> GetById(int Id)
        {
            try
            {
                var types = await _typeSaleService.GetByIdAsync(Id);
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
            Summary = "Crear un tipo de venta",
            Description = "Crea un tipo de venta"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Create([FromBody] SaveTypeSaleViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiException("Campos invalidos."));
                }
                //guardado 
                await _typeSaleService.SaveAsync(vm);
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
            Summary = "Actualizar un tipo de venta",
            Description = "Actualizar un tipo de venta mediante el Id"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(SaveTypeSaleViewModel))]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Update([FromBody] SaveTypeSaleViewModel vm)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new ApiException("Campos invalidos."));
                }
                //guardado 
                var user = await _typeSaleService.UpdateAsync(vm, vm.Id);
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
        Summary = "Remover un tipo de venta",
        Description = "Remover un tipo de venta"
        )]
        [Consumes(MediaTypeNames.Application.Json)]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Delete(int Id)
        {
            try
            {

                await _typeSaleService.RemoveAsync(Id);
                return StatusCode(StatusCodes.Status204NoContent);

            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Message);
            }
        }
    }
}
