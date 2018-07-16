using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaPlace.Data
{
    public partial class PizzaPlaceDBContext : DbContext
    {
        public PizzaPlaceDBContext()
        {
        }

        public PizzaPlaceDBContext(DbContextOptions<PizzaPlaceDBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<HasToppings> HasToppings { get; set; }
        public virtual DbSet<Inventory> Inventory { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<OrderPizza> OrderPizza { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<Pizza> Pizza { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer("Server=tcp:burgos-1806.database.windows.net,1433;Initial Catalog=PizzaPlaceDB;Persist Security Info=False;User ID=burgos;Password=Sanchez123;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<HasToppings>(entity =>
            {
                entity.HasKey(e => e.ToppingsId);

                entity.ToTable("HasToppings", "Pizzeria");

                entity.Property(e => e.ToppingsId).HasColumnName("ToppingsID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.HasToppings)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_HasToppingsInventory");
            });

            modelBuilder.Entity<Inventory>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("Inventory", "Pizzeria");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .ValueGeneratedOnAdd();

                entity.HasOne(d => d.Location)
                    .WithOne(p => p.Inventory)
                    .HasForeignKey<Inventory>(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_InventoryLocation");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.LocationId);

                entity.ToTable("Locations", "Pizzeria");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);
            });

            modelBuilder.Entity<OrderPizza>(entity =>
            {
                entity.ToTable("OrderPizza", "Pizzeria");

                entity.Property(e => e.OrderPizzaId).HasColumnName("OrderPizzaID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPizza)
                    .HasForeignKey(d => d.OrderId)
                    .HasConstraintName("FK_OrderPizza_Order");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OrderPizza)
                    .HasForeignKey(d => d.PizzaId)
                    .HasConstraintName("FK_OrderAPizza");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.HasKey(e => e.OrderId);

                entity.ToTable("Orders", "Pizzeria");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.OrderTime).HasColumnType("datetime");

                entity.Property(e => e.UsersId).HasColumnName("UsersID");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_OrderLocation");

                entity.HasOne(d => d.Users)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UsersId)
                    .HasConstraintName("FK_OrderUser");
            });

            modelBuilder.Entity<Pizza>(entity =>
            {
                entity.ToTable("Pizza", "Pizzeria");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Size).HasMaxLength(50);
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.ToTable("Users", "Pizzeria");

                entity.Property(e => e.UsersId).HasColumnName("UsersID");

                entity.Property(e => e.FirstName)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.LastName).HasMaxLength(128);

                entity.Property(e => e.LocationId).HasColumnName("LocationID");

                entity.Property(e => e.Phone).HasMaxLength(10);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK_UsersLocation");
            });
        }
    }
}
