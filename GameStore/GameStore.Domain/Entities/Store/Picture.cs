using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Entities.Store
{
    public class Picture
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public byte[] Image { get; set; }

        public Game Game { get; set; }
    }
}
