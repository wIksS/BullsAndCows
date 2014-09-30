using BullsAndCows.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Web;

namespace BullsAndCloud.WebServices.Models
{
    public class GameViewModel
    {
        public GameViewModel()
        {

        }
        public GameViewModel(Game game)
        {
            this.Blue = GetBlueUsername(game.BlueUsername);
            this.Red = game.RedUsername;
            this.Name = game.Name;
            this.Id = game.Id;
            this.DateCreated = DateTime.Now;
            this.GameState = game.GameState.ToString();
        }

        public static Expression<Func<Game, GameViewModel>> FromModel
        {
            get
            {
                return g => new GameViewModel
                    {
                        Id = g.Id,
                        Name = g.Name,
                        DateCreated = DateTime.Now,
                        Blue = g.BlueUsername,
                        Red = g.RedUsername,
                        GameState = g.GameState.ToString()
                    };
            }
        }

        public static Expression<Func<Game, GameViewModel>> FromWaitingModel
        {
            get
            {
                return g => new GameViewModel
                {
                    Id = g.Id,
                    Name = g.Name,
                    DateCreated = DateTime.Now,
                    Blue = g.BlueUsername ?? "No blue player yet",
                    Red = g.RedUsername,
                    GameState = g.GameState.ToString()
                };
            }
        }

        private static string GetBlueUsername(string username)
        {
            if (username == null)
            {
                return "No blue player yet";
            }
            else
            {
                return username;
            }
        }

        public int Id { get; set; }

        public string Name { get; set; }

        public string Blue { get; set; }

        public string Red { get; set; }

        public DateTime DateCreated { get; set; }

        public string GameState { get; set; }

    }
}