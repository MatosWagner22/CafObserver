using CafObserver.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace CafObserver.Infrastructure.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Order> Orders { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuración de Order
            modelBuilder.Entity<Order>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Status)
                    .IsRequired()
                    .HasConversion<string>();
                entity.Property(e => e.CreatedAt)
                    .IsRequired();

                // Relación uno a muchos con OrderItems
                entity.HasMany(e => e.Items)
                    .WithOne()
                    .HasForeignKey("OrderId")
                    .OnDelete(DeleteBehavior.Cascade);
            });

            // Configuración de OrderItem como entidad separada
            modelBuilder.Entity<OrderItem>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id)
                    .ValueGeneratedOnAdd();
                entity.Property(e => e.ProductId)
                    .IsRequired();
                entity.Property(e => e.ProductName)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.Quantity)
                    .IsRequired();
                entity.Property(e => e.SpecialInstructions)
                    .HasMaxLength(500);

                // Configurar la relación con Order sin propiedad de navegación
                entity.HasOne<Order>()
                    .WithMany(o => o.Items)
                    .HasForeignKey("OrderId")
                    .IsRequired();
            });

            // Configuración de Product
            modelBuilder.Entity<Product>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(100);
                entity.Property(e => e.Description)
                    .HasMaxLength(500);
                entity.Property(e => e.Price)
                    .IsRequired()
                    .HasColumnType("decimal(18,2)");
                entity.Property(e => e.Category)
                    .IsRequired()
                    .HasConversion<string>();
                entity.Property(e => e.IsAvailable)
                    .IsRequired();
                entity.Property(e => e.PreparationTimeMinutes)
                    .IsRequired();
            });

            // Datos semilla para Products
            modelBuilder.Entity<Product>().HasData(
                new Product
                {
                    Id = 1,
                    Name = "Café Americano",
                    Description = "Café negro tradicional",
                    Price = 2.50m,
                    Category = Domain.Enums.ProductCategory.Coffee,
                    IsAvailable = true,
                    PreparationTimeMinutes = 3
                },
                new Product
                {
                    Id = 2,
                    Name = "Capuchino",
                    Description = "Café con leche espumosa",
                    Price = 3.50m,
                    Category = Domain.Enums.ProductCategory.Coffee,
                    IsAvailable = true,
                    PreparationTimeMinutes = 5
                },
                new Product
                {
                    Id = 3,
                    Name = "Croissant",
                    Description = "Crujiente croissant de mantequilla",
                    Price = 2.00m,
                    Category = Domain.Enums.ProductCategory.Bakery,
                    IsAvailable = true,
                    PreparationTimeMinutes = 2
                },
                new Product
                {
                    Id = 4,
                    Name = "Sándwich de Jamón y Queso",
                    Description = "Sándwich fresco con jamón y queso",
                    Price = 5.00m,
                    Category = Domain.Enums.ProductCategory.Sandwich,
                    IsAvailable = true,
                    PreparationTimeMinutes = 7
                }
            );
        }
    }
}
