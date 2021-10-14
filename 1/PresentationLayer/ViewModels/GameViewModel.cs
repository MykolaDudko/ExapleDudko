using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CourseWork.Models;

namespace CourseWork.ViewModels
{
    public class GameViewModel
    {
        public IEnumerable<PlayerModel> Players { get; set; }
        public IEnumerable<StadiumModel> Stadiums { get; set; }
    }
}
