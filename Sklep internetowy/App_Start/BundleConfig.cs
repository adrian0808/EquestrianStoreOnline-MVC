using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Optimization;

namespace Sklep_internetowy.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundle(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/JqueryAndJqueryui").Include(
                                         "~/Scripts/jquery-{version}.js",
                                         "~/Scripts/jquery-ui-{version}.js"
                                         ));

            bundles.Add(new StyleBundle("~/bundles/css").Include(
                                        "~/Content/themes/base/core.css",
                                        "~/Content/themes/base/autocomplete.css",
                                        "~/Content/themes/base/theme.css",
                                        "~/Content/themes/base/menu.css"
                                        ));

        }
    }
}