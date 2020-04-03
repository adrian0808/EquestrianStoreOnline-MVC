using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Sklep_internetowy.Infrastructure
{
    public class AppConfig
    {
        private static string GraphicFileRelativePath = ConfigurationManager.AppSettings["GraphicFile"];

        public static string GetGraphicFileRelativePath
        {
            get
            {
                return GraphicFileRelativePath;
            }
        }
    }
}