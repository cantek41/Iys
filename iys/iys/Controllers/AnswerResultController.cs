using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace iys.Controllers
{
    public class AnswerResultController : AdminBaseController
    {
        //
        // GET: /AnswerResult/
        public ActionResult Index()
        {
            return View();
        }

        iys.ModelProject.iysContext db = new iys.ModelProject.iysContext();

        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            var model = db.ANSWER_RESULTS;
            return PartialView("_GridView1Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(iys.ModelProject.ANSWER_RESULT item)
        {
            var model = db.ANSWER_RESULTS;
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
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate(iys.ModelProject.ANSWER_RESULT item)
        {
            var model = db.ANSWER_RESULTS;
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
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int32 USER_CODE)
        {
            var model = db.ANSWER_RESULTS;
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
            return PartialView("_GridView1Partial", model.ToList());
        }
	}
}