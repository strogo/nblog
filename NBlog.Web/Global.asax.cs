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
            routes.RouteExistingFiles = false;
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // homepage
            routes.MapRouteLowercase("", "", new { controller = "Home", action = "Index" });

            // feed
            routes.MapRouteLowercase("", "feed", new { controller = "Feed", action = "Index" });

            // search
            routes.MapRouteLowercase("", "search", new { controller = "Search", action = "Index" });

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
            const string dataPath = "~/App_Data/";

            var builder = new ContainerBuilder();

            builder.RegisterType<ExtensibleActionInvoker>().As<IActionInvoker>().InstancePerHttpRequest();
            builder.RegisterControllers(Assembly.GetExecutingAssembly()).InjectActionInvoker().InstancePerHttpRequest();

            builder.RegisterModelBinders(Assembly.GetExecutingAssembly());
            builder.RegisterType<JsonRepository>().As<IRepository>().InstancePerHttpRequest().WithParameter("dataPath", HttpContext.Current.Server.MapPath(dataPath));
            builder.RegisterType<ConfigService>().As<IConfigService>().InstancePerHttpRequest();
            builder.RegisterType<EntryService>().As<IEntryService>().InstancePerHttpRequest();
            builder.RegisterType<UserService>().As<IUserService>().InstancePerHttpRequest();
            builder.RegisterType<Services>().As<IServices>().InstancePerHttpRequest();

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