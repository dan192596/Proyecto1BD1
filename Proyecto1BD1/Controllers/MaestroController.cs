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
    public class MaestroController : Controller
    {
        private readonly IConfiguration configuration;
        public MaestroController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: Maestro
        public ActionResult Index()
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM maestro", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Registro
                numb.Add(sdr[1].ToString());//Carnet
                numb.Add(sdr[2].ToString());//nombre
                numb.Add(sdr[3].ToString());//Apellido
                numb.Add(sdr[6].ToString());//Correo
                al.Add(numb);
            }
            connection.Close();
            return View(al);
        }

        // GET: Maestro/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Maestro/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                String Registro = "";
                String Dpi = "";
                String Nombre = "";
                String Apellido = "";
                String Telefono = "";
                String Direccion = "";
                String Correo = "";
                String FechaNacimiento = "";
                String Fotografia = "";
                String password = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "registro":
                            Registro = item.Value;
                            break;
                        case "dpi":
                            Dpi = item.Value;
                            break;
                        case "nombre":
                            Nombre = item.Value;
                            break;
                        case "apellido":
                            Apellido = item.Value;
                            break;
                        case "telefono":
                            Telefono = item.Value;
                            break;
                        case "direccion":
                            Direccion = item.Value;
                            break;
                        case "correo":
                            Correo = item.Value;
                            break;
                        case "fechanacimiento":
                            FechaNacimiento = item.Value.ToString();
                            break;                        
                        case "fotografia":
                            Fotografia = item.Value;
                            break;
                        case "password":
                            password = item.Value;
                            break;
                    }
                }
                if (Registro != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "EXEC INSERCION_MAESTRO @registro =  @param1, @dpi = @param2, @nombre = @param3, @apellido = @param4, @telefono = @param5, @direccion = @param6, @correo = @param7, @fecha_nacimiento = @param8, @fotografia = @param9, @password = @param10";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = Registro;
                        cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = Dpi;
                        cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).SqlValue = Nombre;
                        cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).SqlValue = Apellido;
                        cmd.Parameters.Add("@param5", SqlDbType.VarChar, 255).SqlValue = Telefono;                        
                        cmd.Parameters.Add("@param6", SqlDbType.VarChar, 255).SqlValue = Direccion;
                        cmd.Parameters.Add("@param7", SqlDbType.VarChar, 255).SqlValue = Correo;
                        cmd.Parameters.Add("@param8", SqlDbType.Date).SqlValue = FechaNacimiento;
                        cmd.Parameters.Add("@param9", SqlDbType.VarChar, 255).SqlValue = Fotografia;
                        cmd.Parameters.Add("@param10", SqlDbType.VarChar, 255).SqlValue = password;
                        cmd.CommandType = CommandType.Text;
                        System.Diagnostics.Debug.WriteLine("algo");
                        cmd.ExecuteNonQuery();
                    }
                    connection.Close();
                }
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Maestro/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Maestro/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {                
                String Dpi = "";
                String Nombre = "";
                String Apellido = "";
                String Telefono = "";
                String Direccion = "";
                String Correo = "";
                String FechaNacimiento = "";
                String Fotografia = "";
                String password = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {                        
                        case "dpi":
                            Dpi = item.Value;
                            break;
                        case "nombre":
                            Nombre = item.Value;
                            break;
                        case "apellido":
                            Apellido = item.Value;
                            break;
                        case "telefono":
                            Telefono = item.Value;
                            break;
                        case "direccion":
                            Direccion = item.Value;
                            break;
                        case "correo":
                            Correo = item.Value;
                            break;
                        case "fechanacimiento":
                            FechaNacimiento = item.Value.ToString();
                            break;
                        case "fotografia":
                            Fotografia = item.Value;
                            break;
                        case "password":
                            password = item.Value;
                            break;
                    }
                }                
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "UPDATE maestro SET dpi = @param2, nombre =  @param3,apellido=@param4,telefono = @param5, direccion = @param6, correo = @param7, fecha_nacimiento = @param8, fotografia = @param9 WHERE registro = @param1";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = id;
                    cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = Dpi;
                    cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).SqlValue = Nombre;
                    cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).SqlValue = Apellido;
                    cmd.Parameters.Add("@param5", SqlDbType.VarChar, 255).SqlValue = Telefono;
                    cmd.Parameters.Add("@param6", SqlDbType.VarChar, 255).SqlValue = Direccion;
                    cmd.Parameters.Add("@param7", SqlDbType.VarChar, 255).SqlValue = Correo;
                    cmd.Parameters.Add("@param8", SqlDbType.Date).SqlValue = FechaNacimiento;
                    cmd.Parameters.Add("@param9", SqlDbType.VarChar, 255).SqlValue = Fotografia;
                    //cmd.Parameters.Add("@param10", SqlDbType.VarChar, 255).SqlValue = password;
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

        // GET: Maestro/Delete/5
        public ActionResult Delete(int id)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>(); ;
            connection.Open();
            String sql = " SELECT * FROM maestro WHERE maestro.registro = @param1";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = id;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    numb.Add(sdr[0].ToString());//Registro
                    numb.Add(sdr[1].ToString());//Carnet
                    numb.Add(sdr[2].ToString());//nombre
                    numb.Add(sdr[3].ToString());//Apellido
                    numb.Add(sdr[6].ToString());//Correo
                }
            }
            connection.Close();
            return View(numb);
        }

        // POST: Maestro/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                String sql = "DELETE FROM maestro WHERE maestro.registro = @param1; ";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = id;
                    cmd.CommandType = CommandType.Text;
                    cmd.ExecuteNonQuery();
                }
                connection.Close();
                return RedirectToAction(nameof(Index));
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Maestro/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}