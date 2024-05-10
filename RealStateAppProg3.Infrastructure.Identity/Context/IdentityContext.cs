

using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using RealStateAppProg3.Infrastructure.Identity.Models;

namespace RealStateAppProg3.Infrastructure.Identity.Context
{
    public class IdentityContext : IdentityDbContext<ApplicationUser>
    {
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //Fluent API

            //Le pasa el model a la clase base
            base.OnModelCreating(modelBuilder);

            //Schema por default que tendra las tablas creadas en Identity
            modelBuilder.HasDefaultSchema("Identity");

            //Configuracion de tablas
            //Cambiando el nombre de la tabla Users
            modelBuilder.Entity<ApplicationUser>(entity =>
            {
                entity.ToTable(name: "Users");
            });

            //Cambiando el nombre de la tabla Role
            modelBuilder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Roles");
            });

            //Cambiando el nombre de la tabla UserRole
            modelBuilder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable(name: "UserRoles");
            });

            //Cambiando el nombre de la tabla UserLogin
            modelBuilder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable(name: "UserLogins");
            });
        }
    }
}