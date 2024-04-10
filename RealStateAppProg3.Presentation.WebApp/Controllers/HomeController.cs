using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Presentation.WebApp.Models;
using System.Diagnostics;

namespace RealStateAppProg3.Presentation.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IPropertyService _propertyService;
        private readonly IUpgradeService _upgradeService;
        private readonly ITypeSaleService _typeSaleService;
        private readonly ITypePropertyService _typePropertyService;



        public HomeController(ILogger<HomeController> logger, IPropertyService propertyService, IUpgradeService upgradeService, 
                                ITypeSaleService typeSaleService, ITypePropertyService typePropertyServic)
        {
            _logger = logger;
            _propertyService = propertyService;
            _upgradeService = upgradeService;
            _typeSaleService = typeSaleService;
            _typePropertyService = typePropertyServic;
        }

        public async Task<IActionResult> Index()
        {
            List<PropertyViewModel> list = await _propertyService.GetAllAsync();
            ViewBag.Tp = await _typePropertyService.GetAllAsync();
            return View(list);
        }
        public async Task<IActionResult> FilterCode(string codigo)
        {
            List<PropertyViewModel> list = await _propertyService.GetAllAsync();
            return View("Index",list.Where(p => p.Code == codigo).ToList());
        }
        

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
