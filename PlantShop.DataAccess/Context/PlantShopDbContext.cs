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

           
          

            modelBuilder.Entity<IdentityUserLogin<int>>(b =>
            {
                b.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            });
        }

        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogFile> BlogFiles { get; set; }
        public DbSet<Contact> Contacts { get; set; }
      
        public DbSet<F.File> Files { get; set; }
    }
}
