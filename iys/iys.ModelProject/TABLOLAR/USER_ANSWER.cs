using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iys.ModelProject
{
    public class USER_ANSWER
    {
        [Key]
        public int ID { get; set; }
        public int? QUIZ_CODE { get; set; }
        public int? QUESTION_CODE { get; set; }
        public string CHOOSE { get; set; }
        public string USER_CODE { get; set; }
    }

    public class USER_QUIZ_STATUS
    {
        [Key]        
        public int QUIZ_CODE { get; set; }        
        public int? DOCUMENT_CODE { get; set; }
        public int? GRADE { get; set; }
        public string USER_CODE { get; set; }
        public DateTime? DATE { get; set; }
    }
}
