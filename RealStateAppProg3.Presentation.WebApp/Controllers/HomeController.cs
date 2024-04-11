using Microsoft.AspNetCore.Mvc;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.ViewModels.Propertys;
using RealStateAppProg3.Core.Application.ViewModels.Upgrades;
using RealStateAppProg3.Core.Domain.Entities;
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
        private readonly IUserService _userService;


        public HomeController(ILogger<HomeController> logger, IPropertyService propertyService, IUpgradeService upgradeService, 
                                ITypeSaleService typeSaleService, ITypePropertyService typePropertyServic, IUserService userService)
        {
            _logger = logger;
            _propertyService = propertyService;
            _upgradeService = upgradeService;
            _typeSaleService = typeSaleService;
            _typePropertyService = typePropertyServic;
            _userService = userService;
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
            ViewBag.Tp = await _typePropertyService.GetAllAsync();
            return View("Index",list.Where(p => p.Code == codigo).ToList());
        }

        public async Task<IActionResult> Filters(int? typePropertyId, decimal? minPrice, decimal? maxPrice, int? rooms, int? bathrooms)
        {
            List<PropertyViewModel> properties = await _propertyService.GetAllAsync();

            // Aplicar filtros
            properties = properties.Where(p =>
            (!typePropertyId.HasValue || p.IdTypeProperty == typePropertyId.Value) &&
                (!minPrice.HasValue || p.Value >= minPrice.Value) &&
                (!maxPrice.HasValue || p.Value <= maxPrice.Value) &&
                (!rooms.HasValue || p.NumberRooms == rooms.Value) &&
                (!bathrooms.HasValue || p.Bathrooms == bathrooms.Value)
            ).ToList();

            ViewBag.Tp = await _typePropertyService.GetAllAsync();
            return View("Index", properties);
        }

        public async Task<IActionResult> PropertyDetails(string id)
        {
            var properties = await _propertyService.GetallWithIncludeDetail();
            var property = properties.FirstOrDefault(p => p.Code == id);
            if (property == null)
            {
                return NotFound();
            }

            return View(property);
        }

        public async Task<IActionResult> Agent()
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
