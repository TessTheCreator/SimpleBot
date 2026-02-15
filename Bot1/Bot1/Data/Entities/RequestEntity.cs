using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace Bot1.Data.Entities
{
    public class RequestEntity
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }
        [Required]
        [Column("request_name")]
        public string RequestName { get; set; }
        [Column("location")]
        public string Location { get; set; }
        [Column("created_by")]
        public string CreatedBy { get; set; }
        [Column("expires")]
        public DateTime Expires { get; set; }
        [Required]
        [Column("guild_id")]
        public ulong guildId { get; set; }
    }
}
