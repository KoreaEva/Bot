using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KakaoConnector.Controllers
{
    public class KeyboardController : Controller
    {
        // GET: Keyboard
        public ActionResult Index()
        {
            Models.Keyboard keyboard = new Models.Keyboard();

            keyboard.type = "buttons";
            keyboard.buttons = new string[]{"인사", "대화"};

            return Json(keyboard, JsonRequestBehavior.AllowGet);
        }
    }
}