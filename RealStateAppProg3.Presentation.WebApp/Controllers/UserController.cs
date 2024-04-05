using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.Users;
using System.Data;

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
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }

        //recibe el login
        [HttpPost]
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
                return RedirectToRoute(new { controller = "Home", action = "Index" });

            }
            else
            {
                login.HasError = response.HasError;
                login.Error = response.Error;
                return View(login);
            }
        }
    }
}
