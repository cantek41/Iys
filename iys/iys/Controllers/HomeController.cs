using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using iys.ModelProject;
using iys.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;

namespace iys.Controllers
{
    public class HomeController : BaseController
    {
        public ActionResult Index()
        {       

            using (var ctx = new iysContext())
            {
                return View();
            }
        }
        public UserManager<ApplicationUser> UserManager { get; private set; }
        public ActionResult Manege()
        {
          //  string[] rolesArray = Roles. GetRolesForUser();
            
                      
        //    IList<string> rol = UserManager.GetRoles(User.Identity.GetUserId());
            //string[] rol=Roles.GetUsersInRole("Admin");
           // string[] rol = r.GetRoles();
            //if (rolesArray.Contains("Admin"))
            //{
            //    return RedirectToAction("index", "AdminPanel");
            //}
            //else
            //{
            //    return RedirectToAction("index", "Dashboard");
            //}            
            return RedirectToAction("index", "Dashboard");
        }
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}