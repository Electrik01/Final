using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class MainGames
    {
        public MainGames()
        {
            Leader = new List<GameShortModel>();
            NewGames = new List<GameShortModel>();
            CarouselGames = new List<GameShortModel>();
        }
        public List<GameShortModel> Leader { get; set; }
        public List<GameShortModel> NewGames { get; set; }
        public List<GameShortModel> CarouselGames { get; set; }
    }
}