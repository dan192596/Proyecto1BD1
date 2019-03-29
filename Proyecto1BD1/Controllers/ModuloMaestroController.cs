using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto1BD1.Controllers
{
    public class ModuloMaestroController : Controller
    {
        // GET: ModuloMaestro
        public ActionResult Index()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            ViewData["Message"] = "Modulo Maestro.";
            return View();
        }

        // GET: ModuloMaestro
        public ActionResult MaterialApoyo()
        {
            ViewData["Message"] = "Modulo Maestro.";
            return View();
        }

        public ActionResult Actividades()
        {
            ViewData["Message"] = "Modulo Maestro.";
            return View();
        }

        // GET: ModuloMaestro
        public ActionResult Alumnos()
        {
            ViewData["Message"] = "Modulo Maestro.";
            return View();
        }

        // GET: ModuloMaestro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModuloMaestro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuloMaestro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModuloMaestro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModuloMaestro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: ModuloMaestro/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModuloMaestro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}