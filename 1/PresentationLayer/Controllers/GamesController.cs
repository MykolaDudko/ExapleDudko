using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using BusinessLogic;
using BusinessLogic.Entities;
using CourseWork.Models;
using AutoMapper;
using CourseWork.ViewModels;

namespace CourseWork.Controllers
{
    public class GamesController : Controller
    {
        public static int currentEditionID;

        public IActionResult AddPage()
        {
            using (var db = new BL())
            {
                GameViewModel model = new GameViewModel()
                {
                    Players = Mapper.Map<List<PlayerModel>>(db.GetPlayers()),
                    Stadiums = Mapper.Map<List<StadiumModel>>(db.GetStadiums())
                };

                return View(model);
            }
        }
        public IActionResult AddGame(string date, int spectators, int place, int[] players, string result)
        {
            List<PlayerModel> chosenPlayers = new List<PlayerModel>();
            StadiumModel chosenPlace = null;
            Models.Result chosenResult = 0;

            using (var db = new BL())
            {
                List<PlayerModel> listPlayers = Mapper.Map<List<PlayerModel>>(db.GetPlayers());

                // Forming the list of players from DataBase
                for (int i = 0; i < players.Length; i++)
                {
                    foreach (PlayerModel playerModel in listPlayers)
                    {
                        if (playerModel.Id == players[i])
                        {
                            chosenPlayers.Add(playerModel);
                            break;
                        }
                    }
                }

                // Getting the stadium by id from DataBase
                foreach (StadiumModel model in Mapper.Map<List<StadiumModel>>(db.GetStadiums()))
                {
                    if (model.Id == place)
                    {
                        chosenPlace = model;
                        break;
                    }
                }

                // Determining the result of the game by input string
                switch (result)
                {
                    case "Won":
                        chosenResult = Models.Result.Won;
                        break;

                    case "Failed":
                        chosenResult = Models.Result.Failed;
                        break;

                    case "Noone":
                        chosenResult = Models.Result.Noone;
                        break;

                    case "NotPlayed":
                        chosenResult = Models.Result.NotPlayed;
                        break;
                }

                // Changing state of involved players
                foreach (PlayerModel player in chosenPlayers)
                {
                    player.IsInGame = true;
                    db.UpdatePlayer(Mapper.Map<PlayerBL>(player));
                };

                // Forming game to DataBase
                GameModel game = new GameModel()
                {
                    Players = chosenPlayers,
                    Spectators = spectators,
                    Date = date,
                    Place = chosenPlace,
                    GameResult = chosenResult,
                };

                db.AddGame(Mapper.Map<GameBL>(game));
            }

            return Redirect("~/Home/Index");
        }

        //public IActionResult SearchPage()
        //{
        //    return View();
        //}
        //public IActionResult SearchResult(string name, string surname)
        //{
        //    List<PlayerModel> foundPlayers = new List<PlayerModel>();

        //    using (var db = new BL())
        //    {
        //        foreach (PlayerModel player in Mapper.Map<List<PlayerModel>>(db.GetPlayers()))
        //        {
        //            if (player.Name == name && player.Surname == surname)
        //                foundPlayers.Add(player);
        //        }
        //    }

        //    return View("ShowAllPage", foundPlayers);
        //}

        public IActionResult ShowAllPage()
        {
            List<GameModel> list = null;

            using (var db = new BL())
                list = Mapper.Map<List<GameModel>>(db.GetGames());

            return View(list);
        }
        //public IActionResult Delete(int id)
        //{
        //    using (var db = new BL())
        //        db.RemovePlayer(id);

        //    return Redirect("~/Home/Index");
        //}
        //public IActionResult DeleteAllPlayers()
        //{
        //    using (var db = new BL())
        //    {
        //        foreach (PlayerModel player in Mapper.Map<List<PlayerModel>>(db.GetPlayers()))
        //            db.RemovePlayer(player.Id);
        //    }

        //    return Redirect("~/Home/Index");
        //}

        //public IActionResult EditPage(int id)
        //{
        //    PlayerModel player = null;

        //    using (var db = new BL())
        //    {
        //        var players = Mapper.Map<List<PlayerModel>>(db.GetPlayers());
        //        foreach (PlayerModel model in players)
        //        {
        //            if (model.Id == id)
        //            {
        //                player = model;
        //                break;
        //            }
        //        }
        //    }

        //    if (player != null)
        //    {
        //        currentEditionID = player.Id;
        //        ViewData["PlayerName"] = player.Name;
        //        ViewData["PlayerSurname"] = player.Surname;
        //        ViewData["PlayerYear"] = player.Date;
        //        ViewData["PlayerHealth"] = player.IsHealthy;
        //        ViewData["PlayerSalary"] = player.Salary;
        //    }

        //    return View();
        //}
        //public IActionResult SaveChanges(int id, string name, string surname, string year, bool health, int salary)
        //{
        //    PlayerModel player = new PlayerModel()
        //    {
        //        Id = currentEditionID,
        //        Name = name,
        //        Surname = surname,
        //        Date = year,
        //        IsHealthy = health,
        //        Salary = salary
        //    };

        //    using (var db = new BL())
        //        db.UpdatePlayer(Mapper.Map<PlayerBL>(player));

        //    return Redirect("~/Home/Index");
        //}
    }
}
