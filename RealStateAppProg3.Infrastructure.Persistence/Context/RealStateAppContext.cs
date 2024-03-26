
using Microsoft.EntityFrameworkCore;
using RealStateAppProg3.Core.Domain.Entities;

namespace RealStateAppProg3.Infrastructure.Persistence.Context
{
    public class RealStateAppContext : DbContext
    {
        public RealStateAppContext(DbContextOptions<RealStateAppContext> options) : base(options){ }

        public DbSet<Property> Property { get; set; }
        public DbSet<TypeProperty> TypeProperty { get; set; }
        public DbSet<TypeSale> TypeSale { get; set; }
        public DbSet<Upgrades> Upgrades { get; set; }
        public DbSet<UpgradesProperty> UpgradesProperty { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //base function
            base.OnModelCreating(modelBuilder);
            #region Renaming tables 
            modelBuilder.Entity<Property>().ToTable("Property");
            modelBuilder.Entity<TypeProperty>().ToTable("TypeProperty");
            modelBuilder.Entity<TypeSale>().ToTable("TypeSale");
            modelBuilder.Entity<Upgrades>().ToTable("Upgrade");
            modelBuilder.Entity<UpgradesProperty>().ToTable("UpgradesProperty");
            #endregion
            #region Configuring Keys
            modelBuilder.Entity<Property>().HasKey(x => x.Code);
            modelBuilder.Entity<TypeProperty>().HasKey(x => x.Id);
            modelBuilder.Entity<TypeSale>().HasKey(x => x.Id);
            modelBuilder.Entity<Upgrades>().HasKey(x => x.Id);
            modelBuilder.Entity<UpgradesProperty>().HasKey(x => x.Id);
            #endregion
            #region Configuring Foreign Keys
            #region Property Keys
            modelBuilder.Entity<Property>()
                .HasOne(x => x.TypeProperty)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.IdTypeProperty);


            modelBuilder.Entity<Property>()
                .HasOne(x => x.TypeSale)
                .WithMany(x => x.Properties)
                .HasForeignKey(x => x.IdTypeSale);

            modelBuilder.Entity<Property>()
                .HasMany(x => x.Upgrades)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.IdProperty).OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Property>()
                .HasMany(x => x.propertyFavs)
                .WithOne(x => x.Property)
                .HasForeignKey(x => x.IdProperty).OnDelete(DeleteBehavior.Cascade);
            #endregion
            #region Upgrades Keys
            modelBuilder.Entity<Upgrades>()
                .HasMany(x => x.Properties)
                .WithOne(x => x.Upgrades)
                .HasForeignKey(x => x.IdUpgrade);
            #endregion
            #endregion
        }
    }
}
