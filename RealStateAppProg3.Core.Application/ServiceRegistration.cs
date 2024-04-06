using Microsoft.Extensions.DependencyInjection;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Application.Services;
using System.Reflection;

namespace RealStateAppProg3.Core.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplicationLayer(this IServiceCollection services)
        {
            #region Mapper
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            #endregion
            #region Services
            services.AddTransient<IUpgradeService, UpgradeService>();
            services.AddTransient<ITypePropertyService, TypePropertyService>();
            services.AddTransient<ITypeSaleService, TypeSaleService>();
            services.AddTransient<IPropertyFavService,PropertyFavService>();
            services.AddTransient<IPropertyService, PropertyService>();
            #endregion
        }
    }
}
