using GameStore.Domain.EF;
using GameStore.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Services
{
        public class ServiceCreator : IServiceCreator
        {
            public IUserService CreateUserService(string connection)
            {
                return new UserService(connection);
            }
        }
}
