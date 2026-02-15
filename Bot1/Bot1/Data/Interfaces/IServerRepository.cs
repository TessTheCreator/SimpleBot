using Bot1.Data.Entities;

namespace Bot1.Data.Interfaces
{
    public interface IServerRepository : IRepository<ServerEntity>
    {
        Task<List<ServerEntity>> GetServersAsync(ulong guildId);
        Task<ServerEntity?> GetServerAsync(int rowId, ulong guildId, ulong userId);
        Task DeleteRange(IEnumerable<ServerEntity> entities);
        Task DeleteOutdatedServers();
    }
}