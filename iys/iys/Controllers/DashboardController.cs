using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.DashboardWeb.Mvc;
using DevExpress.Web.Mvc;

namespace iys.Controllers
{
    public class DashboardController : BaseController
    {
        //
        // GET: /Dashboard/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult dashboardPartial()
        {
            return PartialView("_dashboardPartial");
        }
        public ActionResult userInfoPartial()
        {
            iys.ModelProject.iysContext db = new ModelProject.iysContext();
            int model = 0;
            try
            {
                model = (int)db.USER_QUIZ_STATUSS.Where(x => x.USER_CODE == User.Identity.Name).Average(x => x.GRADE).Value; 
            }
            catch (Exception)
            {                
                
            }
           
                ////(from d in db.USER_QUIZ_STATUSS  
                //         where d.USER_CODE==User.Identity.Name
                //         select new succesVievModel { name = d.USER_CODE, puan = d.GRADE });
            succesVievModel sv = new succesVievModel();
            sv.name = User.Identity.Name;
            sv.puan = model;
            return PartialView("_userInfoPartial", sv);

        }
        public ActionResult calenderPartial()
        {
            iys.ModelProject.iysContext db = new ModelProject.iysContext();
            var model = from d in db.USER_QUIZ_STATUSS
                        where d.USER_CODE==User.Identity.Name
                        select new { name = d.DATE, puan = d.GRADE };
            return PartialView("_calenderPartial", model.ToList());
        }
        public ActionResult successPartial()
        {
            iys.ModelProject.iysContext db = new ModelProject.iysContext();
            var model = (from d in db.DOCUMENTS
                         from b in db.USER_QUIZ_STATUSS.Where(x => x.DOCUMENT_CODE == d.DOCUMENT_CODE).Where(dd=>dd.USER_CODE == User.Identity.Name).DefaultIfEmpty()  
                         orderby b.DATE descending
                         select new succesVievModel { name = d.DOCUMENT_NAME.Substring(0,10), puan = b.GRADE }).Take(20).ToList();
            return PartialView("_successPartial", model);
        }
        public ActionResult ChartPartial()
        {
            iys.ModelProject.iysContext db = new ModelProject.iysContext();
            var model = db.USER_ANSWERS;
            List<succesVievModel> scList = new List<succesVievModel>();
            succesVievModel scK = new succesVievModel();
            scK.name = "Kalan";
            scK.puan = db.DOCUMENTS.Count();
            succesVievModel scS = new succesVievModel();
            scS.name = "Biten";
            scS.puan = db.USER_QUIZ_STATUSS.Where(x=>x.USER_CODE==User.Identity.Name).Where(x => x.GRADE >= 50).Count();
            scK.puan = scK.puan - scS.puan;
            scList.Add(scK);
            scList.Add(scS);
            return PartialView("_ChartPartial", scList);
        }

    }

    public class succesVievModel
    {
        public string name { get; set; }
        public int? puan { get; set; }
    }

}