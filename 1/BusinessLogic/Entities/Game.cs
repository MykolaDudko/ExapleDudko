using System;
using System.Collections.Generic;
using System.Text;

namespace BusinessLogic.Entities
{
    public class GameBL
    {
        public int Id { get; set; }
        public List<PlayerBL> Players { get; set; }
        public string Date { get; set; }
        public StadiumBL Place { get; set; }
        public int Spectators { get; set; }
        public Result GameResult { get; set; }
    }

    public enum Result
    {
        Won, Failed, Noone, NotPlayed
    }
}
