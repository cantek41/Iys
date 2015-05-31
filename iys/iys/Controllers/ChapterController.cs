using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using iys.ModelProject;
using DevExpress.Web.Mvc;

namespace iys.Controllers
{
    public class ChapterController : AdminBaseController
    {
        private iysContext db = new iysContext();

        // GET: /Chapter/
        public ActionResult Index()
        {
            return View();
        }



        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            var model = db.CHAPTERS;
            return PartialView("_GridView1Partial", model.ToList());
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(iys.ModelProject.CHAPTER item)
        {
            var model = db.CHAPTERS;
            //if (ModelState.IsValid)
            //{
                try
                {
                    item.CHAPTER_CODE = 0;                  
                    item.RES_CODE = 0;
                    item.VISIBLE = true;
                    item.ROW_NO = 0 ;

                  //  item.DURATION = TimeSpan.Zero;

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
        public ActionResult GridView1PartialUpdate(iys.ModelProject.CHAPTER item)
        {
            var model = db.CHAPTERS;
            if (ModelState.IsValid)
            {
                try
                {
                    item.LAST_UPDATE = DateTime.Now;
                    item.LAST_UPDATE_USER = getCurrentUserName();
                    var modelItem = model.FirstOrDefault(it => it.CHAPTER_CODE == item.CHAPTER_CODE);
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
        public ActionResult GridView1PartialDelete(System.Int32 CHAPTER_CODE)
        {
            var model = db.CHAPTERS;
            if (CHAPTER_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.CHAPTER_CODE == CHAPTER_CODE);
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
