using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Riode.WebUI.Models.Entities;
using Riode.WebUI.Models.Entities.Membership;

namespace Riode.WebUI.Models.DAL
{
    public class RiodeDbContext : IdentityDbContext<RiodeUser,RiodeRole,int, RiodeUserClaim, RiodeUserRole,RiodeUserLogin, RiodeRoleClaim, RiodeUserToken>
    {
        public RiodeDbContext(DbContextOptions<RiodeDbContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.Entity<RiodeUser>(e =>
            {
                e.ToTable("Users", "Membership");
            });
            builder.Entity<RiodeRole>(e =>
            {
                e.ToTable("Roles", "Membership");
                e.HasData(new List<RiodeRole>
                {
                     new RiodeRole {
                     Id= 1,
                     Name = "SuperAdmin",
                     NormalizedName = "SUPERADMIN"
                },
                     
                     new RiodeRole {
                         Id= 2,
                     Name = "Operator",
                     NormalizedName = "OPERATOR"
                },
                     
                     new RiodeRole {
                         Id= 3,
                     Name = "Reporter",
                     NormalizedName = "REPORTER"
                } 
                });         
            });

            builder.Entity<RiodeUserRole>(e =>
            {
                e.ToTable("UserRoles", "Membership");
            });
            builder.Entity<RiodeUserClaim>(e =>
            {
                e.ToTable("UserClaims", "Membership");
            });
            builder.Entity<RiodeRoleClaim>(e =>
            {
                e.ToTable("RoleClaims", "Membership");
            });
            builder.Entity<RiodeUserLogin>(e =>
            {
                e.ToTable("UserLogins", "Membership");
            });
            builder.Entity<RiodeUserToken>(e =>
            {
                e.ToTable("UserTokens", "Membership");
            });
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

        public DbSet<BlogPost> BlogPosts { get; set; }
        public DbSet<BlogPostComment> BlogPostComments { get; set; }

    }
}
