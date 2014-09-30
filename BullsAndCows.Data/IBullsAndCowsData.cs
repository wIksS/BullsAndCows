using BullsAndCows.Data.Repositories;
using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BullsAndCows.Data
{
    public interface IBullsAndCowsData
    {
        IRepository<Game> Games { get; }

        IRepository<Quess> Quesses { get; }

        IRepository<Message> Messages { get; }

        IRepository<ApplicationUser> Users { get; }        

    }
}
