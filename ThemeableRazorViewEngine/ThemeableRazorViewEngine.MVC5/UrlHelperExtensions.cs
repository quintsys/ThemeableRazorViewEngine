using System.Web.Mvc;
using Quintsys.Web.ThemeableRazorViewEngine.Annotations;

namespace Quintsys.Web.ThemeableRazorViewEngine
{
    public static class UrlHelperExtensions
    {
        /// <summary>
        /// Application absolute path for a theme's image file.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename for the image.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeImageUrl(this UrlHelper urlHelper, string filename)
        {
            return ThemeImageUrl(urlHelper, filename, null);
        }

        /// <summary>
        /// Application absolute path for a theme's image file.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename for the image.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeImageUrl(this UrlHelper urlHelper, string filename, string themeName)
        {
            var virtualPath = ThemeVirtualPaths.Image(filename, themeName);
            return urlHelper.Content(virtualPath);
        }

        /// <summary>
        /// Application absolute path for a theme's stylesheet file.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename for the stylesheet.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeStylesheetUrl(this UrlHelper urlHelper, string filename)
        {
            return ThemeStylesheetUrl(urlHelper, filename, null);
        }

        /// <summary>
        /// Application absolute path for a theme's stylesheet file.
        /// </summary>
        /// <param name="urlHelper">The URL helper.</param>
        /// <param name="filename">The filename for the stylesheet.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        [UsedImplicitly]
        public static string ThemeStylesheetUrl(this UrlHelper urlHelper, string filename, string themeName)
        {
            var virtualPath = ThemeVirtualPaths.Stylesheet(filename, themeName);
            return urlHelper.Content(virtualPath);
        }
    }
}