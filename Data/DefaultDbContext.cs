using app.web.Models.Entities;
using Microsoft.EntityFrameworkCore;

namespace app.web.Data
{
    public class DefaultDbContext : DbContext
    {
        public DefaultDbContext() { }
        
        public DefaultDbContext(DbContextOptions<DefaultDbContext> options)
            : base(options) { }

        public virtual DbSet<Place> Places { get; set; }

        public virtual DbSet<Road> Roads { get; set; }

        public virtual DbSet<LogisticsCenter> LogisticsCenters { get; set; }
    }
}