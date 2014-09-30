using BullsAndCows.Data.Repositories;
using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BullsAndCows.Data
{
    public class BullsAndCowsData : IBullsAndCowsData
    {
        private DbContext context;
        private Dictionary<Type, object> repositories;

        public BullsAndCowsData()
            : this(new BullsAndCowsDbContext())
        {
        }

        public BullsAndCowsData(DbContext context)
        {
            this.context = context;
            this.repositories = new Dictionary<Type, object>();
        }

        public Repositories.IRepository<Game> Games
        {
            get { return this.GetRepository<Game>(); }
        }

        public Repositories.IRepository<Quess> Quesses
        {
            get { return this.GetRepository<Quess>(); }
        }

        public IRepository<Message> Messages
        {
            get { return this.GetRepository<Message>(); }
        }

        public IRepository<ApplicationUser> Users
        {
            get { return this.GetRepository<ApplicationUser>(); }
        }

        private IRepository<T> GetRepository<T>() where T : class
        {
            var typeOfRepository = typeof(T);
            if (!this.repositories.ContainsKey(typeOfRepository))
            {
                var newRepository = Activator.CreateInstance(typeof(Repository<T>), context);
                this.repositories.Add(typeOfRepository, newRepository);
            }

            return (IRepository<T>)this.repositories[typeOfRepository];
        }

        public int SaveChanges()
        {
            return this.context.SaveChanges();
        }
    }
}
