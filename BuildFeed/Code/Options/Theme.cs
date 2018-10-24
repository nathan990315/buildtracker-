using System;
using System.Collections.Immutable;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using BuildFeed.Local;

namespace BuildFeed.Options
{
    public class Theme
    {
        public static readonly ImmutableArray<Theme> AvailableThemes = Enum.GetValues(typeof(SiteTheme))
            .Cast<SiteTheme>()
            .Select(st => new Theme(st))
            .ToImmutableArray();

        public string CookieValue => Value.ToString();
        public string CssPath => $"~/res/css/{Value.ToString().ToLower()}.css";
        public string DisplayName => MvcExtensions.GetDisplayTextForEnum(Value);
        public SiteTheme Value { get; }

        public Theme(SiteTheme st)
        {
            Value = st;
        }
    }

    public enum SiteTheme
    {
        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Common_ThemeDark))]
        Dark = 0,

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Common_ThemeLight))]
        Light,

        [Display(ResourceType = typeof(VariantTerms), Name = nameof(VariantTerms.Common_ThemeWinter))]
        Winter
    }
}