using System;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace BuildFeed.Middleware
{
    public static class LocaleMiddleware
    {
        private const string LANG_COOKIE_NAME = "bf_lang";

        private static Task LoadLocaleFromCookie(HttpContext context, Func<Task> next)
        {
            bool cookieHasValue = context.Request.Cookies.TryGetValue(LANG_COOKIE_NAME, out string langCookie);

            if (!cookieHasValue || string.IsNullOrEmpty(langCookie))
            {
                return next();
            }

            try
            {
                var ci = (CultureInfo)CultureInfo.GetCultureInfo(langCookie).Clone();

                // Get Gregorian Calendar in locale if available
                Calendar gc = ci.OptionalCalendars.FirstOrDefault(c
                    => c is GregorianCalendar calendar
                    && calendar.CalendarType == GregorianCalendarTypes.Localized);
                if (gc != null)
                {
                    ci.DateTimeFormat.Calendar = gc;
                }

                CultureInfo.CurrentCulture = ci;
                CultureInfo.CurrentUICulture = ci;
            }
            catch (CultureNotFoundException)
            {
            }

            return next();
        }

        public static IApplicationBuilder UseLocalisation(this IApplicationBuilder app) => app.Use(LoadLocaleFromCookie);
    }
}