using Bot1.Data.Entities;
using Bot1.Data.Interfaces;
using Bot1.Domain.Interfaces;

namespace Bot1.Services
{
    public class EventService : IEventService
    {
        private readonly IEventRepository _repo;

        public EventService(IEventRepository repo)
        {
            _repo = repo;
        }

        public async Task<List<EventEntity>> GetGuildEventsAsync(ulong guildId)
        {
            return await _repo.GetEventsByGuildIdAsync(guildId);
        }

        public async Task CreateEventAsync(EventEntity entity)
        {
            await _repo.AddAsync(entity);
        }

        public async Task DeleteEventAsync(EventEntity entity)
        {
            await _repo.AddAsync(entity);
        }
    }
}
