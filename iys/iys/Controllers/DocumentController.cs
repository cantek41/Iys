using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iys.ModelProject;
using DevExpress.Web.Mvc;
using DevExpress.Web;
using System.Web.UI;
using System.Data.Entity;

namespace iys.Controllers
{
    public class DocumentController : AdminBaseController
    {
        //
        // GET: /Document/
        public static string filePath;
        public ActionResult Index()
        {
            ViewData["COURSE_CODE"] = getCourse();
            ViewData["CHAPTER_CODE"] = getChapter(0);
            ViewData["LESSON_CODE"] = getLesson(0, 0);
            return View();
        }

        iys.ModelProject.iysContext db = new iys.ModelProject.iysContext();

        [ValidateInput(false)]
        public ActionResult GridView1Partial()
        {
            ViewBag.index = 0;
            var model = db.DOCUMENTS;
            var model1 = from d in db.DOCUMENTS
                         join b in db.CHAPTERS on d.CHAPTER_CODE equals b.CHAPTER_CODE
                         join cs in db.COURSES on d.COURSE_CODE equals cs.COURSE_CODE
                         join lesson in db.LESSONS on d.LESSON_CODE equals lesson.LESSON_CODE
                         select new { d.DOCUMENT_CODE, CHAPTER_CODE = b.CHAPTER_NAME, d.DOCUMENT_TYPE, d.DURATION, d.LINK_TYPE, d.PATH, LESSON_CODE = lesson.LESSON_NAME, COURSE_CODE = cs.COURSE_NAME, d.DOCUMENT_NAME };
            return PartialView("_GridView1Partial", model.ToList());

        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialAddNew(iys.ModelProject.DOCUMENT item)
        {

            var model = db.DOCUMENTS;
           
            try
            {
                item.PATH = filePath;
                item.VISIBLE = true;
                item.CREATE_USER = getCurrentUserName();
                item.CREATE_DATE = DateTime.Now;
                item.LAST_UPDATE = DateTime.Now;
                item.LAST_UPDATE_USER = getCurrentUserName();
                filePath = null;
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
            var model1 = from d in db.DOCUMENTS
                         join b in db.CHAPTERS on d.CHAPTER_CODE equals b.CHAPTER_CODE
                         join cs in db.COURSES on d.COURSE_CODE equals cs.COURSE_CODE
                         join lesson in db.LESSONS on d.LESSON_CODE equals lesson.LESSON_CODE
                         select new { d.DOCUMENT_CODE, CHAPTER_CODE = b.CHAPTER_NAME, d.DOCUMENT_TYPE, d.DURATION, d.LINK_TYPE, d.PATH, LESSON_CODE = lesson.LESSON_NAME, COURSE_CODE = cs.COURSE_NAME, d.DOCUMENT_NAME };
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialUpdate(iys.ModelProject.DOCUMENT item)
        {
            var model = db.DOCUMENTS;
            try
            {
                item.PATH = filePath;
                item.PRIORITY = 0;
                item.ROW_ORDER_NO = 0;
                item.VISIBLE = true;
                item.CREATE_USER = getCurrentUserName();
                item.CREATE_DATE = DateTime.Now;
                item.LAST_UPDATE = DateTime.Now;
                item.LAST_UPDATE_USER = getCurrentUserName();
                filePath = null;
               /// DOCUMENT modelItem = model.FirstOrDefault(it => it.DOCUMENT_CODE == item.DOCUMENT_CODE);
                if (item != null)
                {
                    db.DOCUMENTS.Attach(item);
                    db.Entry(item).State = EntityState.Modified;
                    db.SaveChanges();
                }
            }
            catch (Exception e)
            {
                ViewData["EditError"] = e.Message;
            }
            //}
            //else
            //    ViewData["EditError"] = "Please, correct all errors.";
            var model1 = from d in db.DOCUMENTS
                         join b in db.CHAPTERS on d.CHAPTER_CODE equals b.CHAPTER_CODE
                         join cs in db.COURSES on d.COURSE_CODE equals cs.COURSE_CODE
                         join lesson in db.LESSONS on d.LESSON_CODE equals lesson.LESSON_CODE
                         select new { d.DOCUMENT_CODE, CHAPTER_CODE = b.CHAPTER_NAME, d.DOCUMENT_TYPE, d.DURATION, d.LINK_TYPE, d.PATH, LESSON_CODE = lesson.LESSON_NAME, COURSE_CODE = cs.COURSE_NAME, d.DOCUMENT_NAME };
            return PartialView("_GridView1Partial", model.ToList());
        }
        [HttpPost, ValidateInput(false)]
        public ActionResult GridView1PartialDelete(System.Int32 DOCUMENT_CODE)
        {
            var model = db.DOCUMENTS;
            if (DOCUMENT_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.DOCUMENT_CODE == DOCUMENT_CODE);
                    if (item != null)
                        model.Remove(item);
                    db.SaveChanges();
                }
                catch (Exception e)
                {
                    ViewData["EditError"] = e.Message;
                }
            }
            var model1 = from d in db.DOCUMENTS
                         join b in db.CHAPTERS on d.CHAPTER_CODE equals b.CHAPTER_CODE
                         join cs in db.COURSES on d.COURSE_CODE equals cs.COURSE_CODE
                         join lesson in db.LESSONS on d.LESSON_CODE equals lesson.LESSON_CODE
                         select new { d.DOCUMENT_CODE, CHAPTER_CODE = b.CHAPTER_NAME, d.DOCUMENT_TYPE, d.DURATION, d.LINK_TYPE, d.PATH, LESSON_CODE = lesson.LESSON_NAME, COURSE_CODE = cs.COURSE_NAME, d.DOCUMENT_NAME };
            return PartialView("_GridView1Partial", model.ToList());
        }

        public ActionResult PartialViewChapterCombo(int COURSE_CODE)
        {
            //  MVCxComboBox cmb = (MVCxComboBox)sender;
            int courseID = COURSE_CODE;// Convert.ToInt32(cmb.SelectedItem.Value);
            //int courseID = (Request.Params["COURSE_CODE"] != null) ? int.Parse(Request.Params["COURSE_CODE"]) : -1;
            return PartialView(getChapter(courseID));
        }

        public ActionResult PartialViewLessonCombo(int COURSE_CODE, int CHAPTER_CODE)
        {
            //  MVCxComboBox cmb = (MVCxComboBox)sender;
            int chapterID = CHAPTER_CODE;// Convert.ToInt32(cmb.SelectedItem.Value);
            //int courseID = (Request.Params["COURSE_CODE"] != null) ? int.Parse(Request.Params["COURSE_CODE"]) : -1;
            return PartialView(getLesson(COURSE_CODE, chapterID));
        }

        public ActionResult ImageUpload()
        {

            UploadControlExtension.GetUploadedFiles("PATH", UploadControlHelper.ValidationSettings, UploadControlHelper.uploadControl_FileUploadComplete);

            return null;
        }
    }

    public class UploadControlHelper
    {
        public static readonly UploadControlValidationSettings ValidationSettings = new UploadControlValidationSettings
        {
            AllowedFileExtensions = new string[] { ".jpg", ".png", ".jpeg", ".jpe",".pdf",".pptx",".mp4" },

            MaxFileSize = 10000000
        };

        public static void uploadControl_FileUploadComplete(object sender, FileUploadCompleteEventArgs e)
        {

            if (e.UploadedFile.IsValid)
            {
                string resultFilePath = "~/Documents/" + e.UploadedFile.FileName;
                e.UploadedFile.SaveAs(HttpContext.Current.Request.MapPath(resultFilePath));
                IUrlResolutionService urlResolver = sender as IUrlResolutionService;
                if (urlResolver != null)
                    e.CallbackData = urlResolver.ResolveClientUrl(resultFilePath) + "?refresh=" + Guid.NewGuid().ToString();
                DocumentController.filePath = e.UploadedFile.FileName;
            }
        }
    }
}
