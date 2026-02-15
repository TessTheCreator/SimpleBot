namespace Bot1.Domain.Models
{
    public class ServerModel
    {
        public int Id { get; set; }
        public string ServerId { get; set; }
        public string ServerPassword { get; set; }
        public ulong MadeBy { get; set; }
        public string Host { get; set; }
        public string ExpiresAtHoursStr { get; set; }
        public DateTime? ExpiresAt { get; set; }
        public DateTime? CreatedAt { get; set; }
        public ulong GuildId { get; set; }
    }
}
