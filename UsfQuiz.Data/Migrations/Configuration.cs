namespace UsfQuiz.Data.Migrations
{
    using Commons;
    using DataModels;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Configuration;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    public sealed class Configuration : DbMigrationsConfiguration<ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(ApplicationDbContext context)
        {
            var administratorUserName = ConfigurationManager.AppSettings["administratorUserName"];
            var administratorPassword = ConfigurationManager.AppSettings["administratorPassword"];
            var administratorEmail = ConfigurationManager.AppSettings["administratorEmail"];

            if (!context.Roles.Any(r => r.Name == GlobalConstants.Admin))
            {
                // Create admin role
                var roleStore = new RoleStore<IdentityRole>(context);
                var manager = new RoleManager<IdentityRole>(roleStore);
                var role = new IdentityRole { Name = GlobalConstants.Admin };
                manager.Create(role);
            }

            // Create admin user
            if (!context.Users.Any(u => u.UserName == administratorUserName))
            {
                var store = new UserStore<User>(context);
                var userManager = new UserManager<User>(store);
                var user = new User
                {
                    UserName = administratorUserName,
                    Email = administratorEmail,
                    FirstName = ConfigurationManager.AppSettings["adminFirstName"],
                    LastName = ConfigurationManager.AppSettings["adminLastName"]
                };

                userManager.Create(user, administratorPassword);

                // Assign user to admin role
                userManager.AddToRole(user.Id, GlobalConstants.Admin);
            }
        }
    }
}
