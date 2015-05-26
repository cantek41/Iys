using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iys.ModelProject;

namespace iys.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AdminPanelController : BaseController
    {
        //
        // GET: /AdminPanel/
        public ActionResult Index()
        {
            iysContext db = new iysContext();
            var model = from d in db.COURSES
                        select d;
            return View(model.ToArray());
        }

        public ActionResult MasterDetail()
        {
            iysContext db = new iysContext();
            var model = from d in db.COURSES
                        select d;
            return View(model);
        }
        public ActionResult MasterDetailMasterPartial()
        {
            iysContext db = new iysContext();
            var model = from d in db.COURSES
                        select d;
            return PartialView("MasterDetailMasterPartial", model.ToArray());
        }
        public ActionResult MasterDetailDetailPartial(string customerID)
        {

            ViewData["COURSE_CODE"] = customerID;
            int cID = Convert.ToInt32(customerID);
            iysContext db = new iysContext();
            var model = from d in db.CHAPTERS
                        where d.COURSE_CODE == cID
                        select d;
            return PartialView("MasterDetailDetailPartial", model.ToArray());
        }
    }
}