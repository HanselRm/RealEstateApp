using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class UpgradeController : Controller
    {
        private readonly IUpgradeService _upgradeService;
        public UpgradeController(IUpgradeService upgradeService)
        {
            _upgradeService = upgradeService;
        }
        public async Task<IActionResult> Index()
        {
            var vm = await _upgradeService.GetAllAsync();
            return View(vm);
        }
        public IActionResult Save()
        {
            return View("Save",new SaveUpgradesViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Update(SaveUpgradesViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
            await _upgradeService.UpdateAsync(vm,vm.Id);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> Save(SaveUpgradesViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Save",vm);
            }
            await _upgradeService.SaveAsync(vm);
            return RedirectToRoute(new{ controller = "Upgrade", action = "Index"});
        }

        public IActionResult Remove(int Id)
        {
            ViewBag.Id = Id;    
            return View();
        }
        public async Task<IActionResult> ConfirmRemove(int id)
        {
            await _upgradeService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
        }
    }
}
