using GameStore.Domain.EF;
using GameStore.Domain.Entities.Identity;
using GameStore.Domain.Identity;
using GameStore.Domain.Interfaces;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameStore.Domain.Services
{
    public class UserService:IUserService
    {
        private StoreContext context;
        private ApplicationUserManager userManager;
        private ApplicationRoleManager roleManager;
        public UserService(string connectionString)
        {
            context = new StoreContext(connectionString);
        }
        public ApplicationUserManager UserManager
        {
            get
            {
                if (userManager == null)
                    userManager = new ApplicationUserManager(new UserStore<ApplicationUser>(context));
                return userManager;
            }
        }
        public ApplicationRoleManager RoleManager
        {
            get
            {
                if (roleManager == null)
                    roleManager = new ApplicationRoleManager(new RoleStore<ApplicationRole>(context));
                return roleManager;
            }
        }
        public void SaveAsync()
        {
            context.SaveChangesAsync();
        }
        private bool disposed = false;
        public virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    context.Dispose();
                }
                this.disposed = true;
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
    }
}
