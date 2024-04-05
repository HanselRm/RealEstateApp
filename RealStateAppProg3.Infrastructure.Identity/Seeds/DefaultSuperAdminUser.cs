

using Microsoft.AspNetCore.Identity;
using RealStateAppProg3.Core.Application.Enums;
using RealStateAppProg3.Infrastructure.Identity.Models;

namespace RealStateAppProg3.Infrastructure.Identity.Seeds
{
    public class DefaultSuperAdminUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creacion de usuario Super admin
            ApplicationUser defaultUserSuper = new();
            defaultUserSuper.UserName = "superuser";
            defaultUserSuper.Email = "superuser@email.com";
            defaultUserSuper.Name = "Juan";
            defaultUserSuper.LastName = "Mejia";
            defaultUserSuper.Identification = "402191133";
            defaultUserSuper.EmailConfirmed = true;
            defaultUserSuper.IsActive = true;
            defaultUserSuper.PhoneNumberConfirmed = true;

            //Validar si el default user existe
            if (userManager.Users.All(u => u.Id != defaultUserSuper.Id))
            {
                //Validar si hay algun user con el mismo correo
                var user = await userManager.FindByEmailAsync(defaultUserSuper.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUserSuper, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUserSuper, RoleENum.Client.ToString());
                }
            }
        }
    }
}
