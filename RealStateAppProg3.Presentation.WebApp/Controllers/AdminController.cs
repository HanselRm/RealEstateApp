using AspNetCore;
using Microsoft.AspNetCore.Mvc;
using Org.BouncyCastle.Asn1.Mozilla;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using RealStateAppProg3.Core.Application.ViewModels.Users;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITypePropertyService _typePropertyService;
        private readonly ITypeSaleService _typeSaleService;
        private readonly IUpgradeService _upgradeService;
        //importacion del servicio de usuario
        private readonly IUserService _userService;

        public AdminController(ITypePropertyService typePropertyService,
            ITypeSaleService typeSaleService, IUpgradeService upgradeService, IUserService userService)
        {
            _typeSaleService = typeSaleService;
            _typePropertyService = typePropertyService;
            _upgradeService = upgradeService;
            _userService = userService;
        }
        #region Mant. Admin
        //obtener todos los usuarios con nivel adminsitrador
        public async Task<IActionResult> Index()
        {
            var users = await _userService.GetUsersAdmin();
            return View("IndexAdmin",users);
        }
        //guardar administradores
        public IActionResult SaveAdminUser()
        {
            var userVm = new SaveUserViewModel{ IsActive = true };
            return View("SaveAdminUser", userVm);
        }
        [HttpPost]
        public async Task<IActionResult> SaveAdminUser(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveAdminUser", vm);
            }
            var origin = Request.Headers["origin"];
            var response = await _userService.RegisterAsync(vm, origin);
            //-------------------------
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

        //actualizar administradores
        public async Task<IActionResult> UpdateAdminUser(string Id)
        {
            var user = await _userService.GetByIdWithoutRol(Id); 
            if(user != null)
            {
                return View("UpdateAdminUser", user);
            }
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> UpdateAdminUser(SaveUserViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("UpdateAdminUser", vm);
            }

            await _userService.UpdateAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        //actualizar el estado del usuario IsActive
        public async Task<IActionResult> UpdateStatusUser(string Id, string Message)
        {
            ViewBag.Id = Id;
            ViewBag.Message = Message;  
            return View("ConfirmStatus");
        }
        //confirmando que desea cambiar el estado 
        public async Task<IActionResult> ConfirmUpdateStatus(string Id)
        {
            var user = await _userService.GetByIdWithoutRol(Id);
            //invertimos su valor, en caso de que este activo lo pondra falso y viceversa
            user.IsActive = !user.IsActive;
            //actualizamos
            await _userService.UpdateAsync(user);
            return RedirectToRoute(new { controller = "Admin", action = "Index" });
        }
        #endregion
        #region Mant. de propiedades
        //Mantenimiento de propiedades
        public async Task<IActionResult> MantTpro()
        {
            var vm = await _typePropertyService.GetAllAsync();
            return View(vm);
        }
        //guarda tipo de propiedad
        public IActionResult SaveTPro()
        {
            return View("SaveTPro", new SaveTypePropertyViewModel());
        }

        
        [HttpPost]
        public async Task<IActionResult> SaveTPro(SaveTypePropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTPro", vm);
            }
            await _typePropertyService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "MantTpro" });
        }
        //actualizar tipo de propiedades
        public async Task<IActionResult> UpdateTPro(int Id)
        {
            var typeProperty = await _typePropertyService.GetByIdAsync(Id);
            return View("SaveTPro", typeProperty);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateTPro(SaveTypePropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTPro", vm);
            }
            await _typePropertyService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTpro" });

        }

        public IActionResult RemoveTPro(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }

        public async Task<IActionResult> ConfirmRemoveTPro(int id)
        {
            await _typePropertyService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTpro" });
        }
        #endregion

        #region Mant. Tipo de venta
        public async Task<IActionResult> MantTSale()
        {
            var vm = await _typeSaleService.GetAllAsync();
            return View(vm);
        }

        public IActionResult SaveTSale()
        {
            return View("SaveTSale", new SaveTypeSaleViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> SaveTSale(SaveTypeSaleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTSale", vm);
            }
            await _typeSaleService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "MantTSale" });
        }

        public async Task<IActionResult> UpdateTSale(int Id)
        {
            var typeSale = await _typeSaleService.GetByIdAsync(Id);
            return View("SaveTSale", typeSale);

        }
        [HttpPost]
        public async Task<IActionResult> UpdateTSale(SaveTypeSaleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveTSale", vm);
            }
            await _typeSaleService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTSale" });
        }

        public IActionResult RemoveTSale(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public async Task<IActionResult> ConfirmRemoveTSale(int id)
        {
            await _typeSaleService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Admin", action = "MantTSale" });
        }
        #endregion

        #region Mant. de Mejoras
        public async Task<IActionResult> MantUpgrade()
        {
            var vm = await _upgradeService.GetAllAsync();
            return View(vm);
        }

        public IActionResult SaveUpgrade()
        {
            return View("SaveUpgrade", new SaveUpgradesViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> SaveUpgrade(SaveUpgradesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUpgrade", vm);
            }
            await _upgradeService.SaveAsync(vm);
            return RedirectToRoute(new { controller = "Admin", action = "MantUpgrade" });
        }
        public async Task<IActionResult> UpdateUpgrade(int Id)
        {
            
            var Upgrade = await _upgradeService.GetByIdAsync(Id);
            return View("SaveUpgrade", Upgrade);

        }

        [HttpPost]
        public async Task<IActionResult> UpdateUpgrade(SaveUpgradesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("SaveUpgrade", vm);
            }
            await _upgradeService.UpdateAsync(vm, vm.Id);
            return RedirectToRoute(new { controller = "Admin", action = "MantUpgrade" });
        }

        public IActionResult RemoveUpgrade(int Id)
        {
            ViewBag.Id = Id;
            return View();
        }
        public async Task<IActionResult> ConfirmRemoveUpgrade(int id)
        {
            await _upgradeService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Admin", action = "MantUpgrade" });
        }
        #endregion
    }
}
