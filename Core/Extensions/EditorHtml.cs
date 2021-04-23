using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ReservationProject.Core.Extensions
{
    public static class EditorHtml
    {
        public static IHtmlContent Editor<TModel, TResult>(this IHtmlHelper<TModel> html, 
            Expression<Func<TModel, TResult>> getMethod) => Editor(html, getMethod, getMethod);
        public static IHtmlContent Editor<TModel, TResult1, TResult2>(this IHtmlHelper<TModel> html, 
            Expression<Func<TModel, TResult1>> label, Expression<Func<TModel, TResult2>> value)
        {
            var labelString = html.DisplayNameFor(label);
            return Editor(html, value, labelString);
        }
        public static IHtmlContent Editor<TModel, TResult>(this IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> value, string label)
        {
            var s = HtmlStrings(html, value, label);
            return new HtmlContentBuilder(s);
        }
        internal static List<object> HtmlStrings<TModel, TResult>(IHtmlHelper<TModel> html,
            Expression<Func<TModel, TResult>> value, string label)
        {
            return new List<object> {
                new HtmlString("<dd class=\"col-sm-10\">"),
                html.Raw(label),
                new HtmlString("</dd>"),
                new HtmlString("<dd class=\"col-sm-10\">"),
                html.EditorFor(value, new {htmlAttributes = new {@class = "form-control"}}),
                html.ValidationMessageFor(value, "", new {@class = "text-danger"}),
                new HtmlString("</dd>")
            };
        }
    }
}
