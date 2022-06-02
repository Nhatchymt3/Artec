using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using Outsourcing.Data;
using FluentValidation.Mvc;
using System.Web.Http;

namespace Labixa
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            //AreaRegistration.RegisterAllAreas();
            //FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            //RouteConfig.RegisterRoutes(RouteTable.Routes);
            //BundleConfig.RegisterBundles(BundleTable.Bundles);


            System.Data.Entity.Database.SetInitializer<OutsourcingEntities>(null);
            //System.Data.Entity.Database.SetInitializer(new OutsourcingSampleData());
            GlobalConfiguration.Configure(WebApiConfig.Register);
            AreaRegistration.RegisterAllAreas();
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            Bootstrapper.Run();
            GlobalConfiguration.Configure(ServiceConfig.Register);
            FluentValidationModelValidatorProvider.Configure();//cấu hình cho fluent validation
            GlobalConfiguration.Configuration.EnsureInitialized();
        }
    }
}
