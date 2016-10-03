using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Linq;

namespace SistemaChamadosFiap.Web
{
    public class Global : HttpApplication
    {
        void Application_Start(object sender, EventArgs e)
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            AutoMapperConfig.Initialize();

            // Incializando ADM:

            using (var store = new UserStore<IdentityUser>())
            {
                using (var manager = new UserManager<IdentityUser>(store))
                {
                    var usuario = store.Users.FirstOrDefault(p => p.UserName == "administrador@fiap.com.br");

                    if (usuario == null)
                    {
                        usuario = new IdentityUser("administrador@fiap.com.br");

                        IdentityResult resultado = manager.CreateAsync(usuario, "123456").Result;
                        var identityResult = manager.AddClaimAsync(usuario.Id, new Claim(ClaimTypes.Role, "Administrador")).Result;
                    }
                }
            }

        }
    }
}