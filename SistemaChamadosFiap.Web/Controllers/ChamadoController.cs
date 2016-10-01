using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Models;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SistemaChamadosFiap.Web.Controllers
{
    public class ChamadoController : BaseController<tbChamado, ChamadoModel>
    {
        [HttpGet]
        public override Task<ActionResult> Index()
        {
            return base.Index();
        }

        [HttpGet]
        public override ActionResult Adicionar()
        {
            return base.Adicionar();
        }

        [HttpPost]
        public override Task<ActionResult> Adicionar(ChamadoModel model)
        {
            return base.Adicionar(model);
        }

        [HttpGet]
        public override Task<ActionResult> Editar(int id)
        {
            return base.Editar(id);
        }


        [HttpPost]

        public override Task<ActionResult> Editar(ChamadoModel model)
        {
            return base.Editar(model);
        }

        [HttpGet]
        public override Task<ActionResult> Excluir(int id)
        {
            return base.Excluir(id);
        }
    }
}