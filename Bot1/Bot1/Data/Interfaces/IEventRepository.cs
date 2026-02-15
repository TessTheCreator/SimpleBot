using Bot1.Data.Entities;

namespace Bot1.Data.Interfaces
{
    public interface IEventRepository : IRepository<EventEntity>
    {
        Task<List<EventEntity>> GetEventsByGuildIdAsync(ulong guildId);
    }
}