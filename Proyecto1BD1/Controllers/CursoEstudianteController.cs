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
    public class CursoEstudianteController : Controller
    {
        private readonly Proyecto1Context _context = new Proyecto1Context();
        private readonly IConfiguration configuration;
        public CursoEstudianteController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: CursoEstudiante
        public ActionResult Index()
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM curso_estudiante", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Carnet
                numb.Add(sdr[1].ToString());//Registro
                numb.Add(sdr[2].ToString());//Carrera
                numb.Add(sdr[3].ToString());//Curso
                al.Add(numb);
            }
            connection.Close();
            return View(al);
        }

        // GET: CursoEstudiante/Create
        public ActionResult Create()
        {
            var Estudiantes = (from item in _context.Estudiante
                            select new SelectListItem()
                            {
                                Value = item.Carnet.ToString(),
                                Text = item.Carnet.ToString()
                            }).ToList();
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
            ViewData["Estudiantes"] = Estudiantes;
            ViewData["Maestros"] = Maestros;
            ViewData["Carreras"] = Carreras;
            ViewData["Cursos"] = Cursos;
            return View();
        }

        // POST: CursoEstudiante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                String Registro = "";
                String Carrera = "";
                String Curso = "";
                String Carnet = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "carnet":
                            Carnet = item.Value;
                            break;
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
                if (Carnet!="" && Registro != "" && Carrera != "" && Curso != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "INSERT INTO curso_estudiante(carnet,registro,carrera,curso) VALUES  (@param1,@param2,@param3,@param4)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = Carnet;
                        cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = Registro;
                        cmd.Parameters.Add("@param3", SqlDbType.Int).SqlValue = Carrera;
                        cmd.Parameters.Add("@param4", SqlDbType.Int).SqlValue = Curso;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var Estudiantes = (from item in _context.Estudiante
                                   select new SelectListItem()
                                   {
                                       Value = item.Carnet.ToString(),
                                       Text = item.Nombre.ToString()
                                   }).ToList();
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
                ViewData["Estudiantes"] = Estudiantes;
                ViewData["Maestros"] = Maestros;
                ViewData["Carreras"] = Carreras;
                ViewData["Cursos"] = Cursos;
                return View();
            }
        }

        // GET: CursoEstudiante/Delete/5
        public ActionResult Delete(int carnet, int registro, int carrera, int curso)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>();
            connection.Open();
            String sql = " SELECT * FROM curso_estudiante WHERE curso_estudiante.carnet =@param1 AND curso_estudiante.registro = @param2 AND curso_estudiante.carrera =@param3 AND curso_estudiante.curso  = @param4";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = carnet;
                cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = registro;
                cmd.Parameters.Add("@param3", SqlDbType.Int).SqlValue = carrera;
                cmd.Parameters.Add("@param4", SqlDbType.Int).SqlValue = curso;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    numb.Add(sdr[0].ToString());//Carnet
                    numb.Add(sdr[1].ToString());//Registro
                    numb.Add(sdr[2].ToString());//Carrera
                    numb.Add(sdr[3].ToString());//Curso
                }
            }
            connection.Close();
            return View(numb);
        }

        // POST: CursoEstudiante/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int carnet, int registro, int carrera, int curso, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                String sql = "DELETE FROM curso_estudiante WHERE curso_estudiante.carnet =@param1 AND curso_estudiante.registro = @param2 AND curso_estudiante.carrera =@param3 AND curso_estudiante.curso  = @param4";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = carnet;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = registro;
                    cmd.Parameters.Add("@param3", SqlDbType.Int).SqlValue = carrera;
                    cmd.Parameters.Add("@param4", SqlDbType.Int).SqlValue = curso;
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

        // GET: CursoEstudiante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: CursoEstudiante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: CursoEstudiante/Edit/5
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
    }
}