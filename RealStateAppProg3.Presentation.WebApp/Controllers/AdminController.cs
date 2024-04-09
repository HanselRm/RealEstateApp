using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.TypeProperty;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class AdminController : Controller
    {
        private readonly ITypePropertyService _typePropertyService;

        public AdminController(ITypePropertyService typePropertyService)
        {
            _typePropertyService = typePropertyService;
        }
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
    }
}
