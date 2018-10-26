using System.Globalization;

namespace BuildFeed.Options
{
    public class Locale
    {
        public static readonly Locale[] AvailableLocales =
        {
            new Locale("ar"), new Locale("cs"), new Locale("de"), new Locale("el"), new Locale("en"),
            new Locale("es"), new Locale("fa"), new Locale("fi"), new Locale("fr"), new Locale("he"),
            new Locale("hr"), new Locale("hu"), new Locale("id"), new Locale("it"), new Locale("ja"),
            new Locale("ko"), new Locale("lt"), new Locale("nl"), new Locale("pl"), new Locale("pt"),
            new Locale("pt-BR"), new Locale("ro"), new Locale("ru"), new Locale("sk"), new Locale("sl"),
            new Locale("sv"), new Locale("tr"), new Locale("uk"), new Locale("vi"), new Locale("zh-Hans"),
            new Locale("zh-Hant")
        };

        public string DisplayName => Info.NativeName;

        public CultureInfo Info { get; }
        public string LocaleId { get; }

        private Locale(string localeId)
        {
            LocaleId = localeId;
            Info = CultureInfo.GetCultureInfo(localeId);
        }
    }
}