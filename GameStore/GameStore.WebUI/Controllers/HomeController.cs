using GameStore.Domain.EF;
using GameStore.Domain.Entities.Store;//del
using GameStore.Domain.Interfaces;
using GameStore.Domain.Services;
using GameStore.WebUI.Models;
using Microsoft.AspNet.Identity.Owin;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GameStore.WebUI.Controllers
{
    public class HomeController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        StoreService store = new StoreService("StoreContext");

        public ActionResult Catalog()
        {
            List<GameShortModel> games = new List<GameShortModel>();
            foreach (var game in store.Games.Get())
            {
                games.Add(new GameShortModel
                {
                    Name = game.Name,
                    Picture = game.Pictures.Skip(1).FirstOrDefault().Image,
                    Price = game.Price,
                    Discount = game.Discount,
                    Genres = game.Genres
                   
                });
            }
            return View(games);
        }

        public ActionResult Index()
        {
            MainGames games = new MainGames();
            foreach (var gameData in store.Games.Get().OrderByDescending(g=>g.Orders.Count).Take(5))
            {

                games.Leader.Add(new GameShortModel()
                    {
                        Discount = gameData.Discount,
                        Genres = gameData.Genres,
                        Name = gameData.Name,
                        Price = gameData.Price,
                        Picture = gameData.Pictures.FirstOrDefault().Image
                    }
                );
            }
            foreach (var gameData in store.Games.Get().OrderByDescending(g => g.ReleaseDate).Take(5))
            {

                games.NewGames.Add(new GameShortModel()
                {
                    Discount = gameData.Discount,
                    Genres = gameData.Genres,
                    Name = gameData.Name,
                    Price = gameData.Price,
                    Picture = gameData.Pictures.FirstOrDefault().Image
                }
                );
            }
            foreach (var gameData in store.Games.Get().OrderByDescending(g => g.Orders.Count).Where(g=>g.ReleaseDate>DateTime.Today.AddMonths(-1)).Take(5))
            {

                games.CarouselGames.Add(new GameShortModel()
                {
                    Discount = gameData.Discount,
                    Genres = gameData.Genres,
                    Name = gameData.Name,
                    Price = gameData.Price,
                    Picture = gameData.Pictures.FirstOrDefault().Image
                }
                );
            }
            if (games.Leader.Count != 0)
                return View(games);
            else
                return HttpNotFound();
        }
        /* 
              <compilation debug="true" targetFramework="4.7.1" />
    <httpRuntime targetFramework="4.7.1" />
    <httpModules>
      <add name="ApplicationInsightsWebTracking" type="Microsoft.ApplicationInsights.Web.ApplicationInsightsHttpModule, Microsoft.AI.Web" />
    </httpModules>
    */
       

        public ActionResult Game(string name)
        {
            var game = store.Games.Find(g => g.Name.Replace(" ", "").ToLower() == name).FirstOrDefault();
            return View(game);
        }

      
        public ActionResult GameGFS(List<string> featuresS, List<string> genresS,string sort="")
        {
            var games = store.Games.Get();
            var countFC = featuresS == null ? 0 : featuresS.Count;
            var countGC = genresS == null ? 0 : genresS.Count;

            List<GameShortModel> games_short = new List<GameShortModel>();
            if (featuresS != null || genresS != null)
            {
                foreach (var game in games)
                {
                    int countG = 0,
                        countF = 0;
                    if (genresS != null)
                    foreach (var genre in game.Genres)
                    {
                        if (genresS.Contains(genre.Name))
                            countG++;
                    }
                    if (featuresS != null)
                    foreach (var feature in game.Features)
                    {
                        if (featuresS.Contains(feature.Name))
                            countF++;
                    }
                    if (countF == countFC && countG == countGC)
                        games_short.Add(new GameShortModel
                        {
                            Name = game.Name,
                            Picture = game.Pictures.Skip(1).FirstOrDefault().Image,
                            Price = game.Price,
                            Discount = game.Discount,
                            Genres = game.Genres
                        });

                }
            }
            else
            {
                foreach (var game in games)
                    games_short.Add(new GameShortModel
                {
                    Name = game.Name,
                    Picture = game.Pictures.Skip(1).FirstOrDefault().Image,
                    Price = game.Price,
                    Discount = game.Discount,
                    Genres = game.Genres,
                    ReleaseDate=game.ReleaseDate
                    });
            }
            switch (sort)
            {
                case "Цена (по убыванию)":
                    games_short = games_short.OrderByDescending(g => g.Price).ToList();
                    break;
                case "Цена (по возрастанию)":
                    games_short = games_short.OrderBy(g => g.Price).ToList();
                    break;
                case "Дате релиза (по убыванию)":
                    games_short = games_short.OrderByDescending(g => g.ReleaseDate).ToList();
                    break;
                case "Дате релиза (по возрастанию)":
                    games_short = games_short.OrderBy(g => g.ReleaseDate).ToList();
                    break;
            }
            return PartialView("GCatalog",games_short);
        }
        
        public ActionResult Buy(string name)
        {

            return View(store.Games.Find(g => g.Name.Replace(" ", "").ToLower() == name).FirstOrDefault());
        }
        [HttpPost]
        public ActionResult Buy(string Name,string emailConfirm, string email, string oplata)
        {
            Game gameForReturn = store.Games.Find(g => g.Name == Name).FirstOrDefault();
            if (emailConfirm!=email)
            {
                ModelState.AddModelError("emailConfirm", "Почты не совпадают");
                
                return View(gameForReturn);
            }
            store.Orders.Create(new Order
            {
                Game = gameForReturn,
                Email = email,
                Oplata = oplata,
                Date = DateTime.Today
                
            });
            return View("Success");
        }
    }
    
}
