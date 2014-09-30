using BullsAndCows.Data.Migrations;
using BullsAndCows.Models;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Data
{
    public class BullsAndCowsDbContext : IdentityDbContext<ApplicationUser>
    {
        public BullsAndCowsDbContext()
            : base("BullsAndCowsConnection")
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<BullsAndCowsDbContext, Configuration>());
        }

        public static BullsAndCowsDbContext Create()
        {
            return new BullsAndCowsDbContext();
        }

        public IDbSet<Game> Games { get; set; }

        public IDbSet<Quess> Quesses { get; set; }

        public IDbSet<Message> Messages { get; set; }

        //public IDbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
