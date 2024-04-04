using Microsoft.Extensions.DependencyInjection;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Infrastructure.Persistence.Repositories;

namespace RealStateAppProg3.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services)
        {
            #region repositories
            services.AddTransient<IPropertyFavRepository, PropertyFavRepository>();
            services.AddTransient<IPropertyRepository,PropertyRepository>();
            services.AddTransient<ITypePropertyRepository,TypePropertyRepository>();
            services.AddTransient<ITypeSaleRepository, TypeSaleRepository>();
            services.AddTransient<IUpgradeRepository, UpgradeRepository>();
            services.AddTransient<IUpgradesPropertyRepository, UpgradesPropertyRepository>();
            #endregion
        }
    }
}
