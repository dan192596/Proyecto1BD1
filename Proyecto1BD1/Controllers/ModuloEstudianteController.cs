using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto1BD1.Controllers
{
    public class ModuloEstudianteController : Controller
    {
        // GET: ModuloEstudiante
        public ActionResult Index()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            return View();
        }

        // GET: ModuloEstudiante
        public ActionResult MaterialApoyo()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            return View();
        }

        // GET: ModuloEstudiante
        public ActionResult Actividades()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            return View();
        }

        // GET: ModuloEstudiante
        public ActionResult Notas()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            return View();
        }

        // GET: ModuloEstudiante
        public ActionResult Notificaciones()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            return View();
        }

        // POST: ModuloEstudiante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(IFormCollection collection)
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

        // GET: ModuloEstudiante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModuloEstudiante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuloEstudiante/Create
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

        // GET: ModuloEstudiante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModuloEstudiante/Edit/5
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

        // GET: ModuloEstudiante/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModuloEstudiante/Delete/5
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