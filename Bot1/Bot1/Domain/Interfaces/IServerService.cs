using Bot1.Data.Entities;
using Bot1.Domain.Models;

namespace Bot1.Domain.Interfaces
{
    public interface IServerService
    {
        Task<ServerModel> CreateServerAsync(ServerModel server);
        Task<string> GetServersAsync(ulong guildId);
        Task<ServerModel> GetServerAsync(ulong guildId, ulong userId, int rowId);
        Task<string> GetAllServers();
        Task UpdateServerAsync(ServerModel server, bool isAdmin);
        Task RemoveServerAsync(int serverId, ulong userId, ulong guildId, bool isAdmin);
        Task CleanupExpiredEntriesAsync();
    }
}