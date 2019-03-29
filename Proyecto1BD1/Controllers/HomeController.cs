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
                }
                if (rol.Equals("administrador"))
                {
                    /*string connectionstring = ConfigurationManager.GetConnectionString("DefaultConnectionString");
                    SqlConnection con = new SqlConnection(connectionstring);// pass connection string here


                    SqlCommand cmd = new SqlCommand("LOGIN_MAESTRO",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@usuario", SqlDbType.Text).Value = usuario;
                    cmd.Parameters.Add("@registro", SqlDbType.Int).Value = password;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {

                        // do your code to show data of datatable in grid or any controls if you are showing it in grid then below is code
                        //GridView1.DataSource = dt;
                        //GridView1.DataBind();

                    }
                    else
                    {
                        //lblError.Text = "UserName, Password and UserType are not valid.";
                    }*/
                    var dtLogin = getVariableSesion(usuario, password);
                    //var nombre = dtLogin.Rows[0]["nombre"];

                    if (dtLogin == null)
                    {
                    }
                    else {
                        foreach (DataRow drow in dtLogin.Rows) {
                            Console.WriteLine(drow["nombre"]);
                        }
                    }

                    return RedirectToAction("Index", "ModuloAdministrador");
                }
                else if (rol.Equals("maestro"))
                {
                    return RedirectToAction("Index", "ModuloMaestro");
                }
                else if(rol.Equals("alumno")){

                }
                 //RedirectToPage("ModuloMaestro//Actividades.cshtml");
                //Redirect("ModuloMaestro//Actividades.cshtml");
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        protected DataTable getVariableSesion(string usuario, string password) {
            //var connstring = ConfigurationManager.
            //    ConnectionStrings["DefaultConnectionString"].ConnectionString;
            //string connectionstring = ConfigurationManager.GetConnectionString("DefaultConnectionString");
            
            string connectionstring = configuration.GetConnectionString("DefaultConnectionString");
            // pass connection string here

            using (SqlConnection sql = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand("LOGIN_MAESTRO", sql))
                {
                    cmd.CommandType = CommandType.StoredProcedure;
                    sql.Open();
                    cmd.Parameters.Add(new SqlParameter("@registro", usuario));
                    cmd.Parameters.Add(new SqlParameter("@password", password));
                    var dt = new DataTable();
                    var da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    return dt;
                }
            }

            /*SqlConnection con = new SqlConnection("DefaultConnectionString");// pass connection string here


            SqlCommand cmd = new SqlCommand("LOGIN_MAESTRO");
            cmd.CommandType = CommandType.StoredProcedure;
            cmd.Parameters.Add("@registro", SqlDbType.Text).Value = usuario;
            cmd.Parameters.Add("@password", SqlDbType.Text).Value = password;
            SqlDataAdapter da = new SqlDataAdapter(cmd,con);

            DataTable dt = new DataTable();
            da.Fill(dt);

            return dt;*/
        }

    }
}
