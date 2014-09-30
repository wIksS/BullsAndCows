using BullsAndCows.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using BullsAndCloud.WebServices.Models;

namespace BullsAndCloud.WebServices.Controllers
{
    public class ScoresController : BaseController
    {
        private const int pages = 10;
        public ScoresController(IBullsAndCowsData data)
            :base(data)
        {

        }

        [HttpGet]
        public IHttpActionResult GetScores(int page = 0)
        {
            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            var users = this.data.Users.All().Select(ScoreUserModel.FromModel)
                .OrderByDescending(a => a.Rank)
                .Skip(pages * page)
                .Take(10);

            return Ok(users);
        }
    }
}
