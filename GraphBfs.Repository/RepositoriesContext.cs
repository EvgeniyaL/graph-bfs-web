using Microsoft.EntityFrameworkCore;
using GraphBfs.Models;

namespace GraphBfs.Repository
{
    public class RepositoriesContext: DbContext
    {
        public RepositoriesContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<LogisticCenter> LogisticCenters { get; set; }
        public DbSet<Path> Paths { get; set; }
    }
}
