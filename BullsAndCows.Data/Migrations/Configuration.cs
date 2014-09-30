namespace BullsAndCows.Data.Migrations
{
    using BullsAndCows.Models;
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<BullsAndCowsDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
            AutomaticMigrationDataLossAllowed = true;
        }

        protected override void Seed(BullsAndCows.Data.BullsAndCowsDbContext context)
        {
            //context.Games.Add(new Game
            //    {
            //        Name = "Test delete pls",
            //        DateCreated = DateTime.Now
            //    });
            //context.SaveChanges();
        }
    }
}
