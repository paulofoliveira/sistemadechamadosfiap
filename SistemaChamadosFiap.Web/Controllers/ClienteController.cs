using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Infrastructure;
using SistemaChamadosFiap.Web.Models;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SistemaChamadosFiap.Web.Controllers
{
    [Authorize(Roles = "Administrador, Atendente")]
    public class ClienteController : BaseController<tbCliente, ClienteModel>
    {
        [HttpGet]
        public override ActionResult Index()
        {
            return base.Index();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public override ActionResult Adicionar()
        {
            return base.Adicionar();
        }

        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public override Task<ActionResult> Adicionar(ClienteModel model)
        {
            return base.Adicionar(model);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public override Task<ActionResult> Editar(int id)
        {
            return base.Editar(id);
        }


        [Authorize(Roles = "Administrador")]
        [HttpPost]
        public override Task<ActionResult> Editar(ClienteModel model)
        {
            return base.Editar(model);
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public override Task<ActionResult> Excluir(int id)
        {
            return base.Excluir(id);
        }

        public override void DepoisDeSalvar(tbCliente cliente)
        {
            // Salva usuário do AspNetUsers.

            using (var store = new UserStore<IdentityUser>())
            {
                using (var manager = new UserManager<IdentityUser>(store))
                {
                    var usuario = store.Users.FirstOrDefault(p => p.UserName == cliente.Email);

                    if (usuario == null)
                    {
                        usuario = new IdentityUser(cliente.Email);

                        IdentityResult resultado = manager.Create(usuario, cliente.Senha);
                        manager.AddClaim(usuario.Id, new Claim(ClaimTypes.Role, "Cliente"));
                        manager.AddClaim(usuario.Id, new Claim(CustomClaimTypes.ClientId, cliente.Id.ToString()));
                    }
                }
            }
        }

        private void CriarUsuario(string login, string senha)
        {

        }
    }
}