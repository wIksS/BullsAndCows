using BullsAndCloud.WebServices.Models;
using BullsAndCows.Data;
using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace BullsAndCloud.WebServices.Controllers
{
    [RoutePrefix("api/games")]
    public class GamesController : BaseController
    {
        private const int pages = 10;

        public GamesController(IBullsAndCowsData data)
            : base(data)
        {
        }

        [Authorize]
        [HttpGet]
        public IHttpActionResult GetGameDetails(int id)
        {
            var userId = this.User.Identity.GetUserId();
            var game = this.data.Games.Find(id);
            bool isBlue = false;

            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (userId == game.BlueId)
            {
                isBlue = true;
            }
            else if (userId != game.RedId)
            {
                return this.NotFound();
            }
            var modelGame = new GameDetailsModel(game, isBlue);

            return Ok(modelGame);
        }

        [Authorize]
        [HttpPut]
        public IHttpActionResult JoinGame(int id, NumberModel input)
        {
            var number = input.Number;
            var userId = this.User.Identity.GetUserId();
            var game = this.data.Games.Find(id);

            if (!CheckIfNumberIsValid(number))
            {
                return this.BadRequest("You cant have repeating digits in your number");
            }

            if (input == null || !ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (game.GameState != GameState.WaitingForOpponent)
            {
                return this.BadRequest("You can only join waiting games");
            }

            if (userId == game.RedId)
            {
                return this.BadRequest("You cant join your own game.");
            }
            
            game.BlueId = userId;
            game.BlueNumber = number;
            game.BlueUsername = this.User.Identity.GetUserName();
            game.GameState = GameState.RedInTurn;

            var redUser = this.data.Users.Find(game.RedId);
            redUser.Messages.Add(new Message
                {
                    DateCreated = DateTime.Now,
                    Game = game,
                    GameId = game.Id,
                    Read = false,
                    Type=MessageTypes.GameJoined,
                    Name = game.BlueUsername + "joined your game " + "\"" + game.Name + "\"",
                });
            this.data.Users.SaveChanges();
            this.data.Games.SaveChanges();

            return Ok(game.Name);
        }        

        [HttpGet]
        public IHttpActionResult GetGame(int page = 0)
        {
            var userId = this.User.Identity.GetUserId();

            if (!ModelState.IsValid)
            {
                return this.BadRequest(ModelState);
            }

            if (userId != null)
            {
                var authenticatedGames = this.data.Games.All()
                 .Where(g => g.GameState == GameState.WaitingForOpponent || g.BlueId == userId || g.RedId == userId)
                 .Select(GameViewModel.FromWaitingModel)
                 .OrderByDescending(g => g.GameState)
                 .ThenBy(g => g.Name)
                 .ThenBy(g => g.DateCreated)
                 .ThenBy(g => g.Red)
                 .Skip(pages * page)
                 .Take(pages);

                return Ok(authenticatedGames);
            }
            var games = GetGames(page);

            return Ok(games);
        }

        private IQueryable GetGames(int page)
        {
            return this.data.Games.All()
                 .Where(g => g.GameState == GameState.WaitingForOpponent)
                 .Select(GameViewModel.FromWaitingModel)
                 .OrderBy(g => g.GameState)
                 .ThenBy(g => g.Name)
                 .ThenBy(g => g.DateCreated)
                 .ThenBy(g => g.Red)
                 .Skip(pages * page)
                 .Take(pages);
        }




        [HttpPost]
        [Authorize]
        public IHttpActionResult PostGame(GameBindModel model)
        {
            var userId = this.User.Identity.GetUserId();
            var userName = this.User.Identity.GetUserName();
            if (!CheckIfNumberIsValid(model.Number))
            {
                return BadRequest();
            }

            var game = new Game
            {
                Name = model.Name,
                RedId = userId,
                RedUsername = userName,
                RedNumber = model.Number,
                GameState = GameState.WaitingForOpponent,
                DateCreated = DateTime.Now,
            };

            this.data.Games.Add(game);
            this.data.Games.SaveChanges();

            var resultModel = new GameViewModel(game);

            return Ok(resultModel);
        }

        private static bool CheckIfNumberIsValid(string number)
        {
            var set = new HashSet<Char>();

            for (int i = 0; i < number.Length; i++)
            {
                if (set.Contains(number[i]))
                {
                    return false;
                }
                set.Add(number[i]);
            }

            return true;
        }
    }
}
