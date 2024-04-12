using Microsoft.EntityFrameworkCore;
using KvesterosApi.Models;

namespace KvesterosApi
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
    {
        public DbSet<Hike> Hikes { get; set; }
        public DbSet<HikeImage> HikeImages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your connection string
            optionsBuilder.UseNpgsql(ConnectionStrings.DefaultConnection);
        }

    }
}