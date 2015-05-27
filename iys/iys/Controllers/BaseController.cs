using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using iys.ModelProject;
using System.Collections;

namespace iys.Controllers
{
    public class BaseController : Controller
    {
        /// <summary>
        /// session daki user bilgilerini getirir
        /// </summary>
        /// <returns>user code</returns>
        public int getCurrentUserName()
        {
            int username = -1;
            using (iysContext db = new iysContext())
            {
                string userId = User.Identity.Name;
                username = db.USERDETAILS.Where(x => x.NICK_NAME == userId).Select(x => x.USER_CODE).SingleOrDefault();

            }
            return username;
        }
        /// <summary>
        /// tüm Dersleri getirir
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getCourse()
        {
            using (iysContext db = new iysContext())
            {
                return (from d in db.COURSES
                        select new { Key = d.COURSE_CODE, Value = d.COURSE_NAME }).ToDictionary(t => t.Key, t => t.Value);
            }
        }

        /// <summary>
        /// seçilen dersin bölümlerini getirir
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getChapter(int course)
        {
            using (iysContext db = new iysContext())
            {
                Dictionary<int, string> a = (from d in db.CHAPTERS
                                             where d.COURSE_CODE == course
                                             select new { Key = d.CHAPTER_CODE, Value = d.CHAPTER_NAME }).ToDictionary(t => t.Key, t => t.Value);
                return a;
            }
        }

        /// <summary>
        /// seçilen dersin bölümlerini getirir
        /// </summary>
        /// <returns></returns>
        public static string getChapterString(int chapterCode)
        {
            using (iysContext db = new iysContext())
            {
                string a = (from d in db.CHAPTERS
                            where d.CHAPTER_CODE == chapterCode
                            select d.CHAPTER_NAME).First().ToString();
                return a;
            }
        }

        public static string getLessonString(int lessonCode)
        {
            using (iysContext db = new iysContext())
            {
                string a = (from d in db.LESSONS
                            where d.LESSON_CODE == lessonCode
                            select d.LESSON_NAME).First().ToString();
                return a;
            }
        }

        public static string getDocumentString(int documentCode)
        {
            using (iysContext db = new iysContext())
            {
                string a = (from d in db.DOCUMENTS
                            where d.DOCUMENT_CODE == documentCode
                            select d.DOCUMENT_NAME).First().ToString();
                return a;
            }
        }


        /// <summary>
        /// seçilen dersin bölümlerini getirir
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getLesson(int course, int chapter)
        {
            using (iysContext db = new iysContext())
            {
                Dictionary<int, string> a = (from d in db.LESSONS
                                             where d.COURSE_CODE == course && d.CHAPTER_CODE == chapter
                                             select new { Key = d.LESSON_CODE, Value = d.LESSON_NAME }).ToDictionary(t => t.Key, t => t.Value);
                return a;
            }
        }

        public static Dictionary<int, string> getDocument(int course, int chapter, int lesson)
        {
            using (iysContext db = new iysContext())
            {
                Dictionary<int, string> a = (from d in db.DOCUMENTS
                                             where d.COURSE_CODE == course && d.CHAPTER_CODE == chapter && d.LESSON_CODE == lesson
                                             select new { Key = d.DOCUMENT_CODE, Value = d.DOCUMENT_NAME }).ToDictionary(t => t.Key, t => t.Value);
                return a;
            }
        }

        /// <summary>
        /// seçilen soruyu getiri
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getQuestions(int LESSON_CODE)
        {
            using (iysContext db = new iysContext())
            {
                return (from d in db.QUESTIONS
                        where d.LESSON_CODE == LESSON_CODE
                        select new { Key = d.QUESTION_CODE, Value = d.DESCRIPTION }).ToDictionary(t => t.Key, t => t.Value);
            }
        }
        /// <summary>
        /// seçilen soruyu getiri
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getQuestions()
        {
            using (iysContext db = new iysContext())
            {
                return (from d in db.QUESTIONS
                        select new { Key = d.QUESTION_CODE, Value = d.DESCRIPTION }).ToDictionary(t => t.Key, t => t.Value);
            }
        }

        /// <summary>
        /// seçilen soruyu getiri
        /// </summary>
        /// <returns></returns>
        public static Dictionary<int, string> getType(int gruopDes)
        {
            using (iysContext db = new iysContext())
            {
                return (from d in db.GROUPS
                        where d.GROUP_CODE == gruopDes
                        select new { Key = d.ID, Value = d.EXP_TR }).ToDictionary(t => t.Key, t => t.Value);
            }
        }

    }


    
    [Authorize(Roles="Admin")]
    public class AdminBaseController : BaseController
    {
 
    }
}