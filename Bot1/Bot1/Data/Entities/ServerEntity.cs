using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bot1.Data.Entities
{
    public class ServerEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Column("server_id")]
        public string ServerId {  get; set; }
        [Column("server_pass")]
        public string ServerPassword { get; set; }
        [Column("made_by")]
        public ulong MadeBy { get; set; }
        [Column("host")]
        public string Host { get; set; }
        [Column("expires_at")]
        public DateTime ExpiresAt { get; set; }
        [Column("created_at")]
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        [Required]
        [Column("guild_id")]
        public ulong GuildId { get; set; }
    }
}
