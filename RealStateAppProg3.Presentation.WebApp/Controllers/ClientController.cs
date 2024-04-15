using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.PropertyFav;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class ClientController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPropertyFavService _propertyFavService;
        public ClientController(IUserService userService, IHttpContextAccessor contextAccessor, IPropertyService propertyService,
                                IPropertyFavService propertyFavService)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
            _propertyService = propertyService;
            _propertyFavService = propertyFavService;
        }
        public async Task<IActionResult> Home()
        {
            return View(await _propertyService.GetAllAsync());
        }

        public async Task<IActionResult> addFVProperty(string code, string UserId)
        {
            SavePropertyFavViewModel vm = new SavePropertyFavViewModel
            {
                IdProperty = code,
                IdUser = UserId
            };
            await _propertyFavService.SaveAsync(vm);

            return View("Home", await _propertyService.GetAllAsync());
        }
    }
}
