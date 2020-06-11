using GameStore.Domain.Entities.Identity;
using GameStore.Domain.Entities.Store;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.EF
{
    public class StoreContext: IdentityDbContext<ApplicationUser>
    {
        public StoreContext(string conectionString) : base(conectionString) { }
        public StoreContext() : base("StoreContext") { }
        public DbSet<Game> Games { get; set; }
        public DbSet<Developer> Developers { get; set; }
        public DbSet<Feature> Features { get; set; }
        public DbSet<Genre> Genres { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<Review> Reviews { get; set; }
    }
}
