using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web.Mvc;
using iys.Models;
using iys.ModelProject;

namespace iys.Controllers
{
    public class EducationController : Controller
    {
        //
        // GET: /Education/
        public ActionResult Index()
        {

            return View(getTree());
        }

        public ActionResult TreeListPartial()
        {
            if (DevExpressHelper.IsCallback)
                // Intentionally pauses server-side processing,
                // to demonstrate the Loading Panel functionality.
                Thread.Sleep(500);
            return PartialView("TreeList", getTree());
        }
        [HttpPost]
        public ActionResult getContentDocumant(int DocumentCode)
        {
            using (iysContext db = new iysContext())
            {
                string user = User.Identity.Name;
                int DocumentCodeBefore = DocumentCode - 1;
                USER_QUIZ_STATUS quiz = db.USER_QUIZ_STATUSS.Where(x => x.USER_CODE == user).Where(x => x.DOCUMENT_CODE == DocumentCodeBefore).FirstOrDefault();
                docDetail d = new docDetail();
                if (quiz != null || DocumentCode == 1 || User.IsInRole("Admin"))
                {
                    DOCUMENT doc = db.DOCUMENTS.Find(DocumentCode);

                    d.sure = Convert.ToInt32(doc.DURATION.Substring(0, 2)) * 60 + Convert.ToInt32(doc.DURATION.Substring(3, 2));
                    if (doc.DOCUMENT_TYPE == 5)//video
                    {
                        d.path = doc.PATH;
                    }
                    else if (doc.DOCUMENT_TYPE == 6)
                    {
                        d.path = String.Format("<embed src=\"../dersler/{0}\" width=\"600\" height=\"480\">", doc.PATH);
                    }
                    d.hata = 0;
                }
                else
                {

                    d.path = "Bu Dersi Almaya hakkınız yok!..";
                    d.sure = 0;
                    d.hata = 1;

                }
                return Json(d, JsonRequestBehavior.AllowGet);

            }
            // return "<embed src=\"/Content/pdfd.pdf\" width=\"500\" height=\"375\">"; 
        }
        public static List<TreeListModelItem> getTree()
        {
            iys.ModelProject.iysContext cdb = new ModelProject.iysContext();

            List<TreeListModel> lesson = (from d in cdb.LESSONS
                                          select new TreeListModel
                                          {
                                              ID = d.LESSON_CODE,
                                              ParentID = 0,
                                              Name = d.LESSON_NAME
                                          }).ToList();
            List<TreeListModel> document = (from d in cdb.DOCUMENTS
                                            select new TreeListModel
                                            {
                                                ID = d.DOCUMENT_CODE,
                                                ParentID = d.LESSON_CODE,
                                                Name = d.DOCUMENT_NAME
                                            }).ToList();
            List<TreeListModelItem> treeListItem = new List<TreeListModelItem>();
            treeListItem = convettoItem(treeListItem, lesson, "l", "");
            treeListItem = convettoItem(treeListItem, document, "", "l");

            return treeListItem;
        }


        public static List<TreeListModelItem> convettoItem(List<TreeListModelItem> treeListItem, List<TreeListModel> model, string tag, string ptag)
        {
            foreach (var item in model)
            {
                treeListItem.Add(new TreeListModelItem { ID = tag + item.ID.ToString(), ParentID = ptag + item.ParentID, Name = item.Name });
            }
            return treeListItem;
        }

        [HttpPost]
        public ActionResult Exam(int documenCode)
        {
            using (iysContext db = new iysContext())
            {
                ViewBag.ExamName = db.DOCUMENTS.Find(documenCode).DOCUMENT_NAME;
                List<QUESTION> doc = db.QUESTIONS.Where(x => x.DOCUMENT_CODE == documenCode).ToList();
                return View(doc);
            }

        }
        [HttpPost]
        public ActionResult ExamChoose(int DOCUMENT_CODE, int count)
        {
            using (iysContext db = new iysContext())
            {
                USER_QUIZ_STATUS quiz = new USER_QUIZ_STATUS();
                quiz.DOCUMENT_CODE = DOCUMENT_CODE;
                quiz.DATE = DateTime.Now;
                quiz.USER_CODE = User.Identity.Name;
                db.USER_QUIZ_STATUSS.Add(quiz);
                db.SaveChanges();
                List<QUESTION> sorular = db.QUESTIONS.Where(x => x.DOCUMENT_CODE == DOCUMENT_CODE).ToList();
                int dogruCevap = 0;
                for (int i = 0; i < count; i++)
                {
                    USER_ANSWER answer = new USER_ANSWER();
                    string question_code = Request.Form["item_QUESTION_CODE_" + i.ToString()].ToString();
                    string chose = Request.Form[question_code].ToString();
                    answer.QUESTION_CODE = Convert.ToInt32(question_code);
                    answer.CHOOSE = chose;
                    answer.QUIZ_CODE = quiz.QUIZ_CODE;
                    answer.USER_CODE = User.Identity.Name;
                    db.USER_ANSWERS.Add(answer);

                    //doğre cevap bul
                    if (chose.Equals(sorular.Where(x => x.QUESTION_CODE == answer.QUESTION_CODE).FirstOrDefault().rightChoose))
                    {
                        dogruCevap++;
                    }
                }


                quiz.GRADE = (dogruCevap * 100) / count;
                db.USER_QUIZ_STATUSS.Attach(quiz);
                db.Entry(quiz).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }

        }
    }
}

public class docDetail
{
    public int sure { get; set; }
    public string path { get; set; }
    public int hata { get; set; }
}