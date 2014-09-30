using BullsAndCows.Data;
using BullsAndCows.Models;
using BullsAndCows.UsersWCF.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace BullsAndCows.UsersWCF
{
    // NOTE: You can use the "Rename" command on the "Refactor" menu to change the class name "users" in code, svc and config file together.
    // NOTE: In order to launch WCF Test Client for testing this service, please select users.svc or users.svc.cs at the Solution Explorer and start debugging.
    public class users : Iusers
    {
        private IBullsAndCowsData data;
        private const int pages = 10;

        public users()
            :this(new BullsAndCowsData(BullsAndCowsDbContext.Create()))
        {

        }
        public users(IBullsAndCowsData data)
        {
            this.data = data;
        }
        public IEnumerable<UserModel> GetUsers(int page = 0)
        {
            var users = this.data.Users.All().Select(a => new UserModel
                {
                    Id = a.Id,
                    Username = a.UserName
                })
                .OrderBy(a => a.Username)
                .Skip(page * pages)
                .Take(pages);

            return users;
        }


        public RankUserModel GetUser(string id)
        {
            var user = this.data.Users.Find(id);

            var modelUser = new RankUserModel(user);

            return modelUser;
        }
    }
}
