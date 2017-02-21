using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatWall.Entities
{
    [Serializable]
    public class lMenu
    {
        public string Menu { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
    }
}