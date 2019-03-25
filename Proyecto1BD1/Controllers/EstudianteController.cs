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
    public class EstudianteController : Controller
    {
        private readonly IConfiguration configuration;
        public EstudianteController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: Estudiante
        public ActionResult Index()
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM estudiante", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Carnet
                numb.Add(sdr[1].ToString());//nombre
                numb.Add(sdr[2].ToString());//Apellido
                numb.Add(sdr[6].ToString());//Correo
                al.Add(numb);
            }
            connection.Close();
            return View(al);
        }        

        // GET: Estudiante/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Estudiante/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                String Carnet = "";
                String Nombre = "";
                String Apellido = "";
                String Telefono = "";
                String TelefonoTutor = "";
                String Direccion ="";
                String Correo = "";
                String FechaNacimiento = "";
                String NumeroPartida = "";
                String Fotografia = "";
                String password = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "carnet":
                            Carnet = item.Value;
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
                        case "telefonotutor":
                            TelefonoTutor = item.Value;
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
                        case "numeropartida":
                            NumeroPartida = item.Value;
                            break;
                        case "fotografia":
                            Fotografia = item.Value;
                            break;
                        case "password":
                            password= item.Value;
                            break;                        
                    }
                }
                if (Carnet != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "EXEC INSERCION_ESTUDIANTE @carnet =  @param1,@nombre = @param2,@apellido=@param3,@telefono = @param4, @telefono_tutor = @param5, @direccion = @param6, @correo = @param7, @fecha_nacimiento = @param8, @numero_partida = @param9,@fotografia = @param10, @password = @param11";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = Carnet;
                        cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).SqlValue = Nombre;
                        cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).SqlValue = Apellido;
                        cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).SqlValue = Telefono;
                        cmd.Parameters.Add("@param5", SqlDbType.VarChar, 255).SqlValue = TelefonoTutor;
                        cmd.Parameters.Add("@param6", SqlDbType.VarChar, 255).SqlValue = Direccion;
                        cmd.Parameters.Add("@param7", SqlDbType.VarChar, 255).SqlValue = Correo;
                        cmd.Parameters.Add("@param8", SqlDbType.Date).SqlValue = FechaNacimiento;
                        cmd.Parameters.Add("@param9", SqlDbType.VarChar, 255).SqlValue = NumeroPartida;
                        cmd.Parameters.Add("@param10", SqlDbType.VarChar,255).SqlValue = Fotografia;
                        cmd.Parameters.Add("@param11", SqlDbType.VarChar, 255).SqlValue = password;
                        cmd.CommandType = CommandType.Text;
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

        // GET: Estudiante/Delete/5
        public ActionResult Delete(int id)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>(); ;
            connection.Open();
            String sql = " SELECT * FROM estudiante WHERE estudiante.carnet = @param1";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int, 255).SqlValue = id;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    numb.Add(sdr[0].ToString());//Id
                    numb.Add(sdr[1].ToString());//Nombre
                    numb.Add(sdr[2].ToString());//Descripcion
                }
            }
            connection.Close();
            return View(numb);
        }

        // POST: Estudiante/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                String sql = "DELETE FROM estudiante WHERE estudiante.carnet = @param1; ";
                using (SqlCommand cmd = new SqlCommand(sql, connection))
                {
                    cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = id;
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

        // GET: Estudiante/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Estudiante/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                String Carnet = "";
                String Nombre = "";
                String Apellido = "";
                String Telefono = "";
                String TelefonoTutor = "";
                String Direccion = "";
                String Correo = "";
                String FechaNacimiento = "";
                String NumeroPartida = "";
                String Fotografia = "";
                String password = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {                        
                        case "nombre":
                            Nombre = item.Value;
                            break;
                        case "apellido":
                            Apellido = item.Value;
                            break;
                        case "telefono":
                            Telefono = item.Value;
                            break;
                        case "telefonotutor":
                            TelefonoTutor = item.Value;
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
                        case "numeropartida":
                            NumeroPartida = item.Value;
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
                    String sql = "UPDATE estudiante SET nombre = @param2,apellido=@param3,telefono = @param4, telefono_tutor = @param5, direccion = @param6, correo = @param7, fecha_nacimiento = @param8, numero_partida = @param9,fotografia = @param10 WHERE carnet = @param1";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = id;
                        cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).SqlValue = Nombre;
                        cmd.Parameters.Add("@param3", SqlDbType.VarChar, 255).SqlValue = Apellido;
                        cmd.Parameters.Add("@param4", SqlDbType.VarChar, 255).SqlValue = Telefono;
                        cmd.Parameters.Add("@param5", SqlDbType.VarChar, 255).SqlValue = TelefonoTutor;
                        cmd.Parameters.Add("@param6", SqlDbType.VarChar, 255).SqlValue = Direccion;
                        cmd.Parameters.Add("@param7", SqlDbType.VarChar, 255).SqlValue = Correo;
                        cmd.Parameters.Add("@param8", SqlDbType.Date).SqlValue = FechaNacimiento;
                        cmd.Parameters.Add("@param9", SqlDbType.VarChar, 255).SqlValue = NumeroPartida;
                        cmd.Parameters.Add("@param10", SqlDbType.VarChar, 255).SqlValue = Fotografia;
                        //cmd.Parameters.Add("@param11", SqlDbType.VarBinary, 1).SqlValue = password;
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

        // GET: Estudiante/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}