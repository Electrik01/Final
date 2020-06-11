using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Store
{
    public class Game
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Price { get; set; }
        public int? Discount { get; set; } //скидка - может отсутствовать
        public string Description { get; set; }
        public string Language { get; set; }
        public DateTime ReleaseDate { get; set; }

        public virtual Developer Developer { get; set; }//Разработчик создавший игру
        public virtual ICollection<Genre> Genres { get; set; }//Список жанров
        public virtual ICollection<Feature> Features { get; set; }//Список особенностей
        public virtual ICollection<Review> Reviews { get; set; }//Все отзывы на данную игру 
        public virtual ICollection<Picture> Pictures { get; set; }
        public virtual ICollection<Order> Orders{ get; set; }


    }
}
