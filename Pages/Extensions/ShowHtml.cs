using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text.Encodings.Web;

namespace ReservationProject.Pages.Extensions
{
    public static class ShowHtml
    {
        public static IHtmlContent Show<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> getMethod) => Show(html, getMethod, getMethod);
        public static IHtmlContent Show<TModel, TResult1, TResult2>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult1>> getLabelMethod, Expression<Func<TModel, TResult2>> getValueMethod = null) 
        {
            var labelStr = html.DisplayNameFor(getLabelMethod);
            var valueStr = (getValueMethod is null) ? GetValue(html, getLabelMethod) : GetValue(html, getValueMethod);
            return html.Show(labelStr, valueStr);
        }
        public static IHtmlContent Show<TModel>(this IHtmlHelper<TModel> html, string label, string value)
        {
            if (html == null) throw new ArgumentNullException(nameof(html));
            var s = HtmlStrings(html, label, value);
            return new HtmlContentBuilder(s);
        }
        internal static List<object> HtmlStrings<TModel>(IHtmlHelper<TModel> h, string label, string value)
        {
            return new List<object> {
                new HtmlString("<dt class=\"col-sm-2\">"),
                h.Raw(label),
                new HtmlString("</dt>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                h.Raw(value),
                new HtmlString("</dd>")
            };
        }
        internal static string GetValue<TModel, TResult>(IHtmlHelper<TModel> h, Expression<Func<TModel, TResult>> e)
        {
            var value = h.DisplayFor(e);
            var writer = new System.IO.StringWriter();
            value.WriteTo(writer, HtmlEncoder.Default);
            return writer.ToString();
        }
    }
}
