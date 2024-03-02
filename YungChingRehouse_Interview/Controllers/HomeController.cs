using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YungChingRehouse_Interview.Services;

namespace YungChingRehouse_Interview.Controllers
{
    [Authorize]
    public class HomeController : BaseController
    {
        public HomeController(ICommonService commonService) : base(commonService)
        {

        }
        public ActionResult Index()
        {
            string userName = string.Empty;
            if (userInfo != null)
            {
                userName = userInfo.operName;
                ViewBag.Message = string.IsNullOrEmpty(userName) ? "" : $"Hello {userName}";
            }
            return View();
        }

    }
}