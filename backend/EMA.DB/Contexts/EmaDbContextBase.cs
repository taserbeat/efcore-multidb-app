using EMA.DB.Entities;
using Microsoft.EntityFrameworkCore;

namespace EMA.DB.Contexts
{
    /// <summary>
    /// DbContextのベースクラス
    /// </summary>
    public abstract class EmaDbContextBase : DbContext
    {
        public EmaDbContextBase(DbContextOptions options) : base(options)
        {

        }

        public DbSet<ProductEm> Products { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<ProductEm>(e =>
            {
                // テーブル名
                e.ToTable("products", builder =>
                {
                    builder.HasComment("商品のテーブル");
                });

                // 主キー
                e.HasKey(productEm => productEm.Id);

                // product_id列
                e.Property(productEm => productEm.Id)
                    .HasColumnName("product_id")
                    .HasComment("商品のID")
                    .IsRequired(true);

                // name列
                e.Property(productEm => productEm.Name)
                    .HasColumnName("name")
                    .HasComment("商品の名前")
                    .IsRequired(true);
            });
        }
    }
}
