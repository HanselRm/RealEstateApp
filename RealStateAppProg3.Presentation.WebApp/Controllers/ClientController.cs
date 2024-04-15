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
            ViewBag.PropertyFav = await _propertyFavService.GetAllAsync();
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
            ViewBag.PropertyFav = await _propertyFavService.GetAllAsync();
            return View("Home", await _propertyService.GetAllAsync());
        }

        public async Task<IActionResult> RemoveFromFavorites(string code, string UserId)
        {
            var list = await _propertyFavService.GetAllAsync();
            
            PropertyFavViewModel vm = list.FirstOrDefault( x => x.IdUser == UserId && x.IdProperty == code);
            await _propertyFavService.RemoveAsync(vm.Id);
            ViewBag.PropertyFav = await _propertyFavService.GetAllAsync();
            return View("Home", await _propertyService.GetAllAsync());
        }
        
    }
}
