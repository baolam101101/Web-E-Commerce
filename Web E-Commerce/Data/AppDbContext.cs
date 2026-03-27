using Microsoft.EntityFrameworkCore;
using Web_E_Commerce.Models;

namespace Web_E_Commerce.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Product> Products { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItem> CartItems { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<Address> Addresses { get; set; }
        public DbSet<ProductReview> ProductReviews { get; set; }
        public DbSet<SellerRequest> SellerRequests { get; set; }
        public DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Relationship Product - Category
            modelBuilder.Entity<Product>()
                .HasOne(p => p.Category)
                .WithMany(c => c.Products)
                .HasForeignKey(p => p.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship Order - User
            modelBuilder.Entity<Order>()
                .HasOne<User>()
                .WithMany(u => u.Orders)
                .HasForeignKey(o => o.UserId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship Cart - User
            modelBuilder.Entity<Cart>()
                .HasOne<User>()
                .WithMany()
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship CartItem - Cart
            modelBuilder.Entity<CartItem>()
                .HasOne<Cart>()
                .WithMany(c => c.CartItems)
                .HasForeignKey(ci => ci.CartId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship CartItem - Product
            modelBuilder.Entity<CartItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(ci => ci.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship OrderItem - Product
            modelBuilder.Entity<OrderItem>()
                .HasOne<Product>()
                .WithMany()
                .HasForeignKey(oi => oi.ProductId)
                .OnDelete(DeleteBehavior.Cascade);

            // Relationship OrderItem - Order
            modelBuilder.Entity<OrderItem>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(o => o.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship Payment - Order
            modelBuilder.Entity<Payment>()
                .HasOne<Order>()
                .WithMany()
                .HasForeignKey(p => p.OrderId)
                .OnDelete(DeleteBehavior.Restrict);

            // Relationship ProductReview - Product 
            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.Product)
                .WithMany()
                .HasForeignKey(pr => pr.ProductId);

            // Relationship ProductReview - User
            modelBuilder.Entity<ProductReview>()
                .HasOne(pr => pr.User)
                .WithMany()
                .HasForeignKey(pr => pr.UserId);

            // Precision for currency fields
            modelBuilder.Entity<Order>()
                .Property(o => o.TotalAmount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.UnitPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<OrderItem>()
                .Property(oi => oi.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Payment>()
                .Property(p => p.Amount)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Product>()
                .Property(p => p.Price)
                .HasPrecision(18, 2);

            modelBuilder.Entity<Cart>()
                .Property(c => c.TotalPrice)
                .HasPrecision(18, 2);

            modelBuilder.Entity<CartItem>()
                .Property(ci => ci.PriceAtTime)
                .HasPrecision(18, 2);

            // Configure many-to-many User <->Role
            modelBuilder.Entity<UserRole>()
                .HasKey(ur => new { ur.UserId, ur.RoleId });

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.User)
                .WithMany(u => u.UserRoles)
                .HasForeignKey(ur => ur.UserId);

            modelBuilder.Entity<UserRole>()
                .HasOne(ur => ur.Role)
                .WithMany(r => r.UserRoles)
                .HasForeignKey(ur => ur.RoleId);

            // ============================
            // SellerRequest configuration
            // ============================
            modelBuilder.Entity<SellerRequest>()
                .Property(sr => sr.Status)
                .HasConversion<string>();

            // Seller Request → User
            modelBuilder.Entity<SellerRequest>()
                .HasOne(ur => ur.User)
                .WithMany()
                .HasForeignKey(sr => sr.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            // Index 
            modelBuilder.Entity<User>()
                .HasIndex(u => u.UserName)
                .IsUnique();

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.UserId);

            modelBuilder.Entity<Cart>()
                .HasIndex(c => c.UserId)
                .IsUnique();

            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => new { ci.CartId, ci.ProductId })
                .IsUnique();
            modelBuilder.Entity<CartItem>()
                .HasIndex(ci => ci.ProductId);

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => oi.OrderId);

            modelBuilder.Entity<OrderItem>()
                .HasIndex(oi => oi.ProductId);

            modelBuilder.Entity<Product>()
                .HasIndex(p => p.CategoryId);

            modelBuilder.Entity<Order>()
                .HasIndex(o => o.OrderStatus);

            modelBuilder.Entity<Payment>()
                .HasIndex(p => p.OrderId);

            // ========================
            // GUID NEWSEQUENTIALID (chuỗi Guid tăng dần)
            // ========================
            foreach (var entity in modelBuilder.Model.GetEntityTypes())
            {
                if (typeof(BaseEntity).IsAssignableFrom(entity.ClrType))
                {
                    modelBuilder.Entity(entity.ClrType)
                        .Property("Id")
                        .HasDefaultValueSql("NEWSEQUENTIALID()");
                }
            }

            // Seed Roles
            var adminRoleId = Guid.Parse("11111111-1111-1111-1111-111111111111");
            var sellerRoleId = Guid.Parse("22222222-2222-2222-2222-222222222222");
            var customerRoleId = Guid.Parse("33333333-3333-3333-3333-333333333333");

            var adminUserId = Guid.Parse("aaaaaaaa-aaaa-aaaa-aaaa-aaaaaaaaaaaa");
            var sellerUserId = Guid.Parse("bbbbbbbb-bbbb-bbbb-bbbb-bbbbbbbbbbbb");

            modelBuilder.Entity<Role>().HasData(
                new Role { Id = adminRoleId, Name = "Admin" },
                new Role { Id = sellerRoleId, Name = "Seller" },
                new Role { Id = customerRoleId, Name = "Customer" }
            );

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = adminUserId,
                    UserName = "admin",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("admin123")
                },
                new User
                {
                    Id = sellerUserId,
                    UserName = "seller",
                    PasswordHash = BCrypt.Net.BCrypt.HashPassword("seller123")
                }
            );

            // Seed UserRoles
            modelBuilder.Entity<UserRole>().HasData(
                new UserRole { UserId = adminUserId, RoleId = adminRoleId },
                new UserRole { UserId = sellerUserId, RoleId = sellerRoleId }
            );
        }
        public override async Task<int> SaveChangesAsync(
            CancellationToken cancellationToken = default)
        {
            foreach (var entry in ChangeTracker.Entries<BaseEntity>())
            {
                if (entry.State == EntityState.Added)
                {
                    entry.Entity.CreatedAt = DateTime.UtcNow;
                }

                if (entry.State == EntityState.Modified)
                {
                    entry.Entity.UpdatedAt = DateTime.UtcNow;
                }
            }

            return await base.SaveChangesAsync(cancellationToken);
        }
    }
}