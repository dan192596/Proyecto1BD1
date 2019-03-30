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
    public class AlumnoCarreraController : Controller
    {
        private readonly Proyecto1Context _context = new Proyecto1Context();
        private readonly IConfiguration configuration;

        public AlumnoCarreraController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: AlumnoCarrera
        public ActionResult Index()
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("EXEC VER_ESTUDIANTE_CARRERA", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Id Carrera
                numb.Add(sdr[1].ToString());//Id Estudiante
                numb.Add(sdr[2].ToString());//Nombre Carrera
                numb.Add(sdr[3].ToString());//Nombre Estudiante                
                al.Add(numb);
            }
            connection.Close();
            return View(al);
        }        

        // GET: AlumnoCarrera/Create
        public ActionResult Create()
        {
            var Carreras = (from item in _context.Carrera
                            select new SelectListItem()
                            {
                                Value = item.Carrera1.ToString(),
                                Text = item.Nombre.ToString()
                            }).ToList();
            var Alumnos = (from item in _context.Estudiante
                          select new SelectListItem()
                          {
                              Value = item.Carnet.ToString(),
                              Text = item.Nombre.ToString()
                          }).ToList();
            ViewData["Carreras"] = Carreras;
            ViewData["Alumnos"] = Alumnos;
            return View();
        }

        // POST: AlumnoCarrera/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                String Carnet = "";
                String Carrera = "";                
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "carnet":
                            Carnet = item.Value;
                            break;
                        case "carrera":
                            Carrera = item.Value;
                            break;
                    }
                }
                if (Carnet != "" && Carrera != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "EXEC ASIGNAR_ESTUDIANTE_CARRERA @carnet = @param1, @carrera= @param2";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = Carnet;
                        cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = Carrera;
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                var Carreras = (from item in _context.Carrera
                                select new SelectListItem()
                                {
                                    Value = item.Carrera1.ToString(),
                                    Text = item.Nombre.ToString()
                                }).ToList();
                var Alumnos = (from item in _context.Estudiante
                               select new SelectListItem()
                               {
                                   Value = item.Carnet.ToString(),
                                   Text = item.Nombre.ToString()
                               }).ToList();
                ViewData["Carreras"] = Carreras;
                ViewData["Alumnos"] = Alumnos;
                return View();
            }
        }

        // GET: AlumnoCarrera/Delete/5
        public ActionResult Delete(int carrera, int estudiante)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>();
            connection.Open();
            String sql = " SELECT curso_estudiante.carrera, curso_estudiante.carnet FROM curso_estudiante WHERE curso_estudiante.carrera =@param1 AND curso_estudiante.carnet  = @param2";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {                
                cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = carrera;
                cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = estudiante;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    numb.Add(sdr[0].ToString());//Carnet
                    numb.Add(sdr[1].ToString());//Carrera                    
                }
            }
            connection.Close();
            return View(numb);
        }

        // POST: AlumnoCarrera/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int carrera, int estudiante, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                String sql = "DELETE FROM curso_estudiante WHERE curso_estudiante.carrera =@param1 AND curso_estudiante.carnet  = @param2";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = carrera;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = estudiante;
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
    }
}