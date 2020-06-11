using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Store
{
    public class Feature
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<Game> Games { get; set; }//игры с данной особенностью
    }
}
