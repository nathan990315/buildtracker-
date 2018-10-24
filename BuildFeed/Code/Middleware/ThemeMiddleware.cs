using System;
using System.Threading.Tasks;
using BuildFeed.Options;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BuildFeed.Middleware
{
    public static class ThemeMiddleware
    {
        private const string THEME_COOKIE_NAME = "bf_theme";

        private static Task LoadThemeFromCookie(HttpContext context, Func<Task> next)
        {
            bool cookieHasValue = context.Request.Cookies.TryGetValue(THEME_COOKIE_NAME, out string themeCookie);
            var theme = SiteTheme.Dark;
            if (cookieHasValue && !string.IsNullOrEmpty(themeCookie))
            {
                Enum.TryParse(themeCookie, out theme);
            }

            var themeObj = new Theme(theme);
            context.Features.Set(themeObj);

            return next();
        }

        public static IApplicationBuilder UseThemes(this IApplicationBuilder app) => app.Use(LoadThemeFromCookie);
    }
}