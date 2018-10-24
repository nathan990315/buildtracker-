using System;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;

namespace BuildFeed
{
    public static class MvcExtensions
    {
        public static string CheckboxListForEnum<T>(this HtmlHelper html, string id, T currentItem)
            where T : Enum
        {
            var sb = new StringBuilder();

            foreach (T enumItem in Enum.GetValues(typeof(T)).Cast<T>())
            {
                long enumValue = Convert.ToInt64(enumItem);
                long currentValue = Convert.ToInt64(currentItem);

                if (enumValue == 0)
                {
                    // skip 0-valued bitflags, they're for display only.
                    continue;
                }

                var wrapper = new TagBuilder("div");
                wrapper.Attributes.Add("class", "checkbox");

                var label = new TagBuilder("label")
                {
                    TagRenderMode = TagRenderMode.Normal
                };

                var input = new TagBuilder("input")
                {
                    TagRenderMode = TagRenderMode.SelfClosing
                };
                if ((enumValue & currentValue) != 0)
                {
                    input.MergeAttribute("checked", "checked");
                }

                input.MergeAttribute("type", "checkbox");
                input.MergeAttribute("value", enumValue.ToString());
                input.MergeAttribute("name", id);

                label.InnerHtml.AppendHtml(input.ToString());
                label.InnerHtml.Append(GetDisplayTextForEnum(enumItem));

                wrapper.InnerHtml.AppendHtml(label.ToString());

                sb.Append(wrapper);
            }

            return sb.ToString();
        }

        public static string GetDisplayTextForEnum<T>(T enumObj)
            where T : Enum
        {
            string result = null;
            DisplayAttribute display = enumObj.GetType()
                .GetMember(enumObj.ToString())
                .First()
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .LastOrDefault();

            if (display != null)
            {
                result = display.GetName();
            }

            return result ?? enumObj.ToString();
        }

        public static string GetDisplayDescriptionForEnum<T>(T enumObj)
            where T : Enum
        {
            string result = null;
            DisplayAttribute display = enumObj.GetType()
                .GetMember(enumObj.ToString())
                .First()
                .GetCustomAttributes(false)
                .OfType<DisplayAttribute>()
                .LastOrDefault();

            if (display != null)
            {
                result = display.GetDescription() ?? display.GetName();
            }

            return result ?? enumObj.ToString();
        }

        public static string ToLongDateWithoutDay(this DateTime dt)
        {
            string s = CultureInfo.CurrentUICulture.DateTimeFormat.LongDatePattern;
            s = s.Replace("dddd", "").Replace("ddd", "");
            s = s.Trim(' ', ',');

            return dt.ToString(s);
        }
    }
}