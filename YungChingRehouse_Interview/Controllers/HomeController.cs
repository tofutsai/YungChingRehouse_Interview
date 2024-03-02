using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace YungChingRehouse_Interview.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            //判斷使用者是否已經過登入驗證
            if (User?.Identity?.IsAuthenticated == true)
                return View();
            else
                return RedirectToAction("Login", "Account");
        }

    }
}