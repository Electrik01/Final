using GameStore.Domain.Entities.Store;
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
    [Authorize]
    [Authorize(Roles = "admin")]
    public class AdminController : Controller
    {
        private IUserService UserService
        {
            get
            {
                return HttpContext.GetOwinContext().GetUserManager<IUserService>();
            }
        }
        StoreService store = new StoreService("StoreContext");

        public ActionResult Index()
        {
            string[] Date = 
            {
                DateTime.Today.ToShortDateString(),
                DateTime.Today.AddMonths(-1).ToShortDateString(),
                DateTime.Today.AddMonths(-2).ToShortDateString(),
                DateTime.Today.AddMonths(-3).ToShortDateString(),
                DateTime.Today.AddMonths(-4).ToShortDateString(),
                DateTime.Today.AddMonths(-5).ToShortDateString(),
                DateTime.Today.AddMonths(-6).ToShortDateString(),
                DateTime.Today.AddMonths(-7).ToShortDateString(),
                DateTime.Today.AddMonths(-8).ToShortDateString(),
                DateTime.Today.AddMonths(-9).ToShortDateString(),
                DateTime.Today.AddMonths(-10).ToShortDateString(),
                DateTime.Today.AddMonths(-11).ToShortDateString()

            };
            var orders = store.Orders.Get();
            int[] Count =
            {
                orders.Where(o=>o.Date.Month==DateTime.Today.Month && o.Date.Year==DateTime.Today.Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-1).Month && o.Date.Year==DateTime.Today.AddMonths(-1).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-2).Month && o.Date.Year==DateTime.Today.AddMonths(-2).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-3).Month && o.Date.Year==DateTime.Today.AddMonths(-3).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-4).Month && o.Date.Year==DateTime.Today.AddMonths(-4).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-5).Month && o.Date.Year==DateTime.Today.AddMonths(-5).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-6).Month && o.Date.Year==DateTime.Today.AddMonths(-6).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-7).Month && o.Date.Year==DateTime.Today.AddMonths(-7).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-8).Month && o.Date.Year==DateTime.Today.AddMonths(-8).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-9).Month && o.Date.Year==DateTime.Today.AddMonths(-9).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-10).Month && o.Date.Year==DateTime.Today.AddMonths(-10).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-11).Month && o.Date.Year==DateTime.Today.AddMonths(-11).Year).Count(),
            };
            ViewBag.Date = Date;
            ViewBag.Count = Count;

            List<string> Genres = store.Genres.Get().Select(g => g.Name).ToList();
            List<int> CountGenres = new List<int>();
            foreach(var item in Genres)
            {
                CountGenres.Add(store.Orders.Get().Where(o => o.Game.Genres.Select(g => g.Name.Contains(item)).Contains(true)).Count());
            }
            ViewBag.Genres = Genres;
            ViewBag.CountGenr = CountGenres;
            List<int> CountOplata = new List<int>()
            {
                store.Orders.Find(o=>o.Oplata=="Банковская карта").Count(),
                store.Orders.Find(o=>o.Oplata=="QIWI").Count(),
                store.Orders.Find(o=>o.Oplata=="WebMoney").Count()

            };
            ViewBag.CountOplata = CountOplata;
            return View();
        }
        
        public ActionResult Games()
        {
            var games = store.Games.Get();

            return View(games);
        }
        public ActionResult Search(string searchLine="")
        {
            var games = store.Games.Find(g => g.Name.ToLower().Contains(searchLine.ToLower()));
            return PartialView("ListGames", games.ToList());
        }

        public ActionResult Game(string name)
        {
            string[] Date =
            {
                DateTime.Today.ToShortDateString(),
                DateTime.Today.AddMonths(-1).ToShortDateString(),
                DateTime.Today.AddMonths(-2).ToShortDateString(),
                DateTime.Today.AddMonths(-3).ToShortDateString(),
                DateTime.Today.AddMonths(-4).ToShortDateString(),
                DateTime.Today.AddMonths(-5).ToShortDateString()
            };
            Game game = store.Games.Find(g => g.Name.Replace(" ", "").ToLower() == name).FirstOrDefault();
            var orders = game.Orders;
            int[] Count =
            {
                orders.Where(o=>o.Date.Month==DateTime.Today.Month && o.Date.Year==DateTime.Today.Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-1).Month && o.Date.Year==DateTime.Today.AddMonths(-1).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-2).Month && o.Date.Year==DateTime.Today.AddMonths(-2).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-3).Month && o.Date.Year==DateTime.Today.AddMonths(-3).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-4).Month && o.Date.Year==DateTime.Today.AddMonths(-4).Year).Count(),
                orders.Where(o=>o.Date.Month==DateTime.Today.AddMonths(-5).Month && o.Date.Year==DateTime.Today.AddMonths(-5).Year).Count()
            };
            ViewBag.Date = Date;
            ViewBag.Count = Count;
            return View(store.Games.Find(g => g.Name.Replace(" ", "").ToLower() == name).FirstOrDefault());
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost, ValidateInput(false)]

        public ActionResult Create(GameCreateModel game, IEnumerable<HttpPostedFileBase> uploads)
        {
            
            List<Picture> pictures = new List<Picture>();
            foreach (var file in uploads)
            {
                if (file != null)
                {
                    byte[] imageData = null;
                    // считываем переданный файл в массив байтов
                    using (var binaryReader = new BinaryReader(file.InputStream))
                    {
                        imageData = binaryReader.ReadBytes(file.ContentLength);
                    }
                    pictures.Add(new Picture { Image = imageData, Name = file.FileName });
                }
            }
            // установка массива байтов

            Game gameNew = new Game()
            {
                Name = game.Name,
                Description = game.Description,
                Price = game.Price,
                Language = game.Language,
                Discount = game.Discount,
                Pictures = pictures,
                ReleaseDate = game.ReleaseDate

            };
            if (store.Developer.Find(d => d.Name == game.Developer).FirstOrDefault() == null)
                gameNew.Developer = new Developer { Name = game.Developer };
            else
                gameNew.Developer = store.Developer.Find(d => d.Name == game.Developer).FirstOrDefault();
            ///
            if (store.Genres.Find(g => game.Genres.Contains(g.Name)).FirstOrDefault() == null)
            {
                ModelState.AddModelError("Genres", "Ошибка");
                return View(game);

            }
            else
                gameNew.Genres = store.Genres.Find(g => game.Genres.Contains(g.Name)).ToList();
            ///
            if (store.Features.Find(f => game.Features.Contains(f.Name)).FirstOrDefault() == null)
            {
                ModelState.AddModelError("Features", "Ошибка");
                return View(game);
            }
            else
                gameNew.Features = store.Features.Find(f => game.Features.Contains(f.Name)).ToList();
            ///

            store.Games.Create(gameNew);
            return View();
        }
        public ActionResult Update(string name)
        {
            return View(store.Games.Find(g => g.Name == name).FirstOrDefault());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult Update(Game game)
        {
            Game gameNew = store.Games.Find(g => g.Name == game.Name).FirstOrDefault();
            gameNew.Description = game.Description;
            gameNew.Name = game.Name;
            gameNew.Price = game.Price;
            gameNew.ReleaseDate = game.ReleaseDate;
            gameNew.Language = game.Language;
            store.Games.Update(gameNew);
            return RedirectToAction("Games");
        }
        public ActionResult Delete(string name)
        {
            var game = store.Games.Find(g => g.Name == name).FirstOrDefault();
            store.Pictures.RemoveAll(game.Pictures);
            store.Games.Remove(game);
            return RedirectToAction("Games");       
        }
    }
}