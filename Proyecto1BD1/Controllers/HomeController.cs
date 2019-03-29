using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols;
using Proyecto1BD1.Models;
using System.Configuration;

namespace Proyecto1BD1.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration configuration;

        public object Enconding { get; private set; }

        public HomeController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        // POST: Login/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here
                String usuario = "";
                String password = null;
                String rol = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "user":
                            usuario = item.Value;
                            break;
                        case "password":
                            password = item.Value;
                            break;
                        case "rol":
                            rol = item.Value;
                            break;
                    }
                }//LOGIN PARA TIPO DE USUARIO ADMINISTRADOR
                if (rol.Equals("administrador"))
                {
                    if (usuario == "bases1" && password == "123456789")
                    {
                        return RedirectToAction("Index", "ModuloAdministrador");
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }//LOGIN PARA TIPO DE USUARIO MAESTRO
                else if (rol.Equals("maestro"))
                {
                    int ContadorFilas = 0;
                    List<String> ValoresUsuario = new List<string>();
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "EXEC LOGIN_MAESTRO @registro =  @param1, @password = @param2";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = usuario;
                        cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).SqlValue = password;
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            ValoresUsuario.Add(sdr[0].ToString());//Registro
                            ValoresUsuario.Add(sdr[1].ToString());//nombre
                            ValoresUsuario.Add(sdr[2].ToString());//Apellido
                            ValoresUsuario.Add(sdr[3].ToString());//Imagen
                            ContadorFilas++;
                        }
                    }
                    connection.Close();
                    if (ContadorFilas == 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("Index", "ModuloMaestro");
                    }
                }//LOGIN PARA TIPO DE USUARIO ALUMNO
                else if(rol.Equals("alumno")){
                    int ContadorFilas = 0;
                    List<String> ValoresUsuario = new List<string>();
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "EXEC LOGIN_ESTUDIANTE @carnet =  @param1, @password = @param2";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = usuario;
                        cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).SqlValue = password;
                        cmd.CommandType = CommandType.Text;
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            ValoresUsuario.Add(sdr[0].ToString());//Registro
                            ValoresUsuario.Add(sdr[1].ToString());//nombre
                            ValoresUsuario.Add(sdr[2].ToString());//Apellido
                            ValoresUsuario.Add(sdr[3].ToString());//Imagen
                            ContadorFilas++;
                        }
                    }
                    connection.Close();
                    if (ContadorFilas == 0)
                    {
                        return RedirectToAction(nameof(Index));
                    }
                    else
                    {
                        return RedirectToAction("Index", "ModuloEstudiante");
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
