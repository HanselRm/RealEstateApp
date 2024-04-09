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
        
        
        
        

        
    }
}
