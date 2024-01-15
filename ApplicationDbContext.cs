using Microsoft.EntityFrameworkCore;
using KvesterosAdminApi.Models;

namespace KvesterosAdminApi
{
    public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Hike> Hikes { get; set; }
        
    }
}