using System;
using System.Web.Mvc;
using Quintsys.Web.ThemeableRazorViewEngine.Annotations;

namespace Quintsys.Web.ThemeableRazorViewEngine
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Application absolute path for an image file, taking themes into  consideration. Default theme name = 'Default'.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeImageUrl(this UrlHelper urlHelper, string filename)
        {
            return ThemeImageUrl(urlHelper, filename, null);
        }

        /// <summary>
        /// Application absolute path for an image file, taking themes into  consideration.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeImageUrl(this UrlHelper urlHelper, string filename, string themeName)
        {
            return ContentUrl(urlHelper, ContentLocationFormats.ImagesPath, filename, themeName);
        }

        /// <summary>
        /// Application absolute path for a stylesheet file, taking themes into  consideration. Default theme name = 'Default'.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeStylesheetUrl(this UrlHelper urlHelper, string filename)
        {
            return ThemeStylesheetUrl(urlHelper, filename, null);
        }

        /// <summary>
        /// Application absolute path for a stylesheet file, taking themes into  consideration.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeStylesheetUrl(this UrlHelper urlHelper, string filename, string themeName)
        {
            return ContentUrl(urlHelper, ContentLocationFormats.StylesheetsPath, filename, themeName);
        }

        private static string ContentUrl(UrlHelper urlHelper,
            [NotNull] string pathTemplate,
            [NotNull] string filename,
            string themeName = "Default")
        {
            if (pathTemplate == null)
                throw new ArgumentNullException("pathTemplate");
            if (filename == null)
                throw new ArgumentNullException("filename");

            var contentPath = string.Format(pathTemplate, themeName, filename);
            return urlHelper.Content(contentPath);
        }

        private static class ContentLocationFormats
        {
            public const string ImagesPath =        "~/Themes/{0}/Content/images/{1}";
            public const string StylesheetsPath =   "~/Themes/{0}/Content/css/{1}";
        }
    }
}