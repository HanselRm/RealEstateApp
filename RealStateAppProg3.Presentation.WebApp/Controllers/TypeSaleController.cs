using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;
using RealStateAppProg3.Core.Application.ViewModels.TypeSale;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class TypeSaleController : Controller
    {
        private readonly ITypeSaleService _typeSaleService;
        public TypeSaleController(ITypeSaleService typeSaleService)
        {
            _typeSaleService = typeSaleService;
        }
        public async Task<IActionResult> Index()
        {
            var vm = await _typeSaleService.GetAllAsync();
            return View(vm);
        }
        public IActionResult Save()
        {
            return View("Save",new SaveTypeSaleViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Update(SaveTypeSaleViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
            await _typeSaleService.UpdateAsync(vm,vm.Id);
            return RedirectToRoute(new { controller = "TypeSale", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> Save(SaveTypeSaleViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Save",vm);
            }
            await _typeSaleService.SaveAsync(vm);
            return RedirectToRoute(new{ controller = "TypeSale", action = "Index"});
        }

        public IActionResult Remove(int Id)
        {
            ViewBag.Id = Id;    
            return View();
        }
        public async Task<IActionResult> ConfirmRemove(int id)
        {
            await _typeSaleService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
        }
    }
}
