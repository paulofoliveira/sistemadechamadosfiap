using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading.Tasks;

namespace SistemaChamadosFiap.Web.Controllers
{
    //[Authorize]
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
    }
}