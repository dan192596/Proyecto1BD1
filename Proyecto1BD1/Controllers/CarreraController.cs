using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;

namespace Proyecto1BD1.Controllers
{
    public class CarreraController : Controller
    {
        private readonly IConfiguration configuration;
        public CarreraController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: Carrera
        public ActionResult Index()
        {            
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM carrera", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Id
                numb.Add(sdr[1].ToString());//nombre
                numb.Add(sdr[2].ToString());//Descripcion
                al.Add(numb);                
            }            
            connection.Close();
            return View(al);
        }        

        // GET: Carrera/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Carrera/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                String Nombre = "";
                String Descripcion = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "nombre":
                            Nombre = item.Value;
                            break;
                        case "descripcion":
                            Descripcion = item.Value;
                            break;
                    }
                }
                if (Nombre != "" && Descripcion != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "EXEC INSERCION_CARRERA @nombre =  @param1,@descripcion = @param2";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).SqlValue = Nombre;
                        cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).SqlValue = Descripcion;
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

        // GET: Carrera/Delete/5
        public ActionResult Delete(int id)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>(); ;
            connection.Open();            
            String sql = " SELECT * FROM carrera WHERE carrera.carrera = @param1";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).SqlValue = id;                
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

        // POST: Carrera/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();                
                String sql = "DELETE FROM carrera WHERE carrera.carrera = @param1; ";
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

        // GET: Carrera/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Carrera/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                String Nombre = "";
                String Descripcion = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString())
                    {
                        case "Nombre":
                            Nombre = item.Value;
                            break;
                        case "Descripcion":
                            Descripcion = item.Value;
                            break;
                    }
                }
                if (Nombre != "" && Descripcion != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "UPDATE carrera SET nombre = @param1, descripcion = @param2 WHERE carrera = @param3";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).SqlValue = Nombre;
                        cmd.Parameters.Add("@param2", SqlDbType.VarChar, 255).SqlValue = Descripcion;
                        cmd.Parameters.Add("@param3", SqlDbType.Int).SqlValue = id;
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

        // GET: Carrera/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}