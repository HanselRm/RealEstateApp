

using Microsoft.AspNetCore.Identity;
using RealStateAppProg3.Core.Application.Enums;
using RealStateAppProg3.Infrastructure.Identity.Models;

namespace RealStateAppProg3.Infrastructure.Identity.Seeds
{
    public class DefaultAgentUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creacion de usuario Agent
            ApplicationUser defaultUserAgent = new();
            defaultUserAgent.UserName = "agentuser";
            defaultUserAgent.Email = "agentuser@email.com";
            defaultUserAgent.Name = "Jose";
            defaultUserAgent.LastName = "Mejia";
            defaultUserAgent.Identification = "40229997261";
            defaultUserAgent.EmailConfirmed = true;
            defaultUserAgent.IsActive = true;
            defaultUserAgent.PhoneNumberConfirmed = true;

            //Validar si el default user existe
            if (userManager.Users.All(u => u.Id != defaultUserAgent.Id))
            {
                //Validar si hay algun user con el mismo correo
                var user = await userManager.FindByEmailAsync(defaultUserAgent.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUserAgent, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUserAgent, RoleENum.Agent.ToString());
                }
            }
        }
    }
}
