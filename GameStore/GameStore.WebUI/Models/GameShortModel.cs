using GameStore.Domain.Entities.Store;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class GameShortModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; }
        public byte[] Picture { get; set; }
        public ICollection<Genre> Genres { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}