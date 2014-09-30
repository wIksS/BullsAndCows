using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Models
{
    public class Game
    {
        private ICollection<Quess> blueQuesses;
        private ICollection<Quess> redQuesses;

        public Game()
        {
            this.BlueQuesses = new HashSet<Quess>();
            this.RedQuesses = new HashSet<Quess>();
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public DateTime DateCreated { get; set; }

        public string RedId { get; set; }

        public string BlueId { get; set; }

        public string BlueUsername { get; set; }

        public string RedUsername { get; set; }

        public string RedNumber { get; set; }

        public string BlueNumber { get; set; }

        public GameState GameState { get; set; }

        public virtual ICollection<Quess> BlueQuesses
        {
            get { return this.blueQuesses; }
            set { this.blueQuesses = value; }
        }

        public virtual ICollection<Quess> RedQuesses
        {
            get { return this.redQuesses; }
            set { this.redQuesses = value; }
        }
    }
}
