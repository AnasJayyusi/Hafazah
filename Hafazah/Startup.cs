using Hafazah.DAL;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin;
using Owin;
using Hangfire;
using Hangfire.SqlServer;
using System.Collections.Generic;
using System;
using System.Configuration;

[assembly: OwinStartupAttribute(typeof(Hafazah.Startup))]
namespace Hafazah
{
    public partial class Startup
    {
        private HafazahDbContext _dbContext;
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            CreateRolesandUsers();
            if (ConfigurationManager.AppSettings["HangfireConnection"] != "HangFireDataBaseNotExists")
            {
                app.UseHangfireAspNet(GetHangfireServers);
                app.UseHangfireDashboard();
                // Let's also create a sample background job
                RecurringJob.AddOrUpdate(() => TestRecurringJob(), "0 0 * * *");
            }
        }

        // In this method we will create default User roles and Admin user for login    
        private void CreateRolesandUsers()
        {
            _dbContext = new HafazahDbContext();

            // Creating first Admin Role and creating a default Admin User     
            if (!IsRoleExists("Admin"))
            {
                // Create Admin role
                CreateRole("Admin");
                // Add default User to Role Admin                  
                CreateUser("Administrator", "Anas_Jayyusi@outlook.com", "P@ssw0rd", "Admin");
            }

            // Creating Doctor role     
            if (!IsRoleExists("Teacher"))
            {
                CreateRole("Teacher");
            }

            //  Creating Reception role     
            if (!IsRoleExists("Student"))
            {
                CreateRole("Student");
            }
        }

        #region Helpers
        private bool IsRoleExists(string roleName)
        {
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext)))
            {
                return roleManager.RoleExists(roleName);
            }
        }
        public void CreateRole(string roleName)
        {
            using (var roleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(_dbContext)))
            {
                var role = new Microsoft.AspNet.Identity.EntityFramework.IdentityRole();
                role.Name = roleName;
                roleManager.Create(role);
            }
        }
        public void CreateUser(string username, string email, string password, string roleName)
        {
            var userManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(_dbContext));

            var user = new ApplicationUser();
            user.UserName = username;
            user.Email = email;

            var chkUser = userManager.Create(user, password);

            if (chkUser.Succeeded)
            {
                var result1 = userManager.AddToRole(user.Id, roleName);

            }
        }
        #endregion

        #region Hangfire
        private IEnumerable<IDisposable> GetHangfireServers()
        {

            GlobalConfiguration.Configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(ConfigurationManager.AppSettings["HangfireConnection"], new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });

            yield return new BackgroundJobServer();
        }

        public void TestRecurringJob()
        {

        }
        #endregion
    }
}
