using Microsoft.Owin;
using Owin;
using System;

namespace Sklep_internetowy
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
 
        }     
    }
}
