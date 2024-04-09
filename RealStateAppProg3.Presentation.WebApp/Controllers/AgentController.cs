using Azure;
using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Dtos.Account;
using RealStateAppProg3.Core.Application.Helpers;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class AgentController : Controller
    {
        private readonly IUserService _userService;
        private readonly IHttpContextAccessor _contextAccessor;
        private readonly IPropertyService _propertyService;
        private readonly ITypeSaleService _typeSaleService;
        private readonly ITypePropertyService _typePropertyService;
        private readonly IUpgradeService _upgradeService;


        public AgentController(IUserService userService, IHttpContextAccessor contextAccessor, IPropertyService propertyService, ITypeSaleService typeSaleService,
            ITypePropertyService typePropertyService, IUpgradeService upgradeService)
        {
            _userService = userService;
            _contextAccessor = contextAccessor;
            _typeSaleService = typeSaleService;
            _typePropertyService = typePropertyService;
            _propertyService = propertyService;
            _upgradeService = upgradeService;
        }
        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> MantPro()
        {
            var user = HttpContext.Session.Get<AuthenticationResponse>("user");
            var propiedades = await _propertyService.GetAllAsync();
            return View(propiedades.Where(a => a.IdUser == user.Id).ToList());
        }
        //mi perfil
        public async Task<IActionResult> MyProfile(string Id)
        {
            SaveUserViewModel vm = await _userService.GetByIdWithoutRol(Id);
            return View(vm);
        }

        [HttpPost]
        public async Task<IActionResult> MyProfile(SaveUserViewModel vm)
            {
            if (ModelState["Name"].Errors.Any() || ModelState["LastName"].Errors.Any()
                || ModelState["PhoneNumber"].Errors.Any() )
            {
                return View(vm);
            }
            /*vm.PhotoProfileUrl = UploadFiles.UploadFile(vm.file, "User", vm.Id);*/

            SaveUserViewModel response = await _userService.UpdateAsync(vm);

            if (response != null && response.HasError != true)
            {
                return RedirectToRoute(new { controller = "Agent", action = "Index" });
            }
            else
            {
                vm.HasError = response.HasError;
                vm.Error = response.Error;
                return View(vm);

            }
        }

        public async Task<IActionResult> SaveProp()
        {
            //tipos de venta
            var typeSales = await _typeSaleService.GetAllAsync();
            ViewBag.TypeSales = typeSales;
            //tipo de propiedades
            var typeProperties = await _typePropertyService.GetAllAsync();
            //mejoras
            var upgrades = await _upgradeService.GetAllAsync();
            ViewBag.Upgrades = upgrades;
            ViewBag.TypeProperties = typeProperties;
            return View("SaveProp", new SavePropertyViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveProp(SavePropertyViewModel vm)
        {
            var typeSales = await _typeSaleService.GetAllAsync();
            ViewBag.TypeSales = typeSales;
            //tipo de propiedades
            var typeProperties = await _typePropertyService.GetAllAsync();
            //mejoras
            var upgrades = await _upgradeService.GetAllAsync();
            ViewBag.Upgrades = upgrades;
            ViewBag.TypeProperties = typeProperties;

            if (ModelState["Description"].Errors.Any() || ModelState["Value"].Errors.Any()
                || ModelState["NumberRooms"].Errors.Any() || ModelState["SizeInMeters"].Errors.Any()
                || ModelState["Bathrooms"].Errors.Any() || ModelState["IdTypeProperty"].Errors.Any()
                || ModelState["IdTypeSale"].Errors.Any() || ModelState["Img1"].Errors.Any()
                || ModelState["UpgradesId"].Errors.Any() || ModelState["IdUser"].Errors.Any())
            {
                return View("SaveProp", vm);
            }
            var user = HttpContext.Session.Get<AuthenticationResponse>("user");
            vm.UrlImage1 = UploadFiles.UploadFile(vm.Img1, "Property", vm.Code);


            //en caso de que hayan pasado mas de una imagen
            if (vm.Img2 != null)
            {
                vm.UrlImage2 = UploadFiles.UploadFile(vm.Img2, "Property", vm.Code);
            }
            if (vm.Img3 != null)
            {
                vm.UrlImage3 = UploadFiles.UploadFile(vm.Img3, "Property", vm.Code);
            }
            if (vm.Img4 != null)
            {
                vm.UrlImage4 = UploadFiles.UploadFile(vm.Img4, "Property", vm.Code);

            }
            //guardamos la propiedad
            await _propertyService.SaveAsync(vm, user.Id);

            return RedirectToRoute(new { controller = "Agent", action = "MantPro" });
        }
    }
}
