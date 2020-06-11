using GameStore.Domain.EF;
using GameStore.Domain.Entities.Store;
using GameStore.Domain.Interfaces;
using GameStore.Domain.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Services
{
    public class StoreService
    {
        private StoreContext db;
        public StoreService(string connectionString)
        {
            db = new StoreContext(connectionString);    
        }
        private GenericRepository<Game> GamesRepository;
        private GenericRepository<Developer> DevelopersRepository;
        private GenericRepository<Feature> FeaturesRepository;
        private GenericRepository<Genre> GenresRepository;
        private GenericRepository<Order> OrdersRepository;
        private GenericRepository<Review> ReviewsRepository;
        private GenericRepository<Picture> PicturesRepository;

        public IGenericRepository<Review> Reviews
        {
            get
            {
                if(ReviewsRepository == null)
                {
                    ReviewsRepository = new GenericRepository<Review>(db);
                }
                return ReviewsRepository;
            }
        }
        public IGenericRepository<Picture> Pictures
        {
            get
            {
                if (PicturesRepository == null)
                {
                    PicturesRepository = new GenericRepository<Picture>(db);
                }
                return PicturesRepository;
            }
        }
        public IGenericRepository<Game> Games
        {
            get
            {
                if (GamesRepository == null)
                {
                    GamesRepository = new GenericRepository<Game>(db);
                }
                return GamesRepository;
            }
        }
        public IGenericRepository<Developer> Developer
        {
            get
            {
                if (DevelopersRepository == null)
                {
                    DevelopersRepository = new GenericRepository<Developer>(db);
                }
                return DevelopersRepository;
            }
        }
        public IGenericRepository<Feature> Features
        {
            get
            {
                if (FeaturesRepository == null)
                {
                    FeaturesRepository = new GenericRepository<Feature>(db);
                }
                return FeaturesRepository;
            }
        }
        public IGenericRepository<Genre> Genres
        {
            get
            {
                if (GenresRepository == null)
                {
                    GenresRepository = new GenericRepository<Genre>(db);
                }
                return GenresRepository;
            }
        }
        public IGenericRepository<Order> Orders
        {
            get
            {
                if (OrdersRepository == null)
                {
                    OrdersRepository = new GenericRepository<Order>(db);
                }
                return OrdersRepository;
            }
        }
    }
}
    