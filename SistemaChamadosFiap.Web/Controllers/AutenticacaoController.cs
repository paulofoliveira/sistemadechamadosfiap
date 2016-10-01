using SistemaChamadosFiap.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaChamadosFiap.Web.Controllers
{
    public class AutenticacaoController : Controller
    { 
        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // Processo de autenticação.
            }

            return View(model);
        }

    }
}