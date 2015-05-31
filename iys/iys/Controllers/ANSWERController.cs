using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace iys.Controllers
{
    public class ANSWERController : AdminBaseController
    {
        //
        // GET: /ANSWER/
        public ActionResult Index()
        {
            return View();
        }

        iys.ModelProject.iysContext db = new iys.ModelProject.iysContext();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.ANSWERS;
            return PartialView("_GridViewPartial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(iys.ModelProject.ANSWER item)
        {
            var model = db.ANSWERS;
            //if (ModelState.IsValid)
            //{
                try
                {
                    //item.USER_CODE =
                    //item.QUESTION_CODE = 
                    //item.RESULT_ID =
                    //item.RESULT_TEXT =
                    //item.ACTIVITY_CODE =
                    item.CREATE_USER = getCurrentUserName();
                    item.CREATE_DATE = DateTime.Now;
                    item.LAST_UPDATE = DateTime.Now;
                    item.LAST_UPDATE_USER = getCurrentUserName();
                    model.Add(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            //}
            //else
            //    ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(iys.ModelProject.ANSWER item)
        {
            var model = db.ANSWERS;
            if (ModelState.IsValid)
            {
                try
                {
                    item.LAST_UPDATE = DateTime.Now;
                    item.LAST_UPDATE_USER = getCurrentUserName();
                    var modelItem = model.FirstOrDefault(it => it.USER_CODE == item.USER_CODE);
                    if (modelItem != null)
                    {
                        this.UpdateModel(modelItem);
                        db.SaveChanges();
                    }
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            else
                ViewData["EditError"] = "Please, correct all errors.";
            return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 USER_CODE)
        {
            var model = db.ANSWERS;
            if (USER_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.USER_CODE == USER_CODE);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            return PartialView("_GridViewPartial", model.ToList());
        }
	}
}