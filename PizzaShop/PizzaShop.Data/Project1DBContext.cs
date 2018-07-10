using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PizzaShop.Data
{
    public partial class Project1DBContext : DbContext
    {
        public Project1DBContext()
        {
        }

        public Project1DBContext(DbContextOptions<Project1DBContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Ingredients> Ingredients { get; set; }
        public virtual DbSet<LocationIngredientJunction> LocationIngredientJunction { get; set; }
        public virtual DbSet<Locations> Locations { get; set; }
        public virtual DbSet<OrderPizzaJunction> OrderPizzaJunction { get; set; }
        public virtual DbSet<Orders> Orders { get; set; }
        public virtual DbSet<PizzaIngredientJunction> PizzaIngredientJunction { get; set; }
        public virtual DbSet<Pizzas> Pizzas { get; set; }
        public virtual DbSet<SizingPricing> Ingredient { get; set; }
        public virtual DbSet<Users> Users { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Ingredients>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Ingredients", "PizzaShop");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Type).HasMaxLength(128);
            });

            modelBuilder.Entity<LocationIngredientJunction>(entity =>
            {
                entity.HasKey(e => new { e.LocationId, e.IngredientId });

                entity.ToTable("LocationIngredientJunction", "PizzaShop");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .HasMaxLength(128);

                entity.Property(e => e.IngredientId)
                    .HasColumnName("IngredientID")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.LocationIngredientJunction)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LocationI__Ingre__151B244E");

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.LocationIngredientJunction)
                    .HasForeignKey(d => d.LocationId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__LocationI__Locat__1332DBDC");
            });

            modelBuilder.Entity<Locations>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.ToTable("Locations", "PizzaShop");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();
            });

            modelBuilder.Entity<OrderPizzaJunction>(entity =>
            {
                entity.HasKey(e => new { e.PizzaId, e.OrderId });

                entity.ToTable("OrderPizzaJunction", "PizzaShop");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.OrderId).HasColumnName("OrderID");

                entity.HasOne(d => d.Order)
                    .WithMany(p => p.OrderPizzaJunction)
                    .HasForeignKey(d => d.OrderId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPizz__Order__08B54D69");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.OrderPizzaJunction)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__OrderPizz__Pizza__06CD04F7");
            });

            modelBuilder.Entity<Orders>(entity =>
            {
                entity.ToTable("Orders", "PizzaShop");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.LocationId)
                    .HasColumnName("LocationID")
                    .HasMaxLength(128);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.Timestamp).HasColumnType("datetime");

                entity.Property(e => e.UserId)
                    .HasColumnName("UserID")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Location)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.LocationId)
                    .HasConstraintName("FK__Orders__Location__73BA3083");

                entity.HasOne(d => d.User)
                    .WithMany(p => p.Orders)
                    .HasForeignKey(d => d.UserId)
                    .HasConstraintName("FK__Orders__UserID__74AE54BC");
            });

            modelBuilder.Entity<PizzaIngredientJunction>(entity =>
            {
                entity.HasKey(e => new { e.PizzaId, e.IngredientId });

                entity.ToTable("PizzaIngredientJunction", "PizzaShop");

                entity.Property(e => e.PizzaId).HasColumnName("PizzaID");

                entity.Property(e => e.IngredientId)
                    .HasColumnName("IngredientID")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Ingredient)
                    .WithMany(p => p.PizzaIngredientJunction)
                    .HasForeignKey(d => d.IngredientId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PizzaIngr__Ingre__10566F31");

                entity.HasOne(d => d.Pizza)
                    .WithMany(p => p.PizzaIngredientJunction)
                    .HasForeignKey(d => d.PizzaId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK__PizzaIngr__Pizza__0F624AF8");
            });

            modelBuilder.Entity<Pizzas>(entity =>
            {
                entity.ToTable("Pizzas", "PizzaShop");

                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Price).HasColumnType("money");

                entity.Property(e => e.SizeId)
                    .HasColumnName("SizeID")
                    .HasMaxLength(128);

                entity.HasOne(d => d.Size)
                    .WithMany(p => p.Pizzas)
                    .HasForeignKey(d => d.SizeId)
                    .HasConstraintName("FK__Pizzas__SizeID__00200768");
            });

            modelBuilder.Entity<SizingPricing>(entity =>
            {
                entity.HasKey(e => e.Size);

                entity.ToTable("SizingPricing", "PizzaShop");

                entity.Property(e => e.Size)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.BasePrice).HasColumnType("money");

                entity.Property(e => e.ToppingPrice).HasColumnType("money");
            });

            modelBuilder.Entity<Users>(entity =>
            {
                entity.HasKey(e => e.Username);

                entity.ToTable("Users", "PizzaShop");

                entity.Property(e => e.Username)
                    .HasMaxLength(128)
                    .ValueGeneratedNever();

                entity.Property(e => e.Email).HasMaxLength(128);

                entity.Property(e => e.FavLocation).HasMaxLength(128);

                entity.Property(e => e.FirstName).HasMaxLength(128);

                entity.Property(e => e.LastName).HasMaxLength(128);

                entity.Property(e => e.Phone).HasMaxLength(128);

                entity.HasOne(d => d.FavLocationNavigation)
                    .WithMany(p => p.Users)
                    .HasForeignKey(d => d.FavLocation)
                    .HasConstraintName("FK__Users__FavLocati__7D439ABD");
            });
        }
    }
}
