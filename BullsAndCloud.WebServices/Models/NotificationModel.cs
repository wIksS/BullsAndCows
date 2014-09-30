using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BullsAndCows.Models;
using System.Linq.Expressions;

namespace BullsAndCloud.WebServices.Models
{
    public class NotificationModel
    {
        public static Expression<Func<Message, NotificationModel>> FromModel
        {
            get
            {
                return m => new NotificationModel
                {
                    DateCreated = m.DateCreated,
                    GameId = m.GameId,
                    Id = m.Id,
                    Message = m.Name,
                    State = m.Read == false ? "Unread" : "Read",
                    Type = m.Type.ToString(),
                };
            }
        }

        public int Id { get; set; }

        public string Message { get; set; }

        public DateTime DateCreated { get; set; }

        public string Type { get; set; }

        public object State { get; set; }

        public int GameId { get; set; }
    }
}