using Bot1.Data.Entities;

namespace Bot1.Domain.Interfaces
{
    public interface IEventService
    {
        Task CreateEventAsync(EventEntity entity);
        Task<List<EventEntity>> GetGuildEventsAsync(ulong guildId);
    }
}