namespace Bot1.App.Models
{
    public class ServerApiModel
    {
        public Guid? Id { get; set; }
        public string ServerId { get; set; }
        public string ServerPassword { get; set; }
        public string Host { get; set; }
        public ulong MadeBy { get; set; }
        public ulong GuildId { get; set; }
        public string ExpiresAtHoursStr { get; set; }
        public int? ExpiresAtHours { get; set; }
        public DateTime? CreatedAt { get; set; }
    }
}
