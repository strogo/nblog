using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using NBlog.Web.Application;
using Newtonsoft.Json;
using System.IO;

namespace NBlog.Web
{
    public class MvcApplication : HttpApplication, IContainerProviderAccessor
    {
        static IContainerProvider _containerProvider;

        public IContainerProvider ContainerProvider
        {
            get { return _containerProvider; }
        }

        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                "Default", // Route name
                "{controller}/{action}/{id}", // URL with parameters
                new { controller = "Home", action = "Index", id = UrlParameter.Optional } // Parameter defaults
            );
        }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().HttpRequestScoped(); 
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).InjectActionInvoker().HttpRequestScoped();

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());

            builder.RegisterType<Settings>().HttpRequestScoped();

            _containerProvider = new ContainerProvider(builder.Build());

            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(this.ContainerProvider));

            AreaRegistration.RegisterAllAreas();
            RegisterRoutes(RouteTable.Routes);
        }
    }
}