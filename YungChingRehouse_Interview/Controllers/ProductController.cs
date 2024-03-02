using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using YungChingRehouse_Interview.Services;

namespace YungChingRehouse_Interview.Controllers
{
    [Authorize]
    public class ProductController : BaseController
    {
        public ProductController(ICommonService commonService): base(commonService)
        {

        }

        // GET: Product
        public ActionResult Index()
        {
            return View();
        }
    }
}