using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.Users;
using System.Data;
using NuGet.Protocol.Plugins;
using NuGet.Common;
using RealStateAppProg3.Presentation.WebApp.MiddledWares;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;

        public UserController(IUserService userService, IHttpContextAccessor contextAccessor)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
        }

        //pagina de login
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        //recibe el login
        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Index(LoginViewModel login)
        {
            if (!ModelState.IsValid)
            {
                return View(login);
            }
            AuthenticationResponse response = await _userService.LoginAsync(login);
            if (response != null && response.HasError != true)
            {

                HttpContext.Session.Set<AuthenticationResponse>("user", response);
                if (response.Roles.Contains("Admin"))
                {
                    return RedirectToRoute(new { controller = "Admin", action = "Dashboard" });
                }
                else if (response.Roles.Contains("Agent"))
                {
                    return RedirectToRoute(new { controller = "Agent", action = "Index" });
                }
                else if (response.Roles.Contains("Client"))
                {
                    return RedirectToRoute(new { controller = "Client", action = "Home" });
                }
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }
            else
            {
                login.HasError = response.HasError;
                login.Error = response.Error;
                return View(login);
            }
        }

        //LogOut metodo
        public async Task<IActionResult> LogOut()
        {
            await _userService.SignOutAsync();
            HttpContext.Session.Remove("user");
            return RedirectToRoute(new { controller = "Home", action = "Index" });
        }

        //crear usuario
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Register() 
        {
            return View(new SaveUserViewModel());
        }

        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> Register(SaveUserViewModel vm)
        {
            if (ModelState["Name"].Errors.Any() || ModelState["LastName"].Errors.Any() || ModelState["Identification"].Errors.Any() 
                || ModelState["PhoneNumber"].Errors.Any() || ModelState["file"].Errors.Any() || ModelState["PhoneNumber"].Errors.Any()
                || ModelState["Username"].Errors.Any() || ModelState["Email"].Errors.Any() || ModelState["Password"].Errors.Any() 
                || ModelState["ConfirmPassword"].Errors.Any() || ModelState["TypeUser"].Errors.Any())
            {
                return View(vm);
            }
            var origin = Request.Headers["origin"];
            vm.PhotoProfileUrl = UploadFiles.UploadFile(vm.file, "User", vm.Id);

            SaveUserViewModel response = await _userService.RegisterAsync(vm, origin);

            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);

            }

        }

        //ConfirmEmail method
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ConfirmEmail(string userId, string token)
        {
            string response = await _userService.confirmEmailAsync(userId, token);
            return View("ConfirmEmail", response);
        }
        //Recuperar contraseña
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> PasswordRecover()
        {
            return View(new ForgotPasswordViewModel());

        }

        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> PasswordRecover(ForgotPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            var origin = Request.Headers["origin"];

            ForgotPassWordResponse response = await _userService.ForgotPassWordAsync(vm, origin);

            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
        }
        //resetear contraseña
        [ServiceFilter(typeof(LoginAuthorize))]
        public IActionResult ResetPassword(ResetPasswordViewModel vm)
        {

            return View(vm);
        }
        [HttpPost]
        [ServiceFilter(typeof(LoginAuthorize))]
        public async Task<IActionResult> ResetPasswordd(ResetPasswordViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View(vm);
            }


            ResetPasswordResponse response = await _userService.ResetPassWordAsync(vm);

            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "User", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);
            }
        }

        public async Task<IActionResult> AccesDenied()
        {
            return View();
        }
    }
}
