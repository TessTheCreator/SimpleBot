using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Bot1.Data.Entities
{
    public class EventEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("event_name")]
        public string EventName { get; set; }
        [Column("description")]
        public string Description { get; set; }
        [Column("event_location")]
        public string EventLocation { get; set; }
        [Column("host")]
        public string Host { get; set; }
        [Column("created")]
        public ulong Created { get; set; }
        [Column("start_time")]
        public DateTime StartTime { get; set; }
        [Column("end_time")]
        public DateTime EndTime { get; set; }
        [Required]
        [Column("guild_id")]
        public ulong GuildId { get; set; }

    }
}
