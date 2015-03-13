using System.Web.Mvc;

namespace Quintsys.Web.ThemeableRazorViewEngine
{
    public abstract class ThemeController : Controller
    {
        protected ThemeController(string themeName)
        {
            CurrentTheme = new Theme {Name = themeName};
        }

        public Theme CurrentTheme { get; private set; }
    }
}