using Microsoft.EntityFrameworkCore;
using OmnichannelDB.Domain;

namespace OmnichannelDB.Persistence.Database
{
    public class ApplicationDbContext : DbContext
    {
        public virtual DbSet<Player> Players { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }   
    }
}
