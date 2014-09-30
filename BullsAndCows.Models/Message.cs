using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Models
{
    public class Message
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public MessageTypes Type { get; set; }

        public bool Read { get; set; }

        public int GameId { get; set; }

        public virtual Game Game { get; set; }
    }
}
