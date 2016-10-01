using AutoMapper;
using SistemaChamadosFiap.Data;
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
        where TModel : class
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
            return View(Activator.CreateInstance<TModel>());
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
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

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
                return View(model);
            }
        }

        [HttpGet]
        public virtual async Task<ActionResult> Editar(TModel model)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    TEntity entidade = Mapper.Map<TEntity>(model);

                    using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
                    {
                        db.Set<TEntity>().Attach(entidade);
                        db.Entry(entidade).State = EntityState.Modified;
                        await db.SaveChangesAsync();
                    }
                }

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message);
            }

            return View(model);

        }

        public virtual async Task<ActionResult> Excluir(int id)
        {
            using (SistemaChamadosFiapEntities db = new SistemaChamadosFiapEntities())
            {
                var entidade = await db.Set<TEntity>().FindAsync(id);

                if (entidade == null) return HttpNotFound();
                db.Set<TEntity>().Remove(entidade);
                return RedirectToAction("Index");
            }   
        }
    }
}