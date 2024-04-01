using Microsoft.AspNetCore.Identity;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Infrastructure.Identity.Models;

namespace RealStateAppProg3.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }

        //Metodo login
        public async Task<AuthenticationResponse> AuthenticateAsync(AuthenticationRequest request)
        {
            AuthenticationResponse response = new();

            //Busca el user por email
            var user = await _userManager.FindByEmailAsync(request.UserName);
            if (user == null)
            {
                //si no encuentra por email 
                //Busca por nombre de usuario
                user = await _userManager.FindByNameAsync(request.UserName);
            }

            if (user == null)
            {
                response.HasError = true;
                response.Error = $"No existe una cuenta registrada con {request.UserName}";
                return response;
            }

            //inicia seccion
            var result = await _signInManager.PasswordSignInAsync(request.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Credenciales incorrectas";
            }
            

            //valida si el email es confirm
            if (!user.EmailConfirmed)
            {
                response.HasError = true;
                response.Error = $"Su cuenta no está activa, comuniquese con el Admin";
                return response;
            }
            //Valida si el user esta activo
            if (!user.IsActive)
            {
                response.HasError = true;
                response.Error = $"Comuniquese con su administrador para acceder a los permisos de agente.";
                return response;
            }

            //mapeo de user a response
            response.Id = user.Id;
            response.Email = user.Email;
            response.UserName = user.UserName;
                
            var rolesList = await _userManager.GetRolesAsync(user).ConfigureAwait(false);
            response.Roles = rolesList.ToList();
            response.IsVerified = user.EmailConfirmed;
            return response;
        }

        //cerrar session
        public async Task SignOutAsync()
        {
            await _signInManager.SignOutAsync();
        }
    }

}
