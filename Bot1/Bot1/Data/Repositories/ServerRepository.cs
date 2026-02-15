using Bot1.Data.Entities;
using Bot1.Data.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Bot1.Data.Repositories
{
    public class ServerRepository : Repository<ServerEntity>, IServerRepository
    {
        public ServerRepository(BotDbContext context) : base(context) { }

        public async Task<List<ServerEntity>> GetServersAsync(ulong guildId)
        {
            return await _dbSet
                .Where(a => a.GuildId == guildId)
                .ToListAsync();
        }

        public async Task<ServerEntity?> GetServerAsync(int rowId, ulong guildId, ulong userId)
        {
            return await _dbSet
                .Where(a => a.GuildId == guildId && a.Id == rowId && a.MadeBy == userId)
                .FirstOrDefaultAsync();
        }

        public async Task DeleteRange(IEnumerable<ServerEntity> entities)
        {
            _context.Servers.RemoveRange(entities);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteOutdatedServers()
        {
            var entities = await _dbSet.Where(a => a.ExpiresAt < DateTime.UtcNow).ToListAsync();
            await DeleteRange(entities);
        }
    }
}
