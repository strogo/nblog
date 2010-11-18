using System;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Autofac;
using Autofac.Integration.Web;
using Autofac.Integration.Web.Mvc;
using Elmah;
using NBlog.Web.Application;
using NBlog.Web.Application.Service;
using NBlog.Web.Application.Service.Internal;
using NBlog.Web.Application.Storage;
using NBlog.Web.Application.Storage.Json;

namespace NBlog.Web
{
    public class MvcApplication : HttpApplication, IContainerProviderAccessor, IRequestAuthorizationHandler
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
            routes.MapRouteLowercase("", "", new { controller = "Home", action = "Index" });

            // feed
            routes.MapRouteLowercase("", "feed", new { controller = "Feed", action = "Index" });

            // entry pages
            routes.MapRouteLowercase("", "{id}", new { controller = "Entry", action = "Show" });

            // general route
            routes.MapRouteLowercase("", "{controller}/{action}/{id}", new { id = UrlParameter.Optional });
        }


        public static void RegisterGlobalFilters(GlobalFilterCollection filters)
        {
            // (none)
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

            HtmlHelper.ClientValidationEnabled = true;
            HtmlHelper.UnobtrusiveJavaScriptEnabled = true;
        }


        protected void Application_Error(object sender, EventArgs e)
        {
            if (!HttpContext.Current.IsCustomErrorEnabled)
                return;

            var exception = Server.GetLastError();
            var httpException = new HttpException(null, exception);

            var routeData = new RouteData();
            routeData.Values.Add("controller", "Error");
            routeData.Values.Add("action", "Index");
            routeData.Values.Add("httpException", httpException);

            Server.ClearError();

            var errorController = ControllerBuilder.Current.GetControllerFactory().CreateController(
                new RequestContext(new HttpContextWrapper(Context), routeData), "Error");

            errorController.Execute(new RequestContext(new HttpContextWrapper(Context), routeData));
        }


        // (Elmah.IRequestAuthorizationHandler.Authorize)
        public bool Authorize(HttpContext context)
        {
            var userService = ContainerProvider.RequestLifetime.Resolve<IUserService>();
            if (!userService.Current.IsAdmin)
            {
                throw new HttpException(403, "Forbidden");
            }

            return true;
        }

        
        protected void Application_BeginRequest(object sender, EventArgs e)
        {
            EnforceLowercaseUrl();
        }


        private void EnforceLowercaseUrl()
        {
            var path = Request.Url.AbsolutePath;
            var verbIsGet = string.Equals(Request.HttpMethod, "GET", StringComparison.CurrentCultureIgnoreCase);

            if (!verbIsGet || !path.Any(c => char.IsUpper(c))) return;

            Response.RedirectPermanent(path.ToLowerInvariant() + Request.Url.Query);
        }
    }
}