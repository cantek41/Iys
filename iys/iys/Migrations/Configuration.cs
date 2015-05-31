namespace iys.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using iys.Models;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;

    internal sealed class Configuration : DbMigrationsConfiguration<iys.Models.ApplicationDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
          
            ContextKey = "iys.Models.ApplicationDbContext";
        }

        protected override void Seed(iys.Models.ApplicationDbContext context)
        {
            if (!context.Roles.Any(x=>x.Name=="Admin"))
            {
                var roleStore = new RoleStore<IdentityRole>(context);
                var roleManager = new RoleManager<IdentityRole>(roleStore);

                var userStore = new UserStore<ApplicationUser>(context);
                var userManager = new UserManager<ApplicationUser>(userStore);

                var user = new ApplicationUser { UserName = "admin" };
                userManager.Create(user, "1q2w3e");

                roleManager.Create(new IdentityRole { Name = "Admin" });
                roleManager.Create(new IdentityRole { Name = "Student" });
                userManager.AddToRole(user.Id, "Admin");
                
            }
            
            //ApplicationDbContext db = new ApplicationDbContext();
            //RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
            //UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
            //var str = RoleManager.Create(new IdentityRole("Student"));
            //var adm = RoleManager.Create(new IdentityRole("Admin"));
            //var user = new ApplicationUser() { UserName = "Admin" };
            //var result = UserManager.Create(user, "1q2w3e");
            //base.Seed(context);
        }
    }
}
