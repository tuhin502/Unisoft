using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Threading;

namespace ANOMS.Controllers.Web
{
    public class loginController : Controller
    {
        SqlConnection con = new SqlConnection(ConfigurationManager.ConnectionStrings["ANOMSBDConnectionString"].ConnectionString);
        // GET: login
        [OutputCache(Duration = 15)]
        public ActionResult login()
        {
            _DoBackendStaff();
            return View();
        }


        [HttpPost]
        public ActionResult login(string log)
        {
            string id = Request["id"];
            string password = Request["password"];
            string query = "select *,case when t_ID = 1 then 'Admin' when t_ID = 2 then 'Super Admin' else 'Deler'end as AdminType from Usersd where id = '" + id + "' and password = '" + password + "' and active = 0 ";

            SqlDataAdapter sda = new SqlDataAdapter(query, con);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            if (dt.Rows.Count > 0 && dt.Rows[0]["AdminType"].ToString() != "Deler")
            {

                HttpCookie cookie = new HttpCookie("anmos");
                cookie["id"] = id;
                cookie.Expires = DateTime.Now.AddDays(2);
                Response.Cookies.Add(cookie);

                Session["Type"] = dt.Rows[0]["AdminType"].ToString();
                //HttpCookie type = new HttpCookie("type");
                //type["type"] = dt.Rows[0]["AdminType"].ToString();
                //type.Expires = DateTime.Now.AddDays(2);
                //Response.Cookies.Add(type);
                string check = dt.Rows.ToString();
                if (Session["Type"].Equals("Super Admin"))
                {
                    HttpCookie type = new HttpCookie("type");
                    type["type"] = "Super Admin";
                    type.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(type);
                    Response.Redirect("/Home/Index");
                }
                else if (Session["Type"].Equals("Admin"))
                {
                    HttpCookie type = new HttpCookie("type");
                    type["type"] = "Admin";
                    type.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(type);
                    Response.Redirect("/Home/Index");
                }
                else
                {
                    HttpCookie type = new HttpCookie("type");
                    type["type"] = "Dealer";
                    type.Expires = DateTime.Now.AddDays(2);
                    Response.Cookies.Add(type);
                    Response.Redirect("/Home/Index");
                }
                return View();
            }
            else
            {
                ViewBag.msg = "Invalid UserID or Password";
                return View();
            }

        }
        private void _DoBackendStaff()
        {
            Thread.Sleep(500);
        }
        public ActionResult signout()
        {
            HttpCookie cookie = new HttpCookie("anmos");
            cookie.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(cookie);
            Response.Redirect("/login/login");
            return View();
        }
    }
}