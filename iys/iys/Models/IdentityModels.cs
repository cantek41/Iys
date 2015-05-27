using Microsoft.AspNet.Identity.EntityFramework;
using System.Data.Entity;
using Microsoft.AspNet.Identity;

namespace iys.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit http://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
    }

    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection")
        {
            //    Database.SetInitializer<ApplicationDbContext>(new DBInitializer());

        }

        public class DBInitializer : DropCreateDatabaseAlways<ApplicationDbContext>
        {
            protected override void Seed(ApplicationDbContext context)
            {
                ApplicationDbContext db = new ApplicationDbContext();
                RoleManager<IdentityRole> RoleManager = new RoleManager<IdentityRole>(new RoleStore<IdentityRole>(new ApplicationDbContext()));
                UserManager<ApplicationUser> UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(new ApplicationDbContext()));
                var str = RoleManager.Create(new IdentityRole("Student"));
                var adm = RoleManager.Create(new IdentityRole("Admin"));
                var user = new ApplicationUser() { UserName = "Admin" };
                var result = UserManager.Create(user, "1q2w3e");
                base.Seed(context);
            }            
        }




    }

}