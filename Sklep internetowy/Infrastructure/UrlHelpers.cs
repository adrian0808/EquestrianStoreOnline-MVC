using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Sklep_internetowy.Infrastructure
{
    public static class UrlHelpers
    {
        public static string GraphicsFilePath(this UrlHelper helper, string GraphicFileName)
        {
            var GraphicFileFolder = AppConfig.GetGraphicFileRelativePath;
            var path = Path.Combine(GraphicFileFolder, GraphicFileName);
            var GraphicFileAbsolutePath = helper.Content(path);
            return GraphicFileAbsolutePath;
        }
    }
}