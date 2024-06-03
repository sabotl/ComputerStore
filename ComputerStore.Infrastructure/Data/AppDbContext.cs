using Microsoft.EntityFrameworkCore;
using ComputerStore.Domain.Entities;

namespace ComputerStore.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Goods> Goods { get; set; }
        public DbSet<Cart> Carts { get; set; }
        public DbSet<CartItems> CartItems { get; set; }
        public DbSet<GoodsCategory> goodsCategories { get; set; }
        public DbSet<RefreshToken> refreshTokens { get; set; }
        public DbSet<Shop> Shops { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Cart>()
                .HasMany(c => c.CartItems)
                .WithOne(ci => ci.Cart)
                .HasForeignKey(ci => ci.CartId);

            modelBuilder.Entity<CartItems>()
                .HasOne(ci => ci.Goods)
                .WithMany()
                .HasForeignKey(ci => ci.ProductId);

            modelBuilder.Entity<Goods>()
                .HasOne(g => g.GoodsCategory)
                .WithMany()
                .HasForeignKey(g => g.CategoryID);

            modelBuilder.Entity<Goods>()
                .HasOne(g => g.ProductShop)
                .WithMany(s => s.Products)
                .HasForeignKey(g => g.ShopId);

            modelBuilder.Entity<RefreshToken>();

            modelBuilder.Entity<User>()
                .HasOne(u => u.token)
                .WithOne(rt => rt.User)
                .HasForeignKey<RefreshToken>(rt => rt.UserId);

        }
    }
}
