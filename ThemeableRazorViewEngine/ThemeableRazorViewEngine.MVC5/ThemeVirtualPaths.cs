using System;
using Quintsys.Web.ThemeableRazorViewEngine.Annotations;

namespace Quintsys.Web.ThemeableRazorViewEngine
{
    public static class ThemeVirtualPaths
    {
        /// <summary>
        /// Virtual path for a theme's image. Default theme = 'Default'.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        public static string Image(string filename, string themeName)
        {
            return VirtualPath(ContentLocationFormats.Images, filename, themeName);
        }

        /// <summary>
        /// Virtual path for a theme's stylesheet. Default theme = 'Default'.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        public static string Stylesheet(string filename, string themeName)
        {
            return VirtualPath(ContentLocationFormats.Stylesheets, filename, themeName);
        }

        private static string VirtualPath([NotNull] string pathTemplate,
            [NotNull] string filename,
            string themeName = "Default")
        {
            if (pathTemplate == null)
                throw new ArgumentNullException("pathTemplate");
            if (filename == null)
                throw new ArgumentNullException("filename");

            return string.Format(pathTemplate, themeName, filename);
        }

        private static class ContentLocationFormats
        {
            public const string Images = "~/Themes/{0}/Content/images/{1}";
            public const string Stylesheets = "~/Themes/{0}/Content/css/{1}";
        }
    }
}