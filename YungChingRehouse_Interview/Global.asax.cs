using Autofac;
using Autofac.Integration.Mvc;
using System;
using System.Collections.Generic;
using System.Data.Entity;
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
            var builder = new ContainerBuilder();
            // 註冊所有的Controller作為Service
            builder.RegisterControllers(typeof(HomeController).Assembly);
            // 註冊Service
            builder.RegisterType<AccountService>().As<IAccountService>();
            builder.RegisterType<EncryptService>().As<IEncryptService>();
            builder.RegisterType<CommonService>().As<ICommonService>();
            builder.RegisterType<ProductService>().As<IProductService>();
            // 註冊 EF 資料庫內容 YCReHouseInterviewEntities 生命周期管理，每次 HTTP 請求到來時都會創建一個新的實例。
            builder.RegisterType<YCReHouseInterviewEntities>().As<DbContext>().InstancePerRequest();
            //註冊泛型Repository
            builder.RegisterGeneric(typeof(EFGenericRepository<>)).As(typeof(IRepository<>)).InstancePerRequest();

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
