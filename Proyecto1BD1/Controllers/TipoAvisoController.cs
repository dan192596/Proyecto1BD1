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
    public class TipoAvisoController : Controller
    {
        private readonly IConfiguration configuration;
        public TipoAvisoController(IConfiguration configuration)
        {
            this.configuration = configuration;
        }
        // GET: Aviso
        public ActionResult Index()
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<List<String>> al = new List<List<String>>();
            connection.Open();
            SqlCommand com = new SqlCommand("SELECT * FROM tipo_aviso", connection);
            SqlDataReader sdr = com.ExecuteReader();
            while (sdr.Read())
            {
                List<String> numb = new List<string>();
                numb.Add(sdr[0].ToString());//Id                
                numb.Add(sdr[1].ToString());//Descripcion
                al.Add(numb);
            }
            connection.Close();
            return View(al);
        }

        // GET: Aviso/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Aviso/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                String Descripcion = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "descripcion":
                            Descripcion = item.Value;
                            break;
                    }
                }
                if (Descripcion != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "INSERT INTO tipo_aviso(descripcion) VALUES (@param1)";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {                        
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).SqlValue = Descripcion;
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

        // GET: Aviso/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Aviso/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                String Descripcion = "";
                foreach (var item in collection)
                {
                    System.Diagnostics.Debug.WriteLine(item.ToString());
                    switch (item.Key.ToString().ToLower())
                    {
                        case "descripcion":
                            Descripcion = item.Value;
                            break;
                    }
                }
                if (Descripcion != "")
                {
                    String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                    SqlConnection connection = new SqlConnection(connectionstring);
                    connection.Open();
                    String sql = "UPDATE tipo_aviso SET descripcion = @param1 WHERE tipo_aviso.tipo_aviso = @param2";
                    using (SqlCommand cmd = new SqlCommand(sql, connection))
                    {
                        cmd.Parameters.Add("@param1", SqlDbType.VarChar, 255).SqlValue = Descripcion;
                        cmd.Parameters.Add("@param2", SqlDbType.Int).SqlValue = id;
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

        // GET: Aviso/Delete/5
        public ActionResult Delete(int id)
        {
            String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            SqlConnection connection = new SqlConnection(connectionstring);
            List<String> numb = new List<string>(); ;
            connection.Open();
            String sql = " SELECT * FROM tipo_aviso WHERE tipo_aviso.tipo_aviso = @param1";
            using (SqlCommand cmd = new SqlCommand(sql, connection))
            {
                cmd.Parameters.Add("@param1", SqlDbType.Int).SqlValue = id;
                cmd.CommandType = CommandType.Text;
                SqlDataReader sdr = cmd.ExecuteReader();
                while (sdr.Read())
                {
                    numb.Add(sdr[0].ToString());//Id
                    numb.Add(sdr[1].ToString());//Descripcion
                }
            }
            connection.Close();
            return View(numb);
        }

        // POST: Aviso/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                String connectionstring = configuration.GetConnectionString("DefaultConnectionString");
                SqlConnection connection = new SqlConnection(connectionstring);
                connection.Open();
                String sql = "DELETE FROM tipo_aviso WHERE tipo_aviso.tipo_aviso = @param1;";
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

        // GET: Aviso/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }
    }
}