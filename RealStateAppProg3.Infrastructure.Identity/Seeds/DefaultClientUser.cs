
using Microsoft.AspNetCore.Identity;
using RealStateAppProg3.Core.Application.Enums;
using RealStateAppProg3.Infrastructure.Identity.Models;

namespace RealStateAppProg3.Infrastructure.Identity.Seeds
{
    public class DefaultClientUser
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creacion de usuario Cliente
            ApplicationUser defaultUserClient = new();
            defaultUserClient.UserName = "clientuser";
            defaultUserClient.Email = "clientuser@email.com";
            defaultUserClient.Name = "Juan";
            defaultUserClient.LastName = "Mejia";
            defaultUserClient.Identification = "402191122";
            defaultUserClient.EmailConfirmed = true;
            defaultUserClient.PhoneNumberConfirmed = true;

            //Validar si el default user existe
            if (userManager.Users.All(u => u.Id != defaultUserClient.Id))
            {
                //Validar si hay algun user con el mismo correo
                var user = await userManager.FindByEmailAsync(defaultUserClient.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUserClient, "123Pa$$word!");
                    await userManager.AddToRoleAsync(defaultUserClient, RoleENum.Client.ToString());
                }
            }
        }
    }
}
