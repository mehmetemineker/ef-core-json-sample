using Microsoft.EntityFrameworkCore;

namespace EFCoreJsonSample
{
    public class AppDbContext(DbContextOptions opts) : DbContext(opts)
    {
        public DbSet<Product> Products { get; set; }

        //protected override void OnModelCreating(ModelBuilder modelBuilder)
        //{
        //    modelBuilder.Entity<Product>()
        //        .Property(p => p.Details)
        //        .HasColumnType("jsonb")
        //        .HasConversion(
        //            v => JsonSerializer.Serialize(v, (JsonSerializerOptions)null),
        //            v => JsonSerializer.Deserialize<ProductDetails>(v, (JsonSerializerOptions)null));
        //}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Product>()
                .OwnsOne(p => p.Details)
                .ToJson();
        }
    }

    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public ProductDetails Details { get; set; } = null!;
    }

    [Owned]
    public class ProductDetails
    {
        public string Manufacturer { get; set; } = null!;
        public string WarrantyPeriod { get; set; } = null!;
        public List<string> Tags { get; set; } = null!;
    }
}
