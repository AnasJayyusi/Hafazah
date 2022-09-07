
using Hafazah.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System.Collections.Generic;
using System.Data.Entity;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Hafazah.DAL
{

    public class ApplicationUser : IdentityUser
    {
        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }
    }

    public class HafazahDbContext : IdentityDbContext<ApplicationUser>
    {
        public HafazahDbContext() : base("Hafazah", throwIfV1Schema: false)
        {
        }

        public static HafazahDbContext Create()
        {
            return new HafazahDbContext();
        }

        // DbSet here represent table in database

    }
}

public class HafazahDBInitializer : DropCreateDatabaseAlways<HafazahDbContext>
{

}
