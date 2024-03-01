using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using YungChingRehouse_Interview.Controllers;
using YungChingRehouse_Interview.Models;
using YungChingRehouse_Interview.Models.DAL;
using YungChingRehouse_Interview.Services;

namespace YungChingRehouse_Interview
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            // 創建DB Context實例
            var dbContext = new YCReHouseInterviewEntities();
            var builder = new ContainerBuilder();
            // 註冊所有的Controller作為Service
            builder.RegisterControllers(typeof(HomeController).Assembly);
            // 註冊Service
            builder.RegisterType<AccountService>().As<IAccountService>();
            //註冊泛型Repository
            //builder.RegisterGeneric(typeof(EFGenericRepository<>)).As(typeof(IRepository<>));
            builder.RegisterGeneric(typeof(EFGenericRepository<>)).As(typeof(IRepository<>)).WithParameter(
        new TypedParameter(typeof(YCReHouseInterviewEntities), dbContext));

            // 建立 DI Container
            var container = builder.Build();
            // 用DI Container作為建立Controller時候的DI Resolver。
            DependencyResolver.SetResolver(new AutofacDependencyResolver(container));
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
        }
    }
}
