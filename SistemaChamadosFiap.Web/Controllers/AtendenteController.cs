using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using System.Security.Claims;
using SistemaChamadosFiap.Web.Infrastructure;

namespace SistemaChamadosFiap.Web.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class AtendenteController : BaseController<tbAtendente, AtendenteModel>
    {
        [HttpGet]
        public override ActionResult Index()
        {
            return base.Index();
        }

        [HttpGet]
        public override ActionResult Adicionar()
        {
            return base.Adicionar();
        }

        [HttpPost]
        public override Task<ActionResult> Adicionar(AtendenteModel model)
        {
            return base.Adicionar(model);
        }

        [HttpGet]
        public override Task<ActionResult> Editar(int id)
        {
            return base.Editar(id);
        }


        [HttpPost]

        public override Task<ActionResult> Editar(AtendenteModel model)
        {
            return base.Editar(model);
        }

        [HttpGet]
        public override Task<ActionResult> Excluir(int id)
        {
            return base.Excluir(id);
        }

        public override void DepoisDeSalvar(tbAtendente atendente)
        {
            // Salva usuário do AspNetUsers.

            using (var store = new UserStore<IdentityUser>())
            {
                using (var manager = new UserManager<IdentityUser>(store))
                {
                    var usuario = store.Users.FirstOrDefault(p => p.UserName == atendente.Email);

                    if (usuario == null)
                    {
                        usuario = new IdentityUser(atendente.Email);

                        IdentityResult resultado = manager.Create(usuario, atendente.Senha);
                        manager.AddClaim(usuario.Id, new Claim(ClaimTypes.Role, "Cliente"));
                        manager.AddClaim(usuario.Id, new Claim(CustomClaimTypes.ClientId, atendente.Id.ToString()));
                    }
                }
            }
        }
    }
}