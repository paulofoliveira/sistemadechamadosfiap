using AutoMapper;
using SistemaChamadosFiap.Data;
using SistemaChamadosFiap.Web.Models;
using System;
using System.Data.Entity;
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
       

        [HttpGet]
        public virtual async Task<ActionResult> Index()
        {
            using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
            {
                var model = await db.Set<TEntity>().ToListAsync();
                return View(model);
            }
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

                    using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
                    {
                        db.Set<TEntity>().Add(entidade);
                        await db.SaveChangesAsync();
                    }

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

        [HttpGet]
        public virtual async Task<ActionResult> Editar(int id)
        {
            using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
            {
                var entidade = await db.Set<TEntity>().FindAsync(id);

                if (entidade == null) return HttpNotFound();

                var model = Mapper.Map<TModel>(entidade);
                CarregarEditar(model);
                return View(model);
            }
        }

        [HttpPost]
        public virtual async Task<ActionResult> Editar(TModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TEntity atualizado = Mapper.Map<TEntity>(model);

                    using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
                    {           
                        db.Set<TEntity>().Attach(atualizado);
                        db.Entry(atualizado).State = EntityState.Modified;
                        AtualizarEntidadeAntesDeSalvar(db, atualizado);
                        await db.SaveChangesAsync();
                    }

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

        public virtual void AtualizarEntidadeAntesDeSalvar(DbContext db, TEntity atualizado)
        {

        }

        public virtual async Task<ActionResult> Excluir(int id)
        {
            using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
            {
                var entidade = await db.Set<TEntity>().FindAsync(id);

                if (entidade == null) return HttpNotFound();
                AntesExcluir(db, entidade);
                db.Set<TEntity>().Remove(entidade);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
        }

        public virtual void AntesExcluir(DbContext db, TEntity entidade)
        {

        }
    }
}