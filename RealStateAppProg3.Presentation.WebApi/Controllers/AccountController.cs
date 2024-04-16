using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Enums;
using RealStateAppProg3.Core.Application.Exceptions;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Users;
using Swashbuckle.AspNetCore.Annotations;
using System.Net.Mime;

namespace RealStateAppProg3.Presentation.WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [SwaggerTag("Autenticacion")]
    public class AccountController : ControllerBase 
    {
        private readonly IAccountService _userService;
        public AccountController(IAccountService userService)
        {
            _userService = userService;
        }

        [HttpPost("Authenticate")]
        [SwaggerOperation
            (
                Summary = "Login",
                Description = "Autenticacion de usuario y en caso de que sus credenciales sean validas retorna un JWT"
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> Authenticate(AuthenticationRequest request)
        {
            var response = await _userService.GetRolesByUsername(request.UserName);
            if(response == null)
            {
                return BadRequest(new ApiException("No existe dicho nombre de usuario"));
            }
            if (response.Contains("Client"))
            {
                return BadRequest(new ApiException("No tienes acceso a esta funcionalidad"));
            }
            return Ok(await _userService.AuthenticateAsync(request));
        }


        [Authorize(Roles = "Admin,Developer")]
        [HttpPost("RegisterDev")]
        [SwaggerOperation
            (
                Summary = "Registro de desarrolladores",
                Description = "Registro de usuarios con rol desarrollador."
            )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterDev(SaveUserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiException("Campos invalidos"));
            }
            //modificando para que el rol sea desarrollador
            userVm.TypeUser = RoleENum.Developer.ToString();
            //-----
            var origin = Request.Headers["origin"];
            userVm.PhotoProfileUrl = UploadFiles.UploadFile(userVm.file,"User", origin);
            await _userService.RegisterAsync(userVm, "user");
            return StatusCode(201);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("RegisterAdmin")]
        [SwaggerOperation
       (
           Summary = "Registro de desarrolladores",
           Description = "Registro de usuarios con rol desarrollador."
       )]
        [Consumes(MediaTypeNames.Application.Json)]
        public async Task<IActionResult> RegisterAdmin(SaveUserViewModel userVm)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new ApiException("Campos invalidos"));
            }
            //modificando para que el rol sea desarrollador
            userVm.TypeUser = RoleENum.Admin.ToString();
            //-----
            var origin = Request.Headers["origin"];
            userVm.PhotoProfileUrl = UploadFiles.UploadFile(userVm.file, "User", origin);
            await _userService.RegisterAsync(userVm, "user");
            return StatusCode(201);
        }
    }
}
