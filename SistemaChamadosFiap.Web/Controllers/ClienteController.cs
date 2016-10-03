using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Models;
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
    }
}