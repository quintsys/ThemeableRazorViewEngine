using System;
using System.Linq;
using System.Web.Mvc;

namespace Quintsys.Web.ThemeableRazorViewEngine
{
    /// <summary>
    ///     Razor view engine with themes support.
    ///     Caching is allowed since the structure of the FinApp project allows for only one Theme by stack.
    ///     Currently supports C# only. Removed VB.NET support to increase perfomance on lookups.
    /// </summary>
    public class ThemeableRazorViewEngine : RazorViewEngine
    {
        #region private members and ctor

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThemeableRazorViewEngine" /> class.
        /// </summary>
        public ThemeableRazorViewEngine()
            : this(new[] { "cshtml", "vbhtml" })
        {
        }

        /// <summary>
        ///     Initializes a new instance of the <see cref="ThemeableRazorViewEngine" /> class.
        /// </summary>
        public ThemeableRazorViewEngine(params string[] fileExtensions)
        {
            if (fileExtensions == null)
                throw new ArgumentNullException("fileExtensions");

            FileExtensions = fileExtensions;

            string[] areaLocationFormats = CreateLocationFormats(LocationType.Area);
            AreaViewLocationFormats = areaLocationFormats;
            AreaMasterLocationFormats = areaLocationFormats;
            AreaPartialViewLocationFormats = areaLocationFormats;

            string[] defaultLocationFormats = CreateLocationFormats(LocationType.Default);
            ViewLocationFormats = defaultLocationFormats;
            MasterLocationFormats = defaultLocationFormats;
            PartialViewLocationFormats = defaultLocationFormats;
        }

        #region private members

        private string[] CreateLocationFormats(LocationType locationType)
        {
            string prefix = GetPrefixByLocationType(locationType);
            return FileExtensions
                .SelectMany(x => new[]
                {
                    string.Format("{0}/Themes/%1/Views/{{1}}/{{0}}.{1}", prefix, x),
                    string.Format("{0}/Themes/%1/Views/Shared/{{0}}.{1}", prefix, x),
                    string.Format("{0}/Views/{{1}}/{{0}}.{1}", prefix, x),
                    string.Format("{0}/Views/Shared/{{0}}.{1}", prefix, x)
                })
                .ToArray();
        }

        private static string GetPrefixByLocationType(LocationType locationType)
        {
            string prefix = null;
            switch (locationType)
            {
                case LocationType.Area:
                    prefix = "~/Areas/{2}";
                    break;
                case LocationType.Default:
                    prefix = "~";
                    break;
            }
            return prefix;
        }

        private enum LocationType
        {
            Default,
            Area
        }

        #endregion

        /// <summary>
        ///     Creates a partial view using the specified controller context and partial path, including the Theme name.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="partialPath">The partial path.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        private IView CreateThemePartialView(ControllerContext controllerContext, string partialPath, string themeName)
        {
            string themePath = ThemePath(partialPath, themeName);

            return base.CreatePartialView(controllerContext, themePath);
        }

        /// <summary>
        ///     Creates a view by using the specified controller context and the paths of the view and master view, including the
        ///     Theme name.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewPath">The view path.</param>
        /// <param name="masterPath">The master path.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        private IView CreateThemeView(ControllerContext controllerContext, string viewPath, string masterPath,
            string themeName)
        {
            string themeViewPath = ThemePath(viewPath, themeName);
            string themeMasterPath = ThemePath(masterPath, themeName);

            return base.CreateView(controllerContext, themeViewPath, themeMasterPath);
        }

        /// <summary>
        ///     Generates the correct theme path using a given theme name.
        /// </summary>
        /// <param name="virtualPath">The view path.</param>
        /// <param name="themeName">Name of the theme.</param>
        /// <returns></returns>
        private static string ThemePath(string virtualPath, string themeName)
        {
            const string themeNamePlaceholder = "%1";
            return virtualPath.Replace(themeNamePlaceholder, themeName);
        }

        #endregion

        protected override bool FileExists(ControllerContext controllerContext, string virtualPath)
        {
            using (var themeController = controllerContext.Controller as ThemeController)
            {
                if (themeController != null)
                {
                    return base.FileExists(controllerContext, ThemePath(virtualPath, themeController.CurrentTheme.Name));
                }
            }

            return base.FileExists(controllerContext, virtualPath);
        }

        /// <summary>
        ///     Creates a partial view using the specified controller context and partial path.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="partialPath">The path to the partial view.</param>
        /// <returns>
        ///     The partial view.
        /// </returns>
        protected override IView CreatePartialView(ControllerContext controllerContext, string partialPath)
        {
            using (var themeController = controllerContext.Controller as ThemeController)
            {
                if (themeController != null)
                {
                    return CreateThemePartialView(controllerContext, partialPath, themeController.CurrentTheme.Name);
                }
            }

            return base.CreatePartialView(controllerContext, partialPath);
        }

        /// <summary>
        ///     Creates a view by using the specified controller context and the paths of the view and master view.
        /// </summary>
        /// <param name="controllerContext">The controller context.</param>
        /// <param name="viewPath">The path to the view.</param>
        /// <param name="masterPath">The path to the master view.</param>
        /// <returns>
        ///     The view.
        /// </returns>
        protected override IView CreateView(ControllerContext controllerContext, string viewPath, string masterPath)
        {
            using (var themeController = controllerContext.Controller as ThemeController)
            {
                if (themeController != null)
                {
                    return CreateThemeView(controllerContext, viewPath, masterPath, themeController.CurrentTheme.Name);
                }
            }

            return base.CreateView(controllerContext, viewPath, masterPath);
        }
    }
}