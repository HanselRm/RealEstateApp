using Azure;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Dtos.Email;
using RealStateAppProg3.Core.Application.Enums;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Users;
using RealStateAppProg3.Infrastructure.Identity.Models;
using System.Data;
using System.Runtime.Intrinsics.Arm;
using System.Text;

namespace RealStateAppProg3.Infrastructure.Identity.Services
{
    public class AccountService : IAccountService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailServices _emailService;

        public AccountService(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, IEmailServices emailServices)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailServices;
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
            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = "Credenciales incorrectas";
                return response;
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

        //registrar user
        public async Task<SaveUserViewModel> RegisterAsync(SaveUserViewModel vm, string origin)
        {
            SaveUserViewModel userVM = new SaveUserViewModel();
            var verifUsername = await _userManager.FindByNameAsync(vm.Username);
            if (verifUsername != null)
            {
                userVM.HasError = true;
                userVM.Error = "Este usuario ya esta en uso";
                return userVM;
            }

            //valida que la cedula sea unica
            var user = await _userManager.Users.ToListAsync();
            var verifyCedula = user.FirstOrDefault(user => user.Identification == vm.Identification);
            if (verifyCedula != null)
            {
                userVM.HasError = true;
                userVM.Error = $"Esta cedula ya esta en uso";
                return userVM;
            }
            //valida el email
            var verifyEmail = await _userManager.FindByEmailAsync(vm.Email);
            if (verifyEmail != null)
            {
                userVM.HasError = true;
                userVM.Error = $"Este email {userVM.Email} ya esta en uso";
                return userVM;
            }

            ApplicationUser ApUser = new()
            {
                Name = vm.Name,
                LastName = vm.LastName,
                Email = vm.Email,
                Identification = vm.Identification,
                UserName = vm.Username,
                PhoneNumber = vm.PhoneNumber,
                PhoneNumberConfirmed = true,
                ImgUser = vm.PhotoProfileUrl,
            };

            if (vm.TypeUser == RoleENum.Agent.ToString())
            {
                ApUser.IsActive = false;
                ApUser.EmailConfirmed = true;
            }

            if (vm.TypeUser == RoleENum.Client.ToString())
            {
                ApUser.EmailConfirmed = false;
                ApUser.IsActive = false;
            }
            
            if (vm.TypeUser == RoleENum.Admin.ToString())
            {
                ApUser.IsActive = true;
                ApUser.EmailConfirmed = true;
            }

            var status = await _userManager.CreateAsync(ApUser, vm.Password);

            if (status.Succeeded)
            {
                await _userManager.AddToRoleAsync(ApUser, vm.TypeUser);
                //si el usuario es Cliente le envia un email
                if (vm.TypeUser == RoleENum.Client.ToString())
                {
                    var verificationUri = await SendVerificationEmail(ApUser, origin);
                    await _emailService.sendAsync(new EmailRequest()
                    {
                        To = ApUser.Email,
                        Body = $"Confirme su cuenta en este link: {verificationUri}",
                        Subject = "Confirmacion de correo"
                    });
                }
                
                userVM.HasError = false;

            }
            else
            {
                userVM.HasError = true;
                userVM.Error = $"Hubo un error, intentelo mas tarde";
                return userVM;
            }
            return userVM;
        }

        //Actualizar usuario
        public async Task<SaveUserViewModel> UpdateAsync(SaveUserViewModel vm)
        {
            SaveUserViewModel userVM = new SaveUserViewModel();

            var AppUser = await _userManager.FindByIdAsync(vm.Id);

/*            if(AppUser.UserName != vm.Username)
            {
                var verifUsername = await _userManager.FindByNameAsync(vm.Username);
                if (verifUsername != null)
                {
                    userVM.HasError = true;
                    userVM.Error = "Este usuario ya esta en uso";
                    return userVM;
                }
            }

          if(AppUser.Email != vm.Email)
            {
                //valida el email

                var verifyEmail = await _userManager.FindByEmailAsync(vm.Email);
                if (verifyEmail != null)
                {
                    userVM.HasError = true;
                    userVM.Error = $"Este email {userVM.Email} ya esta en uso";
                    return userVM;
                }
            }*/


            AppUser.Name = vm.Name;
            AppUser.LastName = vm.LastName;
            AppUser.Email = vm.Email;
            AppUser.Identification = vm.Identification;
            AppUser.UserName = vm.Username;
            AppUser.PhoneNumber = vm.PhoneNumber;
            AppUser.PhoneNumberConfirmed = true;
            AppUser.ImgUser = vm.PhotoProfileUrl;
            

            var status = await _userManager.UpdateAsync(AppUser);

            if (status.Succeeded)
            {
                userVM.HasError = false;

            }
            else
            {
                userVM.HasError = true;
                userVM.Error = $"Hubo un error, intentelo mas tarde";
                return userVM;
            }
            return userVM;
        }

        //get UserVm por Id
        public async Task<SaveUserViewModel> GetByIdWithoutRol(string id)
        {

            var user = await _userManager.FindByIdAsync(id);
            if(user == null)
            {
                throw new Exception("No se encontro este usuario");
            }

            SaveUserViewModel vm = new SaveUserViewModel
            {
                Name = user.Name,
                Username = user.UserName,
                Identification = user.Identification,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PhotoProfileUrl = user.ImgUser,
                Email = user.Email,
                Id = user.Id
            };
            return vm;
        }

        //Olvidar contraseña
        public async Task<ForgotPassWordResponse> ForgotPasswordRequestAsync(ForgotPassowordRequest request, string origin)
        {
            ForgotPassWordResponse response = new()
            {
                HasError = false
            };

            var account = await _userManager.FindByEmailAsync(request.Email);
            if(account == null)
            {
                response.HasError = true;
                response.Error = $"Este email {request.Email} esta cuenta no existe";
                return response;
            }
            else
            {
                var verificationUri = await SendForgotPasswordUrl(account, origin);
                await _emailService.sendAsync(new EmailRequest()
                {
                    To = request.Email,
                    Body = $"Por favor resetea la contraseña aqui: {verificationUri}",
                    Subject = "resetear password"
                });
                return response;
            }
        }

        //envia la contra verification
        private async Task<string> SendForgotPasswordUrl(ApplicationUser user, string origin)
        {
            var code = await _userManager.GeneratePasswordResetTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));
            var route = "User/ResetPassword";
            var Uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(Uri.ToString(), "Token", code);

            return verificationUrl;
        }
        //Resetear contra
        public async Task<ResetPasswordResponse> ResetPasswordAsync(ResetPasswordRequest request)
        {
            ResetPasswordResponse response = new();
            response.HasError = false;

            var account = await _userManager.FindByEmailAsync(request.Email);

            if (account == null)
            {
                response.HasError = true;
                response.Error = $"La cuenta {request.Email} no se encuentra";
                return response;
            }

            request.Token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(request.Token));

            var result = await _userManager.ResetPasswordAsync(account, request.Token, request.Password);
            if (!result.Succeeded)
            {
                response.HasError = true;
                response.Error = $"Ha ocurrido un error, intentelo mas tarde";
                return response;

            }
            return response;
        }

        //Confirmar cuenta
        public async Task<string> ConfirmAccount(string userId, string token)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if(user == null)
            {
                return $"No existe una cuenta registrada con ese user: {userId}";
            }

            token = Encoding.UTF8.GetString(WebEncoders.Base64UrlDecode(token));
            var result = await _userManager.ConfirmEmailAsync(user, token);
            if (result.Succeeded)
            {
                user.IsActive = true;
                await _userManager.UpdateAsync(user);
                return $"Cuenta verificada para {user.Email}";
            }
            else
            {
                return $"Ocurrio un error confirmando la cuenta {user.Email}";
            }
        }
        //metodo de verificar email
        public async Task<string> SendVerificationEmail(ApplicationUser user, string origin)
        {
            var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            code = WebEncoders.Base64UrlEncode(Encoding.UTF8.GetBytes(code));

            var route = "User/ConfirmEmail";
                
            var uri = new Uri(string.Concat($"{origin}/", route));
            var verificationUrl = QueryHelpers.AddQueryString(uri.ToString(), "userId", user.Id);
            verificationUrl = QueryHelpers.AddQueryString(verificationUrl, "token", code);

            return verificationUrl;
        }
        //obtener los usuarios con el rol administrador
        public async Task<List<SaveUserViewModel>> GetUsersAdmin()
        {
           var users = await _userManager.GetUsersInRoleAsync(RoleENum.Admin.ToString());
            //

            return users.Select(user => new SaveUserViewModel
            {
                Name = user.Name,
                Username = user.UserName,
                Identification = user.Identification,
                LastName = user.LastName,
                PhoneNumber = user.PhoneNumber,
                PhotoProfileUrl = user.ImgUser,
                Email = user.Email,
                Id = user.Id
            }).ToList();
        }
    }

}
