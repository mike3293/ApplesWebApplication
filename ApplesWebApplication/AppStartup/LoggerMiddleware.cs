using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using AppFunc = System.Func<System.Collections.Generic.IDictionary<string, object>, System.Threading.Tasks.Task>;

namespace ApplesWebApplication.AppStartup
{
    public class LoggerMiddleware
    {
        private readonly AppFunc _next;

        public LoggerMiddleware(AppFunc next)
        {
            _next = next ?? throw new ArgumentNullException("next");
        }

        public Task Invoke(IDictionary<string, object> env)
        {
            Debug.WriteLine($"{DateTime.UtcNow} | Request to: {env["owin.RequestPath"]}");

            return _next(env);
        }
    }
}