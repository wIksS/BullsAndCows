using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.Serialization;
using System.Web;

namespace BullsAndCows.UsersWCF.Models
{
    [DataContract]
    public class RankUserModel
    {
        public RankUserModel(ApplicationUser user)
        {
            this.Wins = user.Wins;
            this.Looses = user.Looses;
            this.Username = user.UserName;
            this.Rank = user.Rank;
            this.Id = user.Id;
        }

        //public static Expression<Func<ApplicationUser, RankUserModel>> FromModel
        //{
        //    get
        //    {
        //        return a => new RankUserModel
        //        {
        //            Id = a.Id,
        //            Username = a.UserName,
        //            Looses = a.Looses,
        //            Rank = a.Rank,
        //            Wins = a.Wins
        //        };
        //    }
        //}

        [DataMember]
        public string Id { get; set; }
      
        [DataMember]
        public int Looses { get; set; }

        [DataMember]
        public int Rank { get; set; }

        [DataMember]
        public string Username { get; set; }

        [DataMember]
        public int Wins { get; set; }
    }
}