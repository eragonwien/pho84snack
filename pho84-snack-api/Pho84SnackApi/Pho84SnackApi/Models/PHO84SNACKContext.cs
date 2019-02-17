using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace Pho84SnackApi.Models
{
    public partial class PHO84SNACKContext : DbContext
    {
        public PHO84SNACKContext()
        {
        }

        public PHO84SNACKContext(DbContextOptions<PHO84SNACKContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Category> Category { get; set; }
        public virtual DbSet<Contact> Contact { get; set; }
        public virtual DbSet<Image> Image { get; set; }
        public virtual DbSet<Menu> Menu { get; set; }
        public virtual DbSet<MenuProduct> MenuProduct { get; set; }
        public virtual DbSet<OpenHour> OpenHour { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Restaurant> Restaurant { get; set; }
        public virtual DbSet<Role> Role { get; set; }
        public virtual DbSet<User> User { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("ProductVersion", "2.2.1-servicing-10028");

            modelBuilder.Entity<Category>(entity =>
            {
                entity.Property(e => e.Name).HasMaxLength(50);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.ImageId);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Category)
                    .HasForeignKey(d => d.RestaurantId);
            });

            modelBuilder.Entity<Contact>(entity =>
            {
                entity.Property(e => e.Address1).HasMaxLength(50);

                entity.Property(e => e.Address2).HasMaxLength(50);

                entity.Property(e => e.City).HasMaxLength(30);

                entity.Property(e => e.Email).HasMaxLength(50);

                entity.Property(e => e.Facebook).HasMaxLength(50);

                entity.Property(e => e.Phone).HasMaxLength(20);

                entity.Property(e => e.Plz)
                    .HasColumnName("PLZ")
                    .HasMaxLength(30);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Contact)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Image>(entity =>
            {
                entity.Property(e => e.MimeType).HasMaxLength(64);
            });

            modelBuilder.Entity<Menu>(entity =>
            {
                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.ImageId);

                entity.HasOne(d => d.Restaurant)
                    .WithMany(p => p.Menu)
                    .HasForeignKey(d => d.RestaurantId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<MenuProduct>(entity =>
            {
                entity.HasKey(e => new { e.MenuId, e.ProductId })
                    .HasName("PK_MenuProducts");

                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Menu)
                    .WithMany(p => p.MenuProduct)
                    .HasForeignKey(d => d.MenuId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuProducts_Menu");

                entity.HasOne(d => d.Product)
                    .WithMany(p => p.MenuProduct)
                    .HasForeignKey(d => d.ProductId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_MenuProducts_Product");
            });

            modelBuilder.Entity<OpenHour>(entity =>
            {
                entity.Property(e => e.Close).HasMaxLength(5);

                entity.Property(e => e.Day).HasMaxLength(10);

                entity.Property(e => e.Open).HasMaxLength(5);

                entity.HasOne(d => d.Contact)
                    .WithMany(p => p.OpenHour)
                    .HasForeignKey(d => d.ContactId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });

            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.Currency)
                    .IsRequired()
                    .HasMaxLength(3);

                entity.Property(e => e.Name).HasMaxLength(50);

                entity.Property(e => e.Price).HasColumnType("money");

                entity.HasOne(d => d.Category)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.CategoryId)
                    .OnDelete(DeleteBehavior.ClientSetNull);

                entity.HasOne(d => d.Image)
                    .WithMany(p => p.Product)
                    .HasForeignKey(d => d.ImageId);
            });

            modelBuilder.Entity<Restaurant>(entity =>
            {
                entity.HasIndex(e => e.Name)
                    .HasName("UQ__Restaura__737584F6DB5007B4")
                    .IsUnique();

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.Property(e => e.Title).HasMaxLength(30);
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.Email)
                    .HasName("UQ__User__A9D1053412799FC5")
                    .IsUnique();

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.Name).IsRequired();

                entity.Property(e => e.Password).IsRequired();

                entity.HasOne(d => d.Role)
                    .WithMany(p => p.User)
                    .HasForeignKey(d => d.RoleId)
                    .OnDelete(DeleteBehavior.ClientSetNull);
            });
        }
    }
}
