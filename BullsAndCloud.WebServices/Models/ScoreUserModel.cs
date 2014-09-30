using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BullsAndCloud.WebServices.Models
{
    public class ScoreUserModel
    {
        public static Expression<Func<ApplicationUser,ScoreUserModel>> FromModel
        {
            get
            {
                return a => new ScoreUserModel
                    {
                        Rank = a.Rank,
                        Username = a.UserName
                    };
            }
        }

        public string Username { get; set; }

        public int Rank { get; set; }
    }
}