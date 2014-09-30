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
    public class QuessController : BaseController
    {
        public QuessController(IBullsAndCowsData data)
            : base(data)
        {

        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult MakeQuess(int id, NumberModel input)
        {
            if (!ModelState.IsValid || input == null)
            {
                return this.BadRequest(ModelState);
            }

            var number = input.Number;

            if (!CheckIfNumberIsValid(number))
            {
                return BadRequest("Your number should have different digits");
            }
            var userId = this.User.Identity.GetUserId();
            var userName = this.User.Identity.GetUserName();
            var game = this.data.Games.Find(id);
            var user = this.data.Users.Find(userId);
            if (game.GameState == GameState.Finished)
            {
                return BadRequest("The game is over");
            }
            if (game.GameState == GameState.WaitingForOpponent)
            {
                return BadRequest("Waiting for opponent");
            }
            var quess = new Quess()
            {
                Game = game,
                GameId = game.Id,
                DateCreated = DateTime.Now,
                Number = number,
                UserId = userId,
                Username = userName,
            };

            var otherUser =this.data.Users.Find(game.RedId);
            if (userId == game.BlueId)
            {
                quess.BullsCount = GetBullsCount(quess.Number, game.RedNumber);
                quess.CowsCount = GetCowsCount(quess.Number, game.RedNumber);

                if (!(game.GameState == GameState.BlueInTurn))
                {
                    return BadRequest("You can play only when its your turn");
                }
                game.BlueQuesses.Add(quess);
                if (quess.CowsCount == 4)
                {
                    user.Wins++;
                    otherUser.Looses++;
                    var wonMessage = new Message()
                    {
                        Game = game,
                        Name = "You beat " + otherUser.UserName + "\"" + game.Name+"\"",
                        DateCreated = DateTime.Now,
                        Type=MessageTypes.GameWon,
                        Read = false,
                        GameId = game.Id,                        
                    };

                    var lostMessage = new Message()
                    {
                        Game = game,
                        Name = user.UserName + "beat you in game "+ "\"" + game.Name + "\"",
                        DateCreated = DateTime.Now,
                        Type = MessageTypes.GameWon,
                        Read = false,
                        GameId = game.Id,
                    };

                    game.GameState = GameState.Finished;
                    user.Messages.Add(wonMessage);
                    otherUser.Messages.Add(lostMessage);
                    //this.data.Messages.Add(wonMessage);
                  //  this.data.Messages.Add(lostMessage);
                   // this.data.Messages.SaveChanges();
                    this.data.Users.SaveChanges();
                }
                else
                {
                    game.GameState = GameState.RedInTurn;

                    var otherTurnMessage = new Message()
                    {
                        Game = game,
                        Name = "Ït is your turn in game " + "\"" + game.Name + "\"",
                        DateCreated = DateTime.Now,
                        Type = MessageTypes.YourTurn,
                        Read = false,
                        GameId = game.Id,
                    };

                    otherUser.Messages.Add(otherTurnMessage);
                    this.data.Users.SaveChanges();
                }


            }
            else if (userId == game.RedId)
            {
                quess.BullsCount = GetBullsCount(quess.Number, game.BlueNumber);
                quess.CowsCount = GetCowsCount(quess.Number, game.BlueNumber);

                if (!(game.GameState == GameState.RedInTurn))
                {
                    return BadRequest("You can play only when its your turn");
                }
                otherUser =this.data.Users.Find(game.BlueId);

                game.RedQuesses.Add(quess);
                if (quess.CowsCount == 4)
                {
                    user.Wins++;
                    otherUser.Looses++;
                    game.GameState = GameState.Finished;
                    var wonMessage = new Message()
                    {
                        Game = game,
                        Name = "You beat " + otherUser.UserName + "\"" + game.Name + "\"",
                        DateCreated = DateTime.Now,
                        Type = MessageTypes.GameWon,
                        Read = false,
                        GameId = game.Id,
                    };

                    var lostMessage = new Message()
                    {
                        Game = game,
                        Name = user.UserName + "beat you in game " + "\"" + game.Name + "\"",
                        DateCreated = DateTime.Now,
                        Type = MessageTypes.GameWon,
                        Read = false,
                        GameId = game.Id,
                    };

                    user.Messages.Add(wonMessage);
                    otherUser.Messages.Add(lostMessage);
                 //   this.data.Messages.Add(wonMessage);
                   // this.data.Messages.Add(lostMessage);
                    //this.data.Messages.SaveChanges();
                    this.data.Users.SaveChanges();
                }
                else
                {
                    game.GameState = GameState.BlueInTurn;

                    var otherTurnMessage = new Message()
                    {
                        Game = game,
                        Name = "Ït is your turn in game " + "\"" + game.Name + "\"",
                        DateCreated = DateTime.Now,
                        Type = MessageTypes.YourTurn,
                        Read = false,
                        GameId = game.Id,
                    };

                    otherUser.Messages.Add(otherTurnMessage);
                    this.data.Users.SaveChanges();
                }
            }
            else
            {
                return this.NotFound();
            }

            this.data.Quesses.Add(quess);
            this.data.Quesses.SaveChanges();
            this.data.Games.SaveChanges();

            return Ok(new QuessModel(quess));
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

        private int GetCowsCount(string quess, string number)
        {
            int cows = 0;
            for (int i = 0; i < quess.Length; i++)
            {
                if (quess[i] == number[i])
                {
                    cows++;
                }
            }

            return cows;
        }

        private int GetBullsCount(string quess, string number)
        {
            int bulls = 0;
            for (int i = 0; i < quess.Length; i++)
            {
                for (int j = 0; j < number.Length; j++)
                {
                    if (quess[i] == number[j])
                    {
                        bulls++;
                    }
                }
            }

            return bulls;
        }
    }
}
