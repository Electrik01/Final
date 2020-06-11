using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Store
{
    public class Genre
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; } //список всех игр, которые имеют данные жанр
    }
}
