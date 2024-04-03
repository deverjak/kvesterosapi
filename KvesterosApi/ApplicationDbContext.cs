using Microsoft.EntityFrameworkCore;
using KvesterosApi.Models;

namespace KvesterosApi
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Hike> Hikes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Replace with your connection string
            optionsBuilder.UseNpgsql(ConnectionStrings.DefaultConnection);
        }

    }
}