using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using PlantShop.DataAccess.Entities.Main;
using PlantShop.DataAccess.Entities.Users;
using F = PlantShop.DataAccess.Entities.Main;

namespace PlantShop.DataAccess.Context
{
    public class PlantShopDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, int>
    {
        public PlantShopDbContext(DbContextOptions options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ProductOrder>()
                .HasKey(po => new { po.ProductId, po.OrderId });

            modelBuilder.Entity<ProductOrder>()
                .HasOne(sc => sc.Product)
                .WithMany(s => s.ProductOrders)
                .HasForeignKey(sc => sc.ProductId);

            modelBuilder.Entity<ProductOrder>()
                .HasOne(sc => sc.Order)
                .WithMany(c => c.ProductOrders)
                .HasForeignKey(sc => sc.OrderId);

            modelBuilder.Entity<PhoneNumber>()
                .HasMany(x => x.Orders)
                .WithOne(c => c.Number)
                .HasForeignKey(sc => sc.numberId); 
            modelBuilder.Entity<PhoneNumber>()
                .HasMany(x => x.Contacts)
                .WithOne(c => c.Number)
                .HasForeignKey(sc => sc.numberId);

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<PhoneNumber> PhoneNumbers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<ProductOrder> ProductOrders { get; set; }
        public DbSet<ProductFile> ProductFiles { get; set; }
        public DbSet<About> Abouts { get; set; }
        public DbSet<AboutCompany> AboutCompanies { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Slider> Sliders { get; set; }
        public DbSet<SliderFile> SliderFiles { get; set; }
        public DbSet<F.File> Files { get; set; }
    }
}
