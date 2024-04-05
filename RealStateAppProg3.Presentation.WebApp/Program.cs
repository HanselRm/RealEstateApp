using Microsoft.AspNetCore.Identity;
using RealStateAppProg3.Infrastructure.Identity;
using RealStateAppProg3.Infrastructure.Shared;
using RealStateAppProg3.Infrastructure.Identity.Models;
using RealStateAppProg3.Infrastructure.Identity.Seeds;

var builder = WebApplication.CreateBuilder(args);
#region Service registration
//service registration identity
builder.Services.AddIdentityInfrastructure(builder.Configuration);

//service registration Shaared
builder.Services.AddSharedInfrastructure(builder.Configuration);
#endregion
// Add services to the container.
builder.Services.AddControllersWithViews();

//Añadir sesiones
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

//Metodo para correr los Seeds
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        //dependency injection 
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();

        //Metodos de los servicios para crear roles y default users con roles
        await DefaultRoles.SeedAsync(userManager, roleManager);
        await DefaultAdministratorUser.SeedAsync(userManager, roleManager);
        await DefaultAgentUser.SeedAsync(userManager, roleManager);
        await DefaultClientUser.SeedAsync(userManager, roleManager);
        await DefaultSuperAdminUser.SeedAsync(userManager, roleManager);
    }
    catch (Exception ex)
    {
    }
}


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
//usa sesiones
app.UseSession();

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

//autentica primero
app.UseAuthentication();    
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=User}/{action=Index}/{id?}");

app.Run();
