using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bot1.Domain.Models
{
    public class AdminModel
    {
        public Guid Id { get; set; }
        public string UserId { get; set; }
        public ulong GuildId { get; set; }
    }
}
