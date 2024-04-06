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

        public IActionResult Save()
        {
            return View("Save",new SaveUpgradesViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Save(SaveUpgradesViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Save",vm);
            }
            await _upgradeService.SaveAsync(vm);
            return RedirectToRoute(new{ controller = "Upgrade", action = "Save"});
        }
    }
}
