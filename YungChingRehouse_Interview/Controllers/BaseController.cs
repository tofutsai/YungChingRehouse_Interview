using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using YungChingRehouse_Interview.Models;
using YungChingRehouse_Interview.Services;

namespace YungChingRehouse_Interview.Controllers
{
    public class BaseController : Controller
    {
        public UserInfo userInfo = new UserInfo();

        protected ICommonService _commonService;
        public BaseController(ICommonService commonService)
        {
            _commonService = commonService;
        }
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            //取得 ASP.NET 使用者
            var user = System.Web.HttpContext.Current.User;

            //是否通過驗證
            if (user?.Identity?.IsAuthenticated == true)
            {
                //取得 FormsIdentity
                var identity = (FormsIdentity)user.Identity;

                //取得 FormsAuthenticationTicket
                var ticket = identity.Ticket;

                //將 Ticket 內的 UserData 解析回 User 物件
                userInfo = _commonService.DecodeJsonToUserInfo(ticket.UserData);

            }
            else
            {
                filterContext.Result = new RedirectToRouteResult(
                    new RouteValueDictionary {
                      {"controller", "Account"},
                      {"action", "Login"}
                });
                return;
            }
        }
    }
}