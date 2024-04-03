
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RealStateAppProg3.Core.Application.Interfaces.Service;
using RealStateAppProg3.Core.Domain.Settings;
using RealStateAppProg3.Infrastructure.Shared.Service;

namespace RealStateAppProg3.Infrastructure.Shared
{
    public static class ServiceRegistration
    {
        public static void AddSharedInfrastructure(this IServiceCollection service, IConfiguration configuracion)
        {
            service.Configure<MailSettings>(configuracion.GetSection("MailSettings"));
            service.AddTransient<IEmailServices, EmailServices>();

        }
    }
}
