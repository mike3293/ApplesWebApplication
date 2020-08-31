﻿using ApplesWebApplication.AppStartup;
using ApplesWebApplication.Filters;
using ApplesWebApplication.Services;
using ApplesWebApplication.Services.Implementations;
using ApplesWebApplication.Services.Interfaces;
using Ninject;
using Owin;
using System;
using System.Diagnostics;
using System.Net.Http.Formatting;
using System.Threading;
using System.Threading.Tasks;
using System.Web.Http;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

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

            appBuilder.Use(typeof(LoggerMiddleware));

            appBuilder.Use(new Func<AppFunc, AppFunc>(next =>
                env =>
                {
                    Stopwatch stopwatch = new Stopwatch();

                    Debug.WriteLine($"{DateTime.UtcNow} | Request start");
                    stopwatch.Start();
                    Task nextResult = next(env).ContinueWith(_ => 
                    {
                        stopwatch.Stop();
                        Debug.WriteLine($"{DateTime.UtcNow} | Request completed in: {stopwatch.Elapsed}");
                    });

                    return nextResult;
                }));

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