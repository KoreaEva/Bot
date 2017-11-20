using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatWall.Model
{
    [Serializable]
    public class OrderItem
    {
        public int ItemID { get; set; }
        public string Title { get; set; }
        public int Quantity { get; set; }
        public Decimal Price { get; set; }
    }
}