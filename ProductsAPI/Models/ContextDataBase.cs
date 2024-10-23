using System.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ProductsAPI.Models{
    public class ContextDataBase : IdentityDbContext<AppUser, AppRole, int>
    {
        public DbSet<Product> Products {get; set;}
        public DbSet<Shipper> Shippers {get; set;}

        public ContextDataBase(DbContextOptions<ContextDataBase> options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
            .Property(p => p.UnitPrice)
            .HasColumnType("decimal(18, 2)");  // Precision: 18, Scale: 2
            
            modelBuilder.Entity<IdentityUserLogin<int>>().HasNoKey();

            base.OnModelCreating(modelBuilder);
        }
    }
}
