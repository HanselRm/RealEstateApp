using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITypePropertyService _typePropertyService;
        private readonly ITypeSaleService _typeSaleService;
        private readonly IUpgradeService _upgradeService;

        public AdminController(ITypePropertyService typePropertyService, ITypeSaleService typeSaleService, IUpgradeService upgradeService)
        {
            _typeSaleService = typeSaleService;
            _typePropertyService = typePropertyService;
            _upgradeService = upgradeService;
        }

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
