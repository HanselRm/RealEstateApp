
using Microsoft.AspNetCore.Identity;
using RealStateAppProg3.Core.Application.Enums;
using RealStateAppProg3.Infrastructure.Identity.Models;
using System.Data;

namespace RealStateAppProg3.Infrastructure.Identity.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedAsync(UserManager<ApplicationUser> userManager, RoleManager<IdentityRole> roleManager)
        {
            //Creacion de roles por defecto
            await roleManager.CreateAsync(new IdentityRole(RoleENum.Admin.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RoleENum.Client.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RoleENum.Agent.ToString()));
            await roleManager.CreateAsync(new IdentityRole(RoleENum.Developer.ToString()));
        }
    }
}
