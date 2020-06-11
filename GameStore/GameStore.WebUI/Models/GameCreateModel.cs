using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GameStore.WebUI.Models
{
    public class GameCreateModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; } 
        public string Description { get; set; }
        public string Language { get; set; }
        public string Developer  { get; set; }
        public string Genres { get; set; }
        public string Features { get; set; }
        public string Reviews { get; set; }
        public DateTime ReleaseDate { get; set; }
    }
}