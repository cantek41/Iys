using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iys.Models
{
    public class QuestionViewModel
    {
        public int documentCode { get; set; }
        public string questionText { get; set; }
        public string chooseAText { get; set; }
        public string chooseBText { get; set; }
        public string chooseCText { get; set; }
        public string chooseDText { get; set; }
        public string chooseEText { get; set; }        
        public char rightChoose { get; set; }
        
    }
}