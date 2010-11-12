﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Security;
using Autofac;
using Autofac.Core;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;
using NBlog.Web.Application.Storage;
using NBlog.Web.Application.Storage.Json;
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

            // homepage
            routes.MapRoute("", "", new { controller = "Home", action = "Index" });

            // general route
            routes.MapRoute("", "{controller}/{action}/{id}", new { id = UrlParameter.Optional });
            
            // entry pages
            routes.MapRoute("", "{slug}", new { controller = "Entry", action = "Show" });
        }

        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            filters.Add(new HandleErrorAttribute());
        }

        protected void Application_Start()
        {
            var builder = new ContainerBuilder();

            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().HttpRequestScoped();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).InjectActionInvoker().HttpRequestScoped();
          
            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterType<JsonRepository>().As<IRepository>().HttpRequestScoped().WithParameter("dataPath", HttpContext.Current.Server.MapPath("~/App_Data/"));
            builder.RegisterType<ConfigService>().As<IConfigService>().HttpRequestScoped();
            builder.RegisterType<EntryService>().As<IEntryService>().HttpRequestScoped();
            builder.RegisterType<UserService>().As<IUserService>().HttpRequestScoped();
            builder.RegisterType<Services>().As<IServices>().HttpRequestScoped();

            _containerProvider = new ContainerProvider(builder.Build());

            ControllerBuilder.Current.SetControllerFactory(new AutofacControllerFactory(this.ContainerProvider));

            AreaRegistration.RegisterAllAreas();
            RegisterGlobalFilters(GlobalFilters.Filters);
            RegisterRoutes(RouteTable.Routes);
        }
    }
}