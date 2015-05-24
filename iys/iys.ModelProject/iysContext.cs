
using iys.ModelProject.TABLOLAR;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iys.ModelProject
{
    public class iysContext: DbContext
    {
        public iysContext()
            : base("DefaultConnection")
        {

        }
        public DbSet<ANSWER> ANSWERS { get; set; }
        public DbSet<ANSWER_RESULT> ANSWER_RESULTS { get; set; }
        public DbSet<CHAPTER> CHAPTERS { get; set; }
        public DbSet<COURSE> COURSES { get; set; }
        public DbSet<DOCUMENT> DOCUMENTS { get; set; }
        public DbSet<LESSON> LESSONS { get; set; }
        public DbSet<QUESTION> QUESTIONS { get; set; }
        public DbSet<USER> USERDETAILS { get; set; }

        public DbSet<GROUP> GROUPS { get; set; }

        public DbSet<GROUPDES> GROUPDESS { get; set; }
        public DbSet<RES> RESS { get; set; }
        public DbSet<USER_ANSWER> USER_ANSWERS { get; set; }
        public DbSet<USER_QUIZ_STATUS> USER_QUIZ_STATUSS { get; set; }
     
    }
}
