using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace iys.Models
{
    public class TreeListModel
    {
        public int? ParentID { get; set; }
        public int? ID { get; set; }
        public string Name { get; set; }
    }
    public class TreeListModelItem
    {
        public string ParentID { get; set; }
        public string ID { get; set; }
        public string Name { get; set; }
    }
}