using Bot1.Data.Entities;
using Microsoft.EntityFrameworkCore;

namespace Bot1.Data
{
    public class BotDbContext : DbContext
    {
        public DbSet<EventEntity> Events { get; set; }
        public DbSet<ServerEntity> Servers { get; set; }

        public BotDbContext(DbContextOptions<BotDbContext> options)
            : base(options)
        {
        }
    }
}
