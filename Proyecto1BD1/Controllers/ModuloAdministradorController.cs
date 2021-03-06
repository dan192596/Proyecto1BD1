﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Proyecto1BD1.Controllers
{
    public class ModuloAdministradorController : Controller
    {
        // GET: ModuloAdministrador
        public ActionResult Index()
        {
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Message"] = "Modulo Administrador.";
            return View();
        }

        // GET: ModuloAdministrador/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: ModuloAdministrador/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ModuloAdministrador/Create
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

        // GET: ModuloAdministrador/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: ModuloAdministrador/Edit/5
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

        // GET: ModuloAdministrador/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: ModuloAdministrador/Delete/5
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