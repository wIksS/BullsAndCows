using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Models
{
    public class Quess
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public virtual Game Game{ get; set; }

        public int GameId { get; set; }

        public string Number { get; set; }

        public DateTime DateCreated { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}
