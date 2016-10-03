using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Data.Enumerations;
using SistemaChamadosFiap.Web.Infrastructure;
using SistemaChamadosFiap.Web.Infrastructure.Extensions;
using SistemaChamadosFiap.Web.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SistemaChamadosFiap.Web.Controllers
{
    [Authorize]
    public class ChamadoController : BaseController<tbChamado, ChamadoModel>
    {
        [HttpGet]
        public override ActionResult Index()
        {
            return base.Index();
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

        [HttpGet]
        [ChildActionOnly]
        public PartialViewResult ModalInteracao()
        {
            return PartialView(new ChamadoInteracaoModel());
        }
        [HttpPost]
        public ActionResult ModalInteracao(ChamadoInteracaoModel model)
        {
            if (ModelState.IsValid)
            {
                model.Id = 0;
                model.DtInteracao = DateTime.Now;
                ViewBag.PropertyCollectionName = "Interacoes";
                return PartialView("~/Views/Chamado/EditorTemplates/ChamadoInteracaoModel.cshtml", model);
            }

            return new HttpStatusCodeResult(HttpStatusCode.InternalServerError);
        }

        public override void AtualizarEntidadeAntesDeSalvar(tbChamado atualizado)
        {
            foreach (var interacao in atualizado.tbChamadoInteracaos)
            {
                if (interacao.Id == 0)
                    _db.Entry(interacao).State = EntityState.Added;
            }
        }

        public async Task<ActionResult> ExcluirInteracao(int id)
        {
            var dbSet = _db.Set<tbChamadoInteracao>();
            var interacao = await dbSet.FindAsync(id);
            if (interacao == null) return HttpNotFound();
            dbSet.Remove(interacao);
            await _db.SaveChangesAsync();
            return new HttpStatusCodeResult(HttpStatusCode.OK);
        }

        public override void CarregarAdicionar(ChamadoModel model)
        {
            CarregarViewBagsPadrao();
        }

        private void CarregarViewBagsPadrao()
        {
            ViewBag.Prioridades = new SelectList(Enum.GetValues(typeof(PrioridadeTipo))
                .Cast<PrioridadeTipo>()
               .ToDictionary(t => (byte)t, t => t.ToString()), "Key", "Value");

            if (ClaimsPrincipal.Current.HasClaimRole("Administrador") || ClaimsPrincipal.Current.HasClaimRole("Atendente"))
            {
                ViewBag.Status = new SelectList(Enum.GetValues(typeof(StatusTipo)).Cast<StatusTipo>().ToDictionary(t => (byte)t, t => t.ToString()), "Key", "Value");
            }
        }

        public override void CarregarEditar(ChamadoModel model)
        {
            CarregarViewBagsPadrao();
        }

        public override void AntesExcluir(tbChamado chamado)
        {
            foreach (var interacao in chamado.tbChamadoInteracaos.ToList())
                _db.Set<tbChamadoInteracao>().Remove(interacao);
        }

        [HttpGet]
        public async Task<ActionResult> EncerrarChamado(int id)
        {
            var chamado = await _db.Set<tbChamado>().FindAsync(id);
            if (chamado == null) return HttpNotFound();

            chamado.Status = (byte)StatusTipo.Finalizado;
            _db.Entry(chamado).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public async Task<ActionResult> ReabrirChamado(int id)
        {
            var chamado = await _db.Set<tbChamado>().FindAsync(id);
            if (chamado == null) return HttpNotFound();

            chamado.Status = (byte)StatusTipo.Aguardando;
            _db.Entry(chamado).State = EntityState.Modified;

            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public override void CarregarIndex()
        {
            ViewBag.Status = new SelectList(Enum.GetValues(typeof(StatusTipo)).Cast<StatusTipo>().ToDictionary(t => (byte)t, t => t.ToString()), "Key", "Value");
        }

        public override IQueryable<tbChamado> Query
        {
            get
            {
                if (ClaimsPrincipal.Current.HasClaimRole("Administrador")
                    || ClaimsPrincipal.Current.HasClaimRole("Atendente"))
                    return base.Query;

                var claimCliente = ClaimsPrincipal.Current.FindFirst(p => p.Type == CustomClaimTypes.ClientId);
                if (claimCliente == null) throw new Exception("Erro ao recuperar Claim.");
                int idCliente = 0;
                if (!int.TryParse(claimCliente.Value, out idCliente)) throw new Exception("Erro ao converter Claim.");

                return base.Query.Where(p => p.IdCliente == idCliente);
            }
        }

        public override ActionResult CarregarDadosIndex()
        {
            if (Request.QueryString["status"] == null)
                return base.CarregarDadosIndex();

            byte status = Convert.ToByte(Request.QueryString["status"]);

            return PartialView("_Dados", Query.Where(p => p.Status == status));
        }

        public override void AntesDeSalvar(tbChamado chamado)
        {
            if (chamado.Id == 0)
            {
                chamado.IdCliente = 1; //Convert.ToInt32(ClaimsPrincipal.Current.FindFirst(p => p.Type == CustomClaimTypes.ClientId).Value);
            }
        }
    }
}