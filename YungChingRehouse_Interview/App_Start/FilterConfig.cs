using System.Web;
using System.Web.Mvc;
using YungChingRehouse_Interview.Filters;

namespace YungChingRehouse_Interview
{
    public class FilterConfig
    {
        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
            filters.Add(new ExceptionFilter());
        }
    }
}
