using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BullsAndCows.UsersWCF.Models
{
    public class UserModel
    {
        public static Expression<Func<ApplicationUser,UserModel>> FromModel
        {
            get
            {
                return a => new UserModel
                {
                    Id = a.Id,
                    Username = a.UserName
                };
            }
        }

        public string Id { get; set; }

        public string Username { get; set; }
    }
}