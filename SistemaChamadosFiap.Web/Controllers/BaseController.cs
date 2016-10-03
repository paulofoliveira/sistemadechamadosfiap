using AutoMapper;
using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace SistemaChamadosFiap.Web.Controllers
{
    /// <summary>
    /// Classe base controladora para operações de CRUD.
    /// </summary>
    /// <typeparam name="TModel"></typeparam>
    public class BaseController<TEntity, TModel> : Controller
        where TModel : BaseModel
        where TEntity : class
    {

        protected readonly DbContext _db;

        public BaseController()
        {
            _db = new SistemaChamadosFiapEntities();
        }

        [HttpGet]
        public virtual ActionResult Index()
        {
            CarregarIndex();
            return View();
        }

        [HttpGet]
        public virtual ActionResult Adicionar()
        {
            TModel model = Activator.CreateInstance<TModel>();
            CarregarAdicionar(model);
            return View(model);
        }

        public virtual void CarregarAdicionar(TModel model)
        {

        }

        public virtual void CarregarEditar(TModel model)
        {

        }

        [HttpPost]
        public virtual async Task<ActionResult> Adicionar(TModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TEntity entidade = Mapper.Map<TEntity>(model);
                    AntesDeSalvar(entidade);

                    _db.Set<TEntity>().Add(entidade);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            CarregarAdicionar(model);
            return View(model);
        }
        public virtual void AntesDeSalvar(TEntity entidade)
        {

        }

        [HttpGet]
        public virtual async Task<ActionResult> Editar(int id)
        {
            var entidade = await _db.Set<TEntity>().FindAsync(id);

            if (entidade == null) return HttpNotFound();

            var model = Mapper.Map<TModel>(entidade);
            CarregarEditar(model);
            return View(model);

        }

        [HttpPost]
        public virtual async Task<ActionResult> Editar(TModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TEntity atualizado = Mapper.Map<TEntity>(model);

                    _db.Set<TEntity>().Attach(atualizado);
                    _db.Entry(atualizado).State = EntityState.Modified;
                    AtualizarEntidadeAntesDeSalvar(atualizado);
                    await _db.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            CarregarEditar(model);
            return View(model);
        }

        public virtual void AtualizarEntidadeAntesDeSalvar(TEntity atualizado)
        {

        }

        public virtual async Task<ActionResult> Excluir(int id)
        {
            var entidade = await _db.Set<TEntity>().FindAsync(id);

            if (entidade == null) return HttpNotFound();
            AntesExcluir(entidade);
            _db.Set<TEntity>().Remove(entidade);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        public virtual void AntesExcluir(TEntity entidade)
        {

        }

        public virtual void CarregarIndex()
        {

        }

        public virtual IQueryable<TEntity> Query
        {
            get
            {
                return _db.Set<TEntity>();
            }
        }

        [HttpGet]
        public virtual ActionResult CarregarDadosIndex()
        {
            return PartialView("_Dados", Query);
        }
        
    }
}