using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatWall.Entities
{
    public class lEntity
    {
        public string entity { get; set; }
        public string type { get; set; }
        public int startIndex { get; set; }
        public int endIndex { get; set; }
        public float score { get; set; }
    }
}