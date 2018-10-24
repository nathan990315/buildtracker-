using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BuildFeed.Middleware
{
    public static class OptionsMiddleware
    {
        private static Task LoadVersionInfo(HttpContext context, Func<Task> next)
        {
            string versionString = Assembly.GetExecutingAssembly()
                    .GetCustomAttributes(typeof(AssemblyMetadataAttribute))
                    .OfType<AssemblyMetadataAttribute>()
                    .FirstOrDefault(a => a.Key == "GitHash")
                    ?.Value
                ?? "N/A";

            context.Items.TryAdd("Version", versionString);

            return next();
        }

        public static IApplicationBuilder UseVersion(this IApplicationBuilder app) => app.Use(LoadVersionInfo);
    }
}