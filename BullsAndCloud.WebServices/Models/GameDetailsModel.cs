using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using BullsAndCows.Models;

namespace BullsAndCloud.WebServices.Models
{
    public class GameDetailsModel
    {
        public GameDetailsModel(Game game, bool isBlue)
        {
            this.GameState = game.GameState.ToString();
            if (isBlue)
            {
                this.YourColor = "blue";
                this.YourNumber = game.BlueNumber;
                this.YourQuesses = game.BlueQuesses.AsQueryable().Select(QuessModel.FromModel).ToList();
                this.OpponentQuesses = game.RedQuesses.AsQueryable().Select(QuessModel.FromModel).ToList();
            }
            else
            {
                this.YourColor = "red";
                this.YourNumber = game.RedNumber;
                this.YourQuesses = game.RedQuesses.AsQueryable().Select(QuessModel.FromModel).ToList();
                this.OpponentQuesses = game.BlueQuesses.AsQueryable().Select(QuessModel.FromModel).ToList();
            }
            this.Name = game.Name;
            this.Id = game.Id;
            this.Red = game.RedUsername;
            this.Blue = game.BlueUsername;
        }
        public int Id { get; set; }

        public string Name { get; set; }

        public string Red { get; set; }

        public string Blue { get; set; }

        public string YourNumber { get; set; }

        public ICollection<QuessModel> YourQuesses { get; set; }

        public ICollection<QuessModel> OpponentQuesses { get; set; }

        public string YourColor { get; set; }

        public string GameState { get; set; }
    }
}