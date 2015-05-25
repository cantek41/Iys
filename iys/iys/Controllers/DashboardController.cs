using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.DashboardWeb.Mvc;
using DevExpress.Web.Mvc;

namespace iys.Controllers
{
    public class DashboardController : Controller
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        [ValidateInput(false)]
        public ActionResult DashboardViewerPartial()
        {
            return PartialView("_DashboardViewerPartial", DashboardViewerSettings.Model);
        }
        public FileStreamResult DashboardViewerPartialExport()
        {
            return DevExpress.DashboardWeb.Mvc.DashboardViewerExtension.Export("DashboardViewer", DashboardViewerSettings.Model);
        }

        iys.ModelProject.iysContext db = new iys.ModelProject.iysContext();

        public ActionResult ChartPartial()
        {
            var model = db.USER_ANSWERS;
            return PartialView("_ChartPartial", model.ToList());
        }
	}
    class DashboardViewerSettings
    {
        public static DevExpress.DashboardWeb.Mvc.DashboardSourceModel Model
        {
            get
            {
                DashboardSourceModel model = new DashboardSourceModel();
                model.DashboardSource=typeof(Dashboard1);
                return model;
            }
        }
    }

}