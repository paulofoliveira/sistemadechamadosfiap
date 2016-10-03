using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Data.Enumerations;
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
        public override Task<ActionResult> Index()
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

        public override void AtualizarEntidadeAntesDeSalvar(DbContext db, tbChamado atualizado)
        {
            foreach (var interacao in atualizado.tbChamadoInteracaos)
            {
                if (interacao.Id == 0)
                    db.Entry(interacao).State = EntityState.Added;
            }
        }

        public async Task<ActionResult> ExcluirInteracao(int id)
        {
            using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
            {
                var dbSet = db.Set<tbChamadoInteracao>();
                var interacao = await dbSet.FindAsync(id);
                if (interacao == null) return HttpNotFound();
                dbSet.Remove(interacao);
                await db.SaveChangesAsync();
                return new HttpStatusCodeResult(HttpStatusCode.OK);
            }
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

        public override void AntesExcluir(DbContext db, tbChamado chamado)
        {
            foreach (var interacao in chamado.tbChamadoInteracaos.ToList())
                db.Set<tbChamadoInteracao>().Remove(interacao);
        }

        [HttpGet]
        public async Task<ActionResult> EncerrarChamado(int id)
        {
            using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
            {
                var chamado = await db.tbChamadoes.FindAsync(id);
                if (chamado == null) return HttpNotFound();

                chamado.Status = (byte)StatusTipo.Finalizado;
                db.Entry(chamado).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }


        [HttpGet]
        public async Task<ActionResult> ReabrirChamado(int id)
        {
            using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
            {
                var chamado = await db.tbChamadoes.FindAsync(id);
                if (chamado == null) return HttpNotFound();

                chamado.Status = (byte)StatusTipo.Aguardando;
                db.Entry(chamado).State = EntityState.Modified;

                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }
    }
}