using Microsoft.EntityFrameworkCore;
using XeroProducts.Data.Models;

namespace XeroProducts.Data.Context
{
    /// <summary>
    /// Generated context by EFCore 
    /// /// </summary>
    public partial class ProductsContext : DbContext
    {
        /// <summary>
        /// 
        /// </summary>
        public ProductsContext()
        {
        }

        /// <summary>
        /// ctor
        /// </summary>
        /// <param name="options"></param>
        public ProductsContext(DbContextOptions<ProductsContext> options)
            : base(options)
        {
        }

        public virtual DbSet<ProductOption> ProductOptions { get; set; }
        public virtual DbSet<Product> Products { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
                 
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="modelBuilder"></param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductOption>(entity =>
            {
                entity.Property(e => e.Description).HasColumnType("varchar(23)");

                entity.Property(e => e.Id).HasColumnType("varchar(36)");

                entity.Property(e => e.Name).HasColumnType("varchar(9)");

                entity.Property(e => e.ProductId).HasColumnType("varchar(36)");
            });
            modelBuilder.Entity<Product>(entity =>
            {
                entity.Property(e => e.DeliveryPrice).HasColumnType("decimal(4,2)");

                entity.Property(e => e.Description).HasColumnType("varchar(35)");

                entity.Property(e => e.Id).HasColumnType("varchar(36)");

                entity.Property(e => e.Name).HasColumnType("varchar(17)");

                entity.Property(e => e.Price).HasColumnType("decimal(6,2)");
            });
            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
