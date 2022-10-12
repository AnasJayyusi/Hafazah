
using Hafazah.DAL;
using Hafazah.Model;
using Hafazah.Model.Entities;
using Hafazah.Model.Entities.Common;
using Hafazah.Model.Entities.DropDownListOptions;
using Hafazah.Model.Entities.Program;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
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
        public virtual DbSet<Member> Members { get; set; }
        public virtual DbSet<Level> Levels { get; set; }
        public virtual DbSet<Country> Countries { get; set; }
        public virtual DbSet<EducationLevel> EducationLevels { get; set; }
        public virtual DbSet<LevelName> LevelNames { get; set; }
        public virtual DbSet<QuranMemorized> QuranMemorized { get; set; }
        public virtual DbSet<GlobalValue> GlobalValues { get; set; }
        public virtual DbSet<Localization> Localizations { get; set; }
    }
}

public class HafazahDBInitializer : DropCreateDatabaseAlways<HafazahDbContext>
{

}
