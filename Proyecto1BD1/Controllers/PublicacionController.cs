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
    public class PublicacionController : Controller
    {
        private readonly IConfiguration configuration;
        public PublicacionController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: Publicacion
        public ActionResult Index()
        {
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
        }

        // GET: Publicacion/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Publicacion/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                String FechaHora = "";
                String Descripcion = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "descripcion":
                            Descripcion = item.Value;
                            break;
                        case "fecahora":
                            FechaHora = item.Value;
                            break;
                    }
                }
                if (Descripcion != "" && FechaHora != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "INSERT INTO publicacion(descripcion,feca_hora) VALUES (@param1, @param2)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).SqlValue = Descripcion;
                        cmd.Parameters.Add("@param2", SqlDbType.DateTime).SqlValue = FechaHora;
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

        // GET: Publicacion/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Publicacion/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                String FechaHora = "";
                String Descripcion = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "descripcion":
                            Descripcion = item.Value;
                            break;
                        case "fecahora":
                            FechaHora = item.Value;
                            break;
                    }
                }
                if (Descripcion != "" && FechaHora != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "UPDATE publicacion SET descripcion = @param1, feca_hora = @param2 WHERE publicacion.publicacion = @param3";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).SqlValue = Descripcion;
                        cmd.Parameters.Add("@param2", SqlDbType.DateTime).SqlValue = FechaHora;
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

        // GET: Publicacion/Delete/5
        public ActionResult Delete(int id)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>(); ;
            connection.Open();
            String sql = " SELECT * FROM publicacion WHERE publicacion.publicacion = @param1";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = id;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    numb.Add(sdr[0].ToString());//Id
                    numb.Add(sdr[1].ToString());//Descripcion
                    numb.Add(sdr[2].ToString());//FechaHora
                }
            }
            connection.Close();
            return View(numb);
        }

        // POST: Publicacion/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                String sql = "DELETE FROM publicacion WHERE publicacion.publicacion = @param1; ";
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

        // GET: Publicacion/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}