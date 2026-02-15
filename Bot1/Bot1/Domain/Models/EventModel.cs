namespace Bot1.Domain.Models
{
    public class EventModel
    {
        public int Id { get; set; }
        public string EventName { get; set; }
        public string Description { get; set; }
        public string EventLocation { get; set; }
        public string Host { get; set; }
        public string CreatedBy { get; set; }
        public DateTime? StartTime { get; set; }
        public DateTime? EndTime { get; set; }
        public ulong GuildId { get; set; }
    }
}
