using System.Web.Mvc;
using Quintsys.Web.ThemeableRazorViewEngine.MVC5;

namespace $rootnamespace$.App_Start {
    public static class ViewEnginesConfig
    {
        public static void RegisterViewEngines(ViewEngineCollection engines, params string[] fileExtensions )
        {
            engines.Clear();
            engines.Add(new ThemeRazorViewEngine(fileExtensions));
        }
    }
}
