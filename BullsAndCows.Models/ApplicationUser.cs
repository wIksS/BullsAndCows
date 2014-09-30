using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Models
{
    public class ApplicationUser : IdentityUser
    {
        private int wins;
        private int looses;
        private ICollection<Message> messages;

        public ApplicationUser()
        {
            this.Messages = new HashSet<Message>();
        }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public int Rank { get; set; }

        public int Wins
        {
            get { return this.wins; }
            set
            {
                this.wins = value; 
                CalculateRank();
            }
        }

        public int Looses
        {
            get { return this.looses; }
            set
            {
                this.looses = value;
                CalculateRank();
            }
        }

        private void CalculateRank()
        {
            this.Rank = 100 * this.wins + 15 * this.looses;
        }

        public virtual ICollection<Message> Messages
        {
            get { return this.messages; }
            set { this.messages = value; }
        }
    }
}
