using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BullsAndCloud.WebServices.Models
{
    public class QuessModel
    {
        public QuessModel()
        {
        }

        public QuessModel(Quess quess)
        {
            this.GameId = quess.GameId;
            this.BullsCount = quess.BullsCount;
            this.CowsCount = quess.CowsCount;
            this.Id = quess.Id;
            this.Number = quess.Number;
            this.Username = quess.Username;
            this.UserId = quess.UserId;
            this.DateMade = quess.DateCreated;
        }

        public static Expression<Func<Quess, QuessModel>> FromModel
        {
            get
            {
                return q => new QuessModel
                {
                    DateMade = DateTime.Now,
                    GameId = q.GameId,
                    Id = q.Id,
                    UserId = q.UserId,
                    Username = q.Username,
                    Number = q.Number,
                    BullsCount = q.BullsCount,
                    CowsCount = q.CowsCount
                };                
            }
        }

        public int Id { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public int GameId { get; set; }

        public string Number { get; set; }

        public DateTime DateMade { get; set; }

        public int CowsCount { get; set; }

        public int BullsCount { get; set; }
    }
}