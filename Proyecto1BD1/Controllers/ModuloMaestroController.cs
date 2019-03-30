using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Proyecto1BD1.Controllers
{
    public class ModuloMaestroController : Controller
    {
        private readonly IConfiguration configuration;
        public ModuloMaestroController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: ModuloMaestro
        public ActionResult Index()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            ViewData["Message"] = "Modulo Maestro.";

            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM publicacion", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Id
                numb.Add(sdr[1].ToString());//Descripcion
                numb.Add(sdr[2].ToString());//Fecha y hora
                al.Add(numb);
            }
            connection.Close();
            return View(al);

            return View();
        }

        // POST: ModuloMaestro/Index
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

        // GET: ModuloMaestro
        public ActionResult MaterialApoyo()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            ViewData["Message"] = "Modulo Maestro.";

            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            String sql = "EXEC VER_MATERIALAPOYO";
            SqlCommand com = new SqlCommand(sql, connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Titulo
                numb.Add(sdr[1].ToString());//Descripcion
                numb.Add(sdr[2].ToString());//Materia
                numb.Add(sdr[3].ToString());//Tipo de publicacion
                numb.Add(sdr[4].ToString());//Fecha y hora
                al.Add(numb);
            }
            connection.Close();
            return View(al);

            return View();
        }

        //GET: ModuloMaestro/Actividades
        public ActionResult Actividades()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            ViewData["Message"] = "Modulo Maestro.";
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
            ViewData["Message"] = "Modulo Maestro.";

            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            String sql = "EXEC VER_ACTIVIDADES";
            SqlCommand com = new SqlCommand(sql, connection);
            SqlDataReader sdr = com.ExecuteReader();
            List<String> aux = new List<string>();
            List<String> aux2 = new List<string>();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Titulo
                numb.Add(sdr[1].ToString());//Nombre
                aux.Add(sdr[1].ToString());//Nombre
                numb.Add(sdr[2].ToString());//Descripcion
                numb.Add(sdr[3].ToString());//fecha de publicacion
                numb.Add(sdr[4].ToString());//Nota
                numb.Add(sdr[5].ToString());//Fecha Limite
                al.Add(numb);
            }
            connection.Close();

            return View(al);
            return View();
        }

        // POST: ModuloMaestro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Actividades(IFormCollection collection)
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

        // GET: ModuloMaestro
        public ActionResult Alumnos()
        {
            ViewData["Codigo"] = VariablesGlobales.Codigo;
            ViewData["Nombre"] = VariablesGlobales.Nombre;
            ViewData["Apellido"] = VariablesGlobales.Apellido;
            ViewData["Fotografia"] = VariablesGlobales.Fotografia;
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