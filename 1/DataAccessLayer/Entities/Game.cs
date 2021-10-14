using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccessLayer.Entities
{
    public class Game
    {
        public int Id { get; set; }
        public List<Player> Players { get; set; }
        public string Date { get; set; }
        public Stadium Place { get; set; }
        public int Spectators { get; set; }
        public Result GameResult { get; set; }
    }
    
    public enum Result
    {
        Won, Failed, Noone, NotPlayed
    }
}
