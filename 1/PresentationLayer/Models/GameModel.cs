using System;
using System.Collections.Generic;
using System.Text;
using AutoMapper;

namespace CourseWork.Models
{
    public class GameModel
    {
        public int Id { get; set; }
        public List<PlayerModel> Players { get; set; }
        public string Date { get; set; }
        public StadiumModel Place { get; set; }
        public int Spectators { get; set; }
        public Result GameResult { get; set; }
    }

    public enum Result
    {
        Won, Failed, Noone, NotPlayed
    }
}
