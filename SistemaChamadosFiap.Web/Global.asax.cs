using System;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace SistemaChamadosFiap.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {      
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Initialize();
        }
    }
}