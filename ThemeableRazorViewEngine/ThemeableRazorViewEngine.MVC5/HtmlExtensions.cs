using System;
using System.Web.Mvc;
using System.Web.Routing;
using Quintsys.Web.ThemeableRazorViewEngine.Annotations;

namespace Quintsys.Web.ThemeableRazorViewEngine
{
    [UsedImplicitly]
    public static class HtmlExtensions
    {
        [UsedImplicitly]
        public static MvcHtmlString ThemeImage(this HtmlHelper htmlHelper, string theme, string path, object htmlAttributes)
        {
            if (String.IsNullOrEmpty(path))
            {
                throw new ArgumentException("path");
            } 

            var tagBuilder = new TagBuilder("img");
            tagBuilder.MergeAttribute("src", ThemeImagePath(htmlHelper, theme, path));
            tagBuilder.MergeAttributes(new RouteValueDictionary(htmlAttributes));

            return MvcHtmlString.Create(tagBuilder.ToString(TagRenderMode.SelfClosing));
        }

        private static string ThemeImagePath(HtmlHelper htmlHelper, string theme, string path)
        {
            UrlHelper urlHelper = ((Controller) htmlHelper.ViewContext.Controller).Url;
            return urlHelper.ThemeImageUrl(path, theme);
        }
    }
}