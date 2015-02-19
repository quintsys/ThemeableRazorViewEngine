using System.Web.Mvc;

namespace Quintsys.Web.ThemeableRazorViewEngine.MVC5
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