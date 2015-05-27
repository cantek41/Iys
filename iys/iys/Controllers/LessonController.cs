using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;

namespace iys.Controllers
{
    public class LessonController : AdminBaseController
    {
        //
        // GET: /Lesson/
        public ActionResult Index()
        {
            ViewData["COURSE_CODE"] = getCourse();
            ViewData["CHAPTER_CODE"] = getChapter(0);
            return View();
        }



        iys.ModelProject.iysContext db = new iys.ModelProject.iysContext();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.LESSONS;
            var model1 = from less in db.LESSONS
                         join cha in db.CHAPTERS on less.CHAPTER_CODE equals cha.CHAPTER_CODE
                         join course in db.COURSES on cha.COURSE_CODE equals course.COURSE_CODE
                         select new { less.LESSON_CODE, CHAPTER_CODE = cha.CHAPTER_NAME, less.LESSON_NAME, less.DURATION, COURSE_CODE = course.COURSE_NAME };
            //return PartialView("_GridViewPartial", model1.ToList());
            return PartialView("_GridViewPartial", model.ToList());

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(iys.ModelProject.LESSON item)
        {
            var model = db.LESSONS;
          
            //if (ModelState.IsValid)
            //{
                try
                {
                    //item.LESSON_CODE =
                    //item.LESSON_NAME =
                    item.RES_CODE = 0;
                    item.ROW_NO = 0;
                    //item.COURSE_CODE =
                    //item.CHAPTER_CODE =
                    //item.DURATION =
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
                var model1 = from less in db.LESSONS
                             join cha in db.CHAPTERS on less.CHAPTER_CODE equals cha.CHAPTER_CODE
                             join course in db.COURSES on cha.COURSE_CODE equals course.COURSE_CODE
                             select new { less.LESSON_CODE, CHAPTER_CODE = cha.CHAPTER_NAME, less.LESSON_NAME, less.DURATION, COURSE_CODE = course.COURSE_NAME };
                return PartialView("_GridViewPartial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialUpdate(iys.ModelProject.LESSON item)
        {
            var model = db.LESSONS;
            if (ModelState.IsValid)
            {
                try
                {
                    item.LAST_UPDATE = DateTime.Now;
                    item.LAST_UPDATE_USER = getCurrentUserName();
                    var modelItem = model.FirstOrDefault(it => it.LESSON_CODE == item.LESSON_CODE);
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
            var model1 = from less in db.LESSONS
                         join cha in db.CHAPTERS on less.CHAPTER_CODE equals cha.CHAPTER_CODE
                         join course in db.COURSES on cha.COURSE_CODE equals course.COURSE_CODE
                         select new { less.LESSON_CODE, CHAPTER_CODE = cha.CHAPTER_NAME, less.LESSON_NAME, less.DURATION, COURSE_CODE = course.COURSE_NAME };
            return PartialView("_GridViewPartial", model.ToList());
           
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialDelete(System.Int32 LESSON_CODE)
        {
            var model = db.LESSONS;
            if (LESSON_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.LESSON_CODE == LESSON_CODE);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model1 = from less in db.LESSONS
                         join cha in db.CHAPTERS on less.CHAPTER_CODE equals cha.CHAPTER_CODE
                         join course in db.COURSES on cha.COURSE_CODE equals course.COURSE_CODE
                         select new { less.LESSON_CODE, CHAPTER_CODE = cha.CHAPTER_NAME, less.LESSON_NAME, less.DURATION, COURSE_CODE = course.COURSE_NAME };
            return PartialView("_GridViewPartial", model.ToList());
        }

        public ActionResult PartialViewChapterCombo(int COURSE_CODE)
        {
            //  MVCxComboBox cmb = (MVCxComboBox)sender;
            int courseID = COURSE_CODE;// Convert.ToInt32(cmb.SelectedItem.Value);
            //int courseID = (Request.Params["COURSE_CODE"] != null) ? int.Parse(Request.Params["COURSE_CODE"]) : -1;
            return PartialView(getChapter(courseID));
        }
    }
}
