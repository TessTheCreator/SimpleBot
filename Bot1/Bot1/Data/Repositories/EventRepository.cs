using Bot1.Data.Entities;
using Bot1.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bot1.Data.Repositories
{
    public class EventRepository : Repository<EventEntity>, IEventRepository
    {
        public EventRepository(BotDbContext context) : base(context) { }

        public async Task<List<EventEntity>> GetEventsByGuildIdAsync(ulong guildId)
        {
            return await _dbSet
                .Where(a => a.GuildId == guildId)
                .ToListAsync();
        }
    }
}
