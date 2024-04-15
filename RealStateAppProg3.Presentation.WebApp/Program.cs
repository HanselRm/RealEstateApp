using Microsoft.AspNetCore.Identity;
using RealStateAppProg3.Infrastructure.Identity;
using RealStateAppProg3.Infrastructure.Shared;
using RealStateAppProg3.Infrastructure.Identity.Models;
using RealStateAppProg3.Infrastructure.Identity.Seeds;
using RealStateAppProg3.Core.Application;
using RealStateAppProg3.Infrastructure.Persistence;
using RealStateAppProg3.Presentation.WebApp.MiddledWares;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddSession();
// Add services to the container.
builder.Services.AddControllersWithViews();
//Application
builder.Services.AddApplicationLayer();
//persistense
builder.Services.AddPersistenceLayer(builder.Configuration);
//Identity
builder.Services.AddIdentityInfrastructure(builder.Configuration);
//Shaared
builder.Services.AddSharedInfrastructure(builder.Configuration);


// Add services to the container.
builder.Services.AddControllersWithViews();

//Añadir sesiones
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

//MiddledWares
builder.Services.AddTransient<ValidateUser, ValidateUser>();
builder.Services.AddScoped<LoginAuthorize>();

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
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
