using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Configuration;
using Proyecto1BD1.Models;

namespace Proyecto1BD1.Controllers
{
    public class MaestroCarreraController : Controller
    {
        private readonly Proyecto1Context _context=new Proyecto1Context();
        private readonly IConfiguration configuration;        

        public MaestroCarreraController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: MaestroCarrera
        public ActionResult Index()
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM maestro_carrera", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Registro
                numb.Add(sdr[1].ToString());//Carrera
                numb.Add(sdr[2].ToString());//Curso
                al.Add(numb);
            }
            connection.Close();
            return View(al);
        }

        // GET: MaestroCarrera/Create
        public ActionResult Create()
        {
            var Maestros = (from item in _context.Maestro
                              select new SelectListItem()
                              {
                                  Value = item.Registro.ToString(),
                                  Text = item.Nombre.ToString()
                              }).ToList();
            var Carreras = (from item in _context.Carrera
                            select new SelectListItem()
                            {
                                Value = item.Carrera1.ToString(),
                                Text = item.Nombre.ToString()
                            }).ToList();
            var Cursos = (from item in _context.Curso
                           select new SelectListItem()
                           {
                               Value = item.Curso1.ToString(),
                               Text = item.Nombre.ToString()
                           }).ToList();
            ViewData["Maestros"] = Maestros;
            ViewData["Carreras"] = Carreras;
            ViewData["Cursos"] = Cursos;
            return View();
        }

        // POST: MaestroCarrera/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                String Registro = "";
                String Carrera = "";
                String Curso = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "registro":
                            Registro = item.Value;
                            break;
                        case "carrera":
                            Carrera = item.Value;
                            break;
                        case "curso":
                            Curso = item.Value;
                            break;
                    }
                }
                if (Registro != "" && Carrera != "" && Curso!="")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "INSERT INTO maestro_carrera(registro,carrera,curso) VALUES  (@param1,@param2,@param3)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = Registro;
                        cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = Carrera;
                        cmd.Parameters.Add("@param3", SqlDbType.Int).SqlValue = Curso;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var Maestros = (from item in _context.Maestro
                                select new SelectListItem()
                                {
                                    Value = item.Registro.ToString(),
                                    Text = item.Nombre.ToString()
                                }).ToList();
                var Carreras = (from item in _context.Carrera
                                select new SelectListItem()
                                {
                                    Value = item.Carrera1.ToString(),
                                    Text = item.Nombre.ToString()
                                }).ToList();
                var Cursos = (from item in _context.Curso
                              select new SelectListItem()
                              {
                                  Value = item.Curso1.ToString(),
                                  Text = item.Nombre.ToString()
                              }).ToList();
                ViewData["Maestros"] = Maestros;
                ViewData["Carreras"] = Carreras;
                ViewData["Cursos"] = Cursos;
                return View();
            }
        }

        // GET: MaestroCarrera/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: MaestroCarrera/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                //No se edita porque todas son primarias
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: MaestroCarrera/Delete/5
        public ActionResult Delete(int registro, int carrera,int curso)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>();
            connection.Open();
            String sql = " SELECT * FROM maestro_carrera WHERE maestro_carrera.registro = @param1 AND maestro_carrera.carrera =@param2 AND maestro_carrera.curso  = @param3";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = registro;
                cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = carrera;
                cmd.Parameters.Add("@param3", SqlDbType.Int).SqlValue = curso;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    numb.Add(sdr[0].ToString());//Maestro
                    numb.Add(sdr[1].ToString());//Carrera
                    numb.Add(sdr[2].ToString());//Curso
                }
            }
            connection.Close();
            return View(numb);
        }

        // POST: MaestroCarrera/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int registro, int carrera, int curso, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                String sql = "DELETE FROM maestro_carrera WHERE maestro_carrera.registro = @param1 AND maestro_carrera.carrera =@param2 AND maestro_carrera.curso  = @param3";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    System.Diagnostics.Debug.WriteLine("r"+registro+" c"+carrera+" cu"+curso);
                    cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = registro;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = carrera;
                    cmd.Parameters.Add("@param3", SqlDbType.Int).SqlValue = curso;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
        // GET: MaestroCarrera/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}