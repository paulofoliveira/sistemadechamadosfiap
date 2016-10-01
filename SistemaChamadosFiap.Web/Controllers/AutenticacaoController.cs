using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Owin.Security;
using SistemaChamadosFiap.Web.Models;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadosFiap.Web.Controllers
{
    public class AutenticacaoController : Controller
    {
        [Authorize]
        [HttpGet]
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View(new LoginModel());
        }

        [HttpPost]
        public async Task<ActionResult> Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                using (var store = new UserStore<IdentityUser>())
                {
                    using (var manager = new UserManager<IdentityUser>(store))
                    {
                        var usuario = await manager.FindAsync(model.Login, model.Senha);

                        if (usuario == null)
                            ModelState.AddModelError("", "Usuário ou senha inválidos.");
                        else
                        {
                            var identity = manager.CreateIdentity(usuario, DefaultAuthenticationTypes.ApplicationCookie);
                            AuthenticationManager.SignIn(new AuthenticationProperties() { IsPersistent = false }, identity);
                            return RedirectToAction("Index");
                        }
                    }
                }
            }

            return View(model);
        }

        public ActionResult Logout()
        {
            AuthenticationManager.SignOut();
            return RedirectToAction("Index");
        }

        [HttpGet]
        public ActionResult Criar()
        {
            return View(new UsuarioModel());
        }


        [HttpPost]
        public async Task<ActionResult> Criar(UsuarioModel model)
        {
            if (ModelState.IsValid)
            {
                using (var store = new UserStore<IdentityUser>())
                {
                    using (var manager = new UserManager<IdentityUser>(store))
                    {
                        var usuario = new IdentityUser(model.Login);
                        IdentityResult resultado = await manager.CreateAsync(usuario, model.Senha);

                        if (resultado.Succeeded)                       
                            return await Login(new LoginModel() { Login = model.Login, Senha = model.Senha });                        
                        else
                            ModelState.AddModelError("", resultado.Errors.FirstOrDefault());
                    }
                }
            }

            return View(model);
        }


        private IAuthenticationManager AuthenticationManager
        {

            get
            {
                return System.Web.HttpContext.Current.GetOwinContext().Authentication;
            }
        }

    }
}