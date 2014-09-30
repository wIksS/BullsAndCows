using BullsAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using BullsAndCloud.WebServices.Models;

namespace BullsAndCloud.WebServices.Controllers
{
    public class NotificationsController : BaseController
    {
        private const int pages = 10;
        public NotificationsController(IBullsAndCowsData data)
            : base(data)
        {
        }

        [HttpGet]
        [Authorize]
        public IHttpActionResult GetNotifications(int page = 0)
        {
            var user = this.data.Users.Find(User.Identity.GetUserId());

            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var messages = user.Messages.AsQueryable().Select(NotificationModel.FromModel)
                .OrderByDescending(m => m.State)
                .ThenBy(m => m.DateCreated)
                .Skip(pages * page)
                .Take(pages);

            //foreach (var item in messages)
            //{
            //    var message = this.data.Messages.Find(item.Id);
            //    message.Read = true;
            //    this.data.Messages.SaveChanges();
            //}
            return Ok(messages);
        }

        [HttpGet]
        [Authorize]
        [Route("api/notifications/next")]
        public IHttpActionResult Next()
        {
            var oldest = this.data.Messages.All().Where(m => m.Read == false).OrderByDescending(m => m.DateCreated)
                .Take(1).Select(NotificationModel.FromModel);
            var notification = this.data.Messages.Find(oldest.FirstOrDefault().Id);
            notification.Read = true;
            this.data.Messages.SaveChanges();
            return Ok(oldest);
        }
    }
}
