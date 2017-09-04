using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace GreatWall.Entities
{
    public class GreatWallLUIS
    {
        public string query { get; set; }
        public lIntent[] intents { get; set; }
        public lEntity[] entities { get; set; }
    }
}