using Microsoft.AspNetCore.Mvc;
using Microsoft.Identity.Client;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class PropertyController : Controller
    {
        private readonly IPropertyService _propertyService;
        private readonly ITypeSaleService _typeSaleService;
        private readonly ITypePropertyService _typePropertyService;
        private readonly IUpgradeService _upgradeService;
        public PropertyController(IPropertyService propertyService, ITypeSaleService typeSaleService,
            ITypePropertyService typePropertyService, IUpgradeService upgradeService)
        {
            _typeSaleService = typeSaleService;
            _typePropertyService = typePropertyService;
            _propertyService = propertyService;
            _upgradeService = upgradeService;

        }
        public IActionResult Index()
        {
            return View();
        }
        
        public async Task<IActionResult> Save()
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
            return View("Save", new SavePropertyViewModel());
        }
    }
}
