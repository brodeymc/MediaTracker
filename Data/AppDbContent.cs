using Microsoft.EntityFrameworkCore;
using MediaTrackerAPI.Models;

namespace MediaTrackerAPI.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<MediaItem> MediaItems { get; set; }

        public AppDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }
    }
}