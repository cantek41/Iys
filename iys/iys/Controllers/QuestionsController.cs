using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Web;
using System.Web.Mvc;
using DevExpress.Web;
using DevExpress.Web.Mvc;
using iys.Models;
using iys.ModelProject;
using System.Reflection;

namespace iys.Controllers
{
    public class QuestionsController : AdminBaseController
    {
        //
        // GET: /Questions/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Partial1()
        {
            return PartialView("Partial1");
        }

        iys.ModelProject.iysContext db = new iys.ModelProject.iysContext();

        [ValidateInput(false)]
        public ActionResult GridViewPartial()
        {
            var model = db.QUESTIONS;
            return PartialView("_GridViewPartial", model.ToList());
        }

        public ActionResult ExternalEditFormEdit(int productID)
        {
            var modelItem = db.QUESTIONS.FirstOrDefault(it => it.QUESTION_CODE == productID);
            
            if (modelItem == null)
            {
                modelItem = new iys.ModelProject.QUESTION();
                modelItem.QUESTION_CODE = -1;
            }
            return View("ExternalEditForm", new Tuple<IList<TreeListModelItem>, QUESTION>(getTree(), modelItem));
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
        public ActionResult addQuestion(QUESTION Item2)
        {
            DOCUMENT doc = db.DOCUMENTS.Find(Item2.DOCUMENT_CODE);
            if (Item2.QUESTION_CODE == -1)
            {
                QUESTION question = new QUESTION();
                if (doc != null)
                {
                    question.COURSE_CODE = doc.COURSE_CODE;
                    question.CHAPTER_CODE = doc.CHAPTER_CODE;
                    question.LESSON_CODE = doc.LESSON_CODE;
                    question.DOCUMENT_CODE = doc.DOCUMENT_CODE;
                    question.questionText = Item2.questionText;
                    question.rightChoose = Item2.rightChoose.ToString();
                    question.chooseAText = Item2.chooseAText;
                    question.chooseBText = Item2.chooseBText;
                    question.chooseCText = Item2.chooseCText;
                    question.chooseDText = Item2.chooseDText;
                   // question.chooseEText = Item2.chooseEText;
                    db.QUESTIONS.Add(question);
                    db.SaveChanges();                    
                    return Content("Eklendi");
                }
                else
                    return Content("Döküman Bulunamadı");
            }
            else
            {
                QUESTION original = db.QUESTIONS.Find(Item2.QUESTION_CODE);            

                if (original != null)
                {
                    db.Entry(original).CurrentValues.SetValues(Item2);
                    db.SaveChanges();
                }             
                return Content("Soru Güncellendi");
            }
          



        }

        public ActionResult DeleteQuestion(System.Int32 QUESTION_CODE)
        {
            var model = db.QUESTIONS;
            if (QUESTION_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.QUESTION_CODE == QUESTION_CODE);
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
        public static void CreateTreeViewNodesRecursive(List<TreeListModelItem> model, MVCxTreeViewNodeCollection nodesCollection, string parentID)
        {
            List<TreeListModelItem> rows = model.Where(x => x.ParentID == parentID).ToList(); // model.Select("[ParentID] = " + parentID);

            foreach (TreeListModelItem row in rows)
            {
                MVCxTreeViewNode node = nodesCollection.Add(row.Name, row.ID);
                CreateTreeViewNodesRecursive(model, node.Nodes, row.ID);
            }
        }


        public static List<TreeListModelItem> getTree()
        {
            iys.ModelProject.iysContext cdb = new ModelProject.iysContext();

            List<TreeListModel> couse = (from d in cdb.COURSES
                                         select new TreeListModel
                                         {
                                             ID = d.COURSE_CODE,
                                             ParentID = 0,
                                             Name = d.COURSE_NAME
                                         }).ToList();
            List<TreeListModel> chapter = (from d in cdb.CHAPTERS
                                           select new TreeListModel
                                           {
                                               ID = d.CHAPTER_CODE,
                                               ParentID = d.COURSE_CODE,
                                               Name = d.CHAPTER_NAME
                                           }).ToList();
            List<TreeListModel> lesson = (from d in cdb.LESSONS
                                          select new TreeListModel
                                          {
                                              ID = d.LESSON_CODE,
                                              ParentID = d.CHAPTER_CODE,
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
            treeListItem = convettoItem(treeListItem, couse, "co", "");
            treeListItem = convettoItem(treeListItem, chapter, "ch", "co");
            treeListItem = convettoItem(treeListItem, lesson, "l", "ch");
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

        [HttpPost, ValidateInput(false)]
        public ActionResult ExternalEditFormEdit(iys.ModelProject.QUESTION product)
        {
            if (!ModelState.IsValid)
                return View("ExternalEditForm", "EditingForm", product);

            if (product.QUESTION_CODE == -1)
                db.QUESTIONS.Add(product);
            else
            {
                db.QUESTIONS.Attach(product);

                db.Entry(product).State = System.Data.Entity.EntityState.Modified;
            }
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult GridViewPartialAddNew(iys.ModelProject.QUESTION item)
        {
            var model = db.QUESTIONS;
            //if (ModelState.IsValid)
            //{
            try
            {
                //item.QUESTION_CODE = 
                //item.DESCRIPTION = 
                //item.RES_CODE = 
                //item.COURSE_CODE = 
                //item.CHAPTER_CODE =
                //item.LESSON_CODE =
                //item.DOCUMENT_CODE =
                //item.ROW_NO = 
                //item.LEVEL =
                //item.QUESTION_TYPE =
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
        public ActionResult GridViewPartialUpdate(iys.ModelProject.QUESTION item)
        {
            var model = db.QUESTIONS;
            if (ModelState.IsValid)
            {
                try
                {
                    item.LAST_UPDATE = DateTime.Now;
                    item.LAST_UPDATE_USER = getCurrentUserName();
                    var modelItem = model.FirstOrDefault(it => it.QUESTION_CODE == item.QUESTION_CODE);
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
        public ActionResult GridViewPartialDelete(System.Int32 QUESTION_CODE)
        {
            var model = db.QUESTIONS;
            if (QUESTION_CODE >= 0)
            {
                try
                {
                    var item = model.FirstOrDefault(it => it.QUESTION_CODE == QUESTION_CODE);
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

        public ActionResult PartialViewDocumentCombo(int COURSE_CODE, int CHAPTER_CODE, int LESSON_CODE)
        {
            //  MVCxComboBox cmb = (MVCxComboBox)sender;
            int lessonID = LESSON_CODE;// Convert.ToInt32(cmb.SelectedItem.Value);
            //int courseID = (Request.Params["COURSE_CODE"] != null) ? int.Parse(Request.Params["COURSE_CODE"]) : -1;
            return PartialView(getDocument(COURSE_CODE, CHAPTER_CODE, lessonID));
        }


    }
}