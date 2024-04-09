using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class TypePropertyController : Controller
    {
        private readonly ITypePropertyService _typePropertyService;
        public TypePropertyController(ITypePropertyService typePropertyService)
        {
            _typePropertyService = typePropertyService;
        }
        public async Task<IActionResult> Index()
        {
            var vm = await _typePropertyService.GetAllAsync();
            return View(vm);
        }
        public IActionResult Save()
        {
            return View("Save",new SaveTypePropertyViewModel());
        }
        [HttpPost]
        public async Task<IActionResult> Update(SaveTypePropertyViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                return View("Save", vm);
            }
            await _typePropertyService.UpdateAsync(vm,vm.Id);
            return RedirectToRoute(new { controller = "TypeProperty", action = "Index" });
        }
        [HttpPost]
        public async Task<IActionResult> Save(SaveTypePropertyViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                return View("Save",vm);
            }
            await _typePropertyService.SaveAsync(vm);
            return RedirectToRoute(new{ controller = "TypeProperty", action = "Index"});
        }

        public IActionResult Remove(int Id)
        {
            ViewBag.Id = Id;    
            return View();
        }
        public async Task<IActionResult> ConfirmRemove(int id)
        {
            await _typePropertyService.RemoveAsync(id);
            return RedirectToRoute(new { controller = "Upgrade", action = "Index" });
        }
    }
}
