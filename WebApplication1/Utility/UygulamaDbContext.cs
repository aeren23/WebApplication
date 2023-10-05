using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Utility
{
    public class UygulamaDbContext :IdentityDbContext
    {
        public UygulamaDbContext(DbContextOptions<UygulamaDbContext> options) : base(options) { }

        public DbSet<KitapTuru> KitapTurleri { get; set; }
        public DbSet<Kitap> Kitaplar {get; set; }
        public DbSet<Kiralama> Kiralamalar { get; set;}
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
    }
}
