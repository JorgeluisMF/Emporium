using EmporiumApp.Core.Application.ViewModels.User;
using EmporiumApp.Core.Domain.Common;
using EmporiumApp.Core.Domain.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using EmporiumApp.Core.Application.Helpers;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmporiumApp.Infraestructure.Persistence.Context
{
    public class ApplicationContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        private UserViewModel user = new();

        public ApplicationContext(DbContextOptions<ApplicationContext> options, IHttpContextAccessor http) : base(options)
        {
            _httpContextAccessor = http;
        }

        #region dbSets -->
        public DbSet<Improvement> Improvements { get; set; }
        public DbSet<SaleType> SaleTypes { get; set; }
        public DbSet<ProductType> PropertyTypes { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Carrito> Carritos { get; set; }
        public DbSet<ProductImprovement> PropertyImprovement { get; set; }


        #endregion

        public override Task<int> SaveChangesAsync(CancellationToken ct = new())
        {
            if (_httpContextAccessor.HttpContext != null)
            {
                user = _httpContextAccessor.HttpContext.Session.Get<UserViewModel>("user");
            }

            foreach (var entry in ChangeTracker.Entries<AuditableBaseEntity>())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.Created = DateTime.Now;
                        entry.Entity.CreatedBy = user.UserName;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModified = DateTime.Now;
                        entry.Entity.ModifiedBy = user.UserName;
                        break;
                }
            }

            return base.SaveChangesAsync(ct);
        }

        protected override void OnModelCreating(ModelBuilder mb)
        {
            #region tables

            mb.Entity<Improvement>()
                .ToTable("Improvements");

            mb.Entity<SaleType>()
                .ToTable("SaleTypes");

            mb.Entity<ProductType>()
                .ToTable("ProductTypes");

            mb.Entity<Product>()
                .ToTable("Products");

            mb.Entity<Carrito>()
                .ToTable("Carritos");

            #endregion

            #region primary keys

            mb.Entity<Improvement>()
                .HasKey(e => e.Id);

            mb.Entity<SaleType>()
                .HasKey(e => e.Id);

            mb.Entity<ProductType>()
                .HasKey(e => e.Id);

            mb.Entity<Product>()
                .HasKey(e => e.Id);

            mb.Entity<Carrito>()
                .HasKey(e => e.Id);

            #endregion

            #region relations

            mb.Entity<SaleType>()
                .HasMany<Product>(t => t.Products)
                .WithOne(p => p.SaleType)
                .HasForeignKey(p => p.SaleTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<ProductType>()
                .HasMany<Product>(t => t.Products)
                .WithOne(p => p.ProductType)
                .HasForeignKey(p => p.ProductTypeId)
                .OnDelete(DeleteBehavior.Cascade);

            mb.Entity<Product>()
                .HasMany<Improvement>(p => p.Improvements)
                .WithMany(i => i.Products);

            #endregion

            #region property configurations

            #region Improvement
            mb.Entity<Improvement>()
                .Property(p => p.Name)
                .IsRequired();

            mb.Entity<Improvement>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #region SaleType
            mb.Entity<SaleType>()
                .Property(p => p.Name)
                .IsRequired();

            mb.Entity<SaleType>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #region ProductType
            mb.Entity<ProductType>()
                .Property(p => p.Name)
                .IsRequired();

            mb.Entity<ProductType>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #region Product
            mb.Entity<Product>()
                .Property(p => p.Code)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.ProductTypeId)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.SaleTypeId)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.RoomQty)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.RestRoomQty)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.Price)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.ParcelSize)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.IdAgent)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.AgentName)
                .IsRequired();

            mb.Entity<Product>()
                .Property(p => p.Description)
                .IsRequired();
            #endregion

            #region ProductCarrito
            mb.Entity<Carrito>()
                .Property(p => p.ClientId)
                .IsRequired();

            mb.Entity<Carrito>()
                .Property(p => p.ProductId)
                .IsRequired();
            #endregion

            #endregion
        }
    }
}
