using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateAppProg3.Core.Application.Interfaces.Repositories;
using RealStateAppProg3.Infrastructure.Persistence.Context;
using RealStateAppProg3.Infrastructure.Persistence.Repositories;

namespace RealStateAppProg3.Infrastructure.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistenceLayer(this IServiceCollection services, IConfiguration configuracion)
        {
            #region context
            services.AddDbContext<RealStateAppContext>(options => options.UseSqlServer(configuracion.GetConnectionString("default"),
                m => m.MigrationsAssembly(typeof(RealStateAppContext).Assembly.FullName)));
            #endregion
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
