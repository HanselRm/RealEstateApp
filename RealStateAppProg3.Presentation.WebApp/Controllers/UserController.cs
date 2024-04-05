using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class UserController : Controller
    {
        //pagina de login
        public IActionResult Index()
        {
            return View(new LoginViewModel());
        }
    }
}
