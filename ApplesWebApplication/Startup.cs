using ApplesWebApplication.Filters;
using ApplesWebApplication.Services;
using ApplesWebApplication.Services.Implementations;
using ApplesWebApplication.Services.Interfaces;
using Ninject;
using Owin;
using System.Net.Http.Formatting;
using System.Web.Http;

namespace ApplesWebApplication
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            var config = new HttpConfiguration();

            config.DependencyResolver = new NinjectResolver(CreateKernel());

            config.MapHttpAttributeRoutes();
            config.Routes.MapHttpRoute(
                name: "DefaultApi",
                routeTemplate: "api/{controller}/{id}",
                defaults: new { id = RouteParameter.Optional }
            );

            config.Filters.Add(new ValidateModelAttribute());

            var jsonFormatter = new JsonMediaTypeFormatter();
            jsonFormatter.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore;
            config.Services.Replace(typeof(IContentNegotiator), new JsonContentNegotiator(jsonFormatter));

            appBuilder.UseWebApi(config);
        }

        private static IKernel CreateKernel()
        {
            var kernel = new StandardKernel();

            kernel.Bind<DBContext>().ToSelf();
            kernel.Bind<IAppleVarietyService>().To<AppleVarietyService>();
            kernel.Bind<IRegionService>().To<RegionService>();

            return kernel;
        }
    }
}