using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.Entities;

namespace Riode.WebUI.Models.DAL
{
    public class RiodeDbContext : DbContext
    {
        public RiodeDbContext(DbContextOptions<RiodeDbContext> options) : base(options)
        {
        }

        public DbSet<Brand> Brands { get; set; }
        public DbSet<ProductColor> ProductColors { get; set; }
        public DbSet<ProductSize> ProductSizes { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<ProductImage> ProductImages { get; set; }
        public DbSet<ProductSizeColorItem> ProductSizeColorCollection { get; set; }

        public DbSet<Specification> Specifications { get; set; }
        public DbSet<SpecificationCategoryItem> SpecificationCategoryCollection { get; set; }
        public DbSet<SpecificationValue> SpecificationValues { get; set; }
        public DbSet<ContactPost> ContactPosts { get; set; }
        public DbSet<Faq> Faqs { get; set; }
        public DbSet<Subscribe> Subscribes { get; set; }
        public DbSet<AuditLog> AuditLogs { get; set; }

    }
}
